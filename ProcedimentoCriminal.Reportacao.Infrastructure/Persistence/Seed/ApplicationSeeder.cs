using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Seed
{
    public class ApplicationSeeder
    {
        private static readonly Dictionary<string, Type> _enumTables = new Dictionary<string, Type>
        {
            {"categorias_veiculo", typeof(CategoriaVeiculo)},
            {"envolvimentos", typeof(Envolvimento)},
            {"estados_civis", typeof(EstadoCivil)},
            {"graus_instrucao", typeof(GrauInstrucao)},
            {"meios_empregados", typeof(MeioEmpregado)},
            {"naturezas", typeof(Natureza)},
            {"naturezas_acidente", typeof(NaturezaAcidente)},
            {"tipos_objeto", typeof(TipoVeiculo)},
            {"tipos_veiculo", typeof(TipoVeiculo)},
            {"ufs", typeof(Uf)},
        };

        private readonly IDapperConnectionFactory _factory;

        public ApplicationSeeder(IDapperConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task SeedDatabaseAsync()
        {
            using var connection = await _factory.CreateConnectionAsync();
            var tables = await connection.QueryAsync<string>("SHOW TABLES");
            if (tables.Any()) return;

            using var transaction = connection.BeginTransaction();

            await CreateTables(connection, transaction);
            await SeedEnumTables(connection, transaction);
            await CreateTriggers(connection, transaction);

            transaction.Commit();
        }

        private async Task SeedEnumTables(IDbConnection connection, IDbTransaction transaction)
        {
            foreach (var table in _enumTables)
            {
                var sb = new StringBuilder($"INSERT INTO {table.Key} (id, descricao) VALUES ");
                foreach (var value in Enum.GetValues(table.Value))
                    sb.Append($"({(int) value},'{value.ToString()}'),");
                sb.Length--;

                await connection.ExecuteAsync(sb.ToString(), transaction: transaction);
            }
        }

        private async Task CreateTables(IDbConnection connection, IDbTransaction transaction)
        {
            foreach (var table in _enumTables)
            {
                await connection.ExecuteAsync(
                    "CREATE TABLE `" + table.Key + "` (" +
                    "`id` int NOT NULL," +
                    "`descricao` varchar(45) NOT NULL," +
                    "PRIMARY KEY (`id`)" +
                    ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci",
                    transaction: transaction);
            }

            await connection.ExecuteAsync(
                "CREATE TABLE `ocorrencias` (" +
                "`id` char(36) NOT NULL," +
                "`identificador_ocorrencia` varchar(20) NOT NULL," +
                "`delegacia_policia_apuracao` varchar(45) NOT NULL," +
                "`inicio_periodo_fato` datetime NOT NULL," +
                "`fim_periodo_fato` datetime NOT NULL," +
                "`data_hora_comunicacao` datetime NOT NULL," +
                "`local_fato` varchar(200) NOT NULL," +
                "`descricao_fato` varchar(400) DEFAULT NULL," +
                "`id_natureza` int NOT NULL," +
                "`created` datetime NOT NULL," +
                "`created_by` varchar(45) NOT NULL," +
                "`last_modified` datetime DEFAULT NULL," +
                "`last_modified_by` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY (`id`)," +
                "KEY `natureza_fk_idx` (`id_natureza`)," +
                "CONSTRAINT `natureza_fk` FOREIGN KEY(`" +
                "id_natureza`) REFERENCES `naturezas` (`id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci", transaction: transaction);

            await connection.ExecuteAsync(
                "CREATE TABLE `pessoas_envolvidas` (" +
                "`id` char(36) NOT NULL, " +
                "`id_envolvimento` int NOT NULL," +
                "`nome` varchar(45) DEFAULT NULL," +
                "`identidade_rg` int DEFAULT NULL," +
                "`identidade_orgao_emissor` varchar(10) DEFAULT NULL," +
                "`id_identidade_uf` int DEFAULT NULL," +
                "`nome_mae` varchar(45) DEFAULT NULL," +
                "`nome_pai` varchar(45) DEFAULT NULL," +
                "`nascimento_data` datetime DEFAULT NULL," +
                "`id_nascimento_uf` int DEFAULT NULL," +
                "`sexo` char(1) DEFAULT NULL," +
                "`cpf` varchar(11) DEFAULT NULL," +
                "`passaporte` varchar(20) DEFAULT NULL," +
                "`id_estado_civil` int DEFAULT NULL," +
                "`id_grau_instrucao` int DEFAULT NULL," +
                "`nome_social` varchar(45) DEFAULT NULL," +
                "`endereco_residencial` varchar(200) DEFAULT NULL," +
                "`endereco_comercial` varchar(200) DEFAULT NULL," +
                "`id_ocorrencia` char(36) NOT NULL," +
                "`created` datetime NOT NULL," +
                "`created_by` varchar(45) NOT NULL," +
                "`last_modified` datetime DEFAULT NULL," +
                "`last_modified_by` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY(`id`)," +
                "KEY `ocorrencias_fk_idx` (`id_ocorrencia`)," +
                "CONSTRAINT `ocorrencias_fk` FOREIGN KEY(`" +
                "id_ocorrencia`) REFERENCES `ocorrencias` (`id`)," +
                "KEY `envolvimentos_fk_idx` (`id_envolvimento`)," +
                "CONSTRAINT `envolvimentos_fk` FOREIGN KEY(`" +
                "id_envolvimento`) REFERENCES `envolvimentos` (`id`)," +
                "KEY `ufs_identidade_fk_idx` (`id_identidade_uf`)," +
                "CONSTRAINT `identidade_ufs_fk` FOREIGN KEY(`" +
                "id_identidade_uf`) REFERENCES `ufs` (`id`)," +
                "KEY `ufs_nascimento_fk_idx` (`id_nascimento_uf`)," +
                "CONSTRAINT `nascimento_ufs_fk` FOREIGN KEY(`" +
                "id_nascimento_uf`) REFERENCES `ufs` (`id`)," +
                "KEY `estados_civis_fk_idx` (`id_estado_civil`)," +
                "CONSTRAINT `estados_civis_fk` FOREIGN KEY(`" +
                "id_estado_civil`) REFERENCES `estados_civis` (`id`)," +
                "KEY `graus_instrucao_fk_idx` (`id_grau_instrucao`)," +
                "CONSTRAINT `graus_instrucao_fk` FOREIGN KEY(`" +
                "id_grau_instrucao`) REFERENCES `graus_instrucao` (`id`)" +
                "    ) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci",
                transaction: transaction);

            await connection.ExecuteAsync(
                "CREATE TABLE `veiculos_envolvidos` (" +
                "`id` char(36) NOT NULL, " +
                "`marca` varchar(45) DEFAULT NULL," +
                "`modelo` varchar(45) DEFAULT NULL," +
                "`cor` varchar(20) DEFAULT NULL," +
                "`renavam` varchar(20) DEFAULT NULL," +
                "`chassi` varchar(20) DEFAULT NULL," +
                "`ano_fabricacao` int DEFAULT NULL," +
                "`ano_modelo` int DEFAULT NULL," +
                "`placa` varchar(10) DEFAULT NULL," +
                "`segurado` tinyint DEFAULT NULL," +
                "`id_pessoa_envolvida` char(36) NOT NULL," +
                "`id_uf` int DEFAULT NULL," +
                "`id_tipo` int DEFAULT NULL," +
                "`id_categoria` int DEFAULT NULL," +
                "`created` datetime NOT NULL," +
                "`created_by` varchar(45) NOT NULL," +
                "`last_modified` datetime DEFAULT NULL," +
                "`last_modified_by` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY(`id`)," +
                "KEY `pessoas_envolvidas_fk_idx` (`id_pessoa_envolvida`)," +
                "CONSTRAINT `pessoas_envolvidas_veiculos_envolvidos_fk` FOREIGN KEY(`" +
                "id_pessoa_envolvida`) REFERENCES `pessoas_envolvidas` (`id`)," +
                "KEY `ufs_fk_idx` (`id_uf`)," +
                "CONSTRAINT `ufs_fk` FOREIGN KEY(`" +
                "id_uf`) REFERENCES `ufs` (`id`)," +
                "KEY `tipos_veiculo_fk_idx` (`id_tipo`)," +
                "CONSTRAINT `tipos_veiculo_fk` FOREIGN KEY(`" +
                "id_tipo`) REFERENCES `tipos_veiculo` (`id`)," +
                "KEY `categorias_veiculo_fk_idx` (`id_categoria`)," +
                "CONSTRAINT `categorias_veiculo_fk` FOREIGN KEY(`" +
                "id_categoria`) REFERENCES `categorias_veiculo` (`id`)" +
                "    ) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci",
                transaction: transaction);

            await connection.ExecuteAsync(
                "CREATE TABLE `objetos_envolvidos` (" +
                "`id` char(36) NOT NULL, " +
                "`descricao` varchar(100) NOT NULL," +
                "`id_pessoa_envolvida` char(36) NOT NULL," +
                "`id_tipo` int NOT NULL," +
                "`created` datetime NOT NULL," +
                "`created_by` varchar(45) NOT NULL," +
                "`last_modified` datetime DEFAULT NULL," +
                "`last_modified_by` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY(`id`)," +
                "KEY `pessoas_envolvidas_fk_idx` (`id_pessoa_envolvida`)," +
                "CONSTRAINT `pessoas_envolvidas_objetos_envolvidos_fk` FOREIGN KEY(`" +
                "id_pessoa_envolvida`) REFERENCES `pessoas_envolvidas` (`id`)," +
                "KEY `tipos_objeto_fk_idx` (`id_tipo`)," +
                "CONSTRAINT `tipos_objeto_fk` FOREIGN KEY(`" +
                "id_tipo`) REFERENCES `tipos_objeto` (`id`)" +
                "    ) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci",
                transaction: transaction);
        }

        private async Task CreateTriggers(IDbConnection connection, IDbTransaction transaction)
        {
            await connection.ExecuteAsync("CREATE TRIGGER update_identificador_ocorrencia " +
                                          "AFTER INSERT " +
                                          "ON ocorrencias FOR EACH ROW " +
                                          "BEGIN " +
                                          "UPDATE ocorrencias SET identificador_ocorrencia = " +
                                          "CONCAT((SELECT COUNT(*) FROM ocorrencias WHERE " +
                                          "delegacia_policia_apuracao = NEW.delegacia_policia_apuracao) + 1, " +
                                          "NEW.identificador_ocorrencia) " +
                                          "WHERE id = NEW.id; " +
                                          "END;  ", transaction: transaction);
        }
    }
}