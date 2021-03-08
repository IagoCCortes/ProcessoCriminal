using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Seed
{
    public class ApplicationSeeder
    {
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

            await connection.ExecuteAsync(
                "CREATE TABLE `orgaos` (" +
                "`id` int NOT NULL," +
                "`descricao` varchar(45) NOT NULL," +
                "PRIMARY KEY (`id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci",
                transaction: transaction);

            await connection.ExecuteAsync(
                "CREATE TABLE `naturezas` (" +
                "`id` int NOT NULL," +
                "`descricao` varchar(45) NOT NULL," +
                "PRIMARY KEY (`id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci",
                transaction: transaction);

            await connection.ExecuteAsync(
                "CREATE TABLE `tipos_ocorrencia` (" +
                "`id` int NOT NULL," +
                "`descricao` varchar(45) NOT NULL," +
                "PRIMARY KEY (`id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci",
                transaction: transaction);

            await connection.ExecuteAsync(
                "CREATE TABLE `ocorrencias` (" +
                "`id` char(36) NOT NULL," +
                "`identificador_ocorrencia` varchar(20) NOT NULL," +
                "`tipo` varchar(20) NOT NULL," +
                "`delegacia_policia_apuracao` varchar(45) NOT NULL," +
                "`natureza` varchar(45) NOT NULL," +
                "`data_hora_fato` datetime NOT NULL," +
                "`data_hora_comunicacao` datetime NOT NULL," +
                "`endereco_fato` varchar(200) DEFAULT NULL," +
                "`praticado_por_menor` tinyint NOT NULL," +
                "`local_periciado` tinyint NOT NULL," +
                "`tipo_local` varchar(45) NOT NULL," +
                "`objeto_meio_empregado` varchar(45) NOT NULL," +
                "`id_tipo_ocorrencia` int NOT NULL," +
                "`id_natureza` int NOT NULL," +
                "`created` datetime NOT NULL," +
                "`created_by` varchar(45) NOT NULL," +
                "`last_modified` datetime DEFAULT NULL," +
                "`last_modified_by` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY (`id`)," +
                "KEY `tipo_ocorrencia_fk_idx` (`id_tipo_ocorrencia`)," +
                "CONSTRAINT `tipo_ocorrencia_fk` FOREIGN KEY(`" +
                "id_tipo_ocorrencia`) REFERENCES `tipos_ocorrencia` (`id`)," +
                "KEY `natureza_fk_idx` (`id_natureza`)," +
                "CONSTRAINT `natureza_fk` FOREIGN KEY(`" +
                "id_natureza`) REFERENCES `naturezas` (`id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci", transaction: transaction);

            await connection.ExecuteAsync(
                "CREATE TABLE `pessoas_envolvidas` (" +
                "`id` char(36) NOT NULL, " +
                "`nome` varchar(45) NOT NULL," +
                "`envolvimento` varchar(45) NOT NULL," +
                "`sexo` char(1) NOT NULL," +
                "`cpf` varchar(11) DEFAULT NULL," +
                "`profissao` varchar(45) DEFAULT NULL," +
                "`gravidade_lesoes` varchar(45) DEFAULT NULL," +
                "`raca_cor` varchar(45) DEFAULT NULL," +
                "`id_ocorrencia` char(36) NOT NULL," +
                "`created` datetime NOT NULL," +
                "`created_by` varchar(45) NOT NULL," +
                "`last_modified` datetime DEFAULT NULL," +
                "`last_modified_by` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY(`id`)," +
                "KEY `ocorrencias_fk_idx` (`id_ocorrencia`)," +
                "CONSTRAINT `ocorrencias_fk` FOREIGN KEY(`" +
                "id_ocorrencia`) REFERENCES `ocorrencias` (`id`)" +
                "    ) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci",
                transaction: transaction);

            await connection.ExecuteAsync(
                "CREATE TABLE `unidades_moveis` (" +
                "`id` char(36) NOT NULL, " +
                "`matricula_responsavel` varchar(45) NOT NULL," +
                "`unidade_responsavel` varchar(45) NOT NULL," +
                "`orgao` varchar(10) NOT NULL," +
                "`prefixo_vtr` varchar(10) NOT NULL," +
                "`responsavel` varchar(45) NOT NULL," +
                "`id_ocorrencia` char(36) NOT NULL," +
                "`id_orgao` int NOT NULL," +
                "`created` datetime NOT NULL," +
                "`created_by` varchar(45) NOT NULL," +
                "`last_modified` datetime DEFAULT NULL," +
                "`last_modified_by` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY(`id`)," +
                "KEY `id_ocorrencia_idx` (`id_ocorrencia`)," +
                "CONSTRAINT `id_ocorrencia_um` FOREIGN KEY(`" +
                "id_ocorrencia`) REFERENCES `ocorrencias` (`id`)," +
                "KEY `orgao_fk_idx` (`id_orgao`)," +
                "CONSTRAINT `orgao_fk` FOREIGN KEY(`" +
                "id_orgao`) REFERENCES `orgaos` (`id`)" +
                "    ) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci",
                transaction: transaction);

            await connection.ExecuteAsync("INSERT INTO tipos_ocorrencia (id, descricao) VALUES (1, 'Flagrante')",
                transaction: transaction);
            await connection.ExecuteAsync(
                "INSERT INTO orgaos (id, descricao) VALUES (1, 'CBM'), (2, 'PM'), (3, 'DETRAN'), (4, 'PC'), (5, 'PF')",
                transaction: transaction);
            await connection.ExecuteAsync("INSERT INTO naturezas (id, descricao) VALUES (1, 'Criminal')",
                transaction: transaction);

            await connection.ExecuteAsync(
                "INSERT INTO ocorrencias (" +
                "id,identificador_ocorrencia,id_tipo,delegacia_policia_apuracao,id_natureza,data_hora_fato," +
                "data_hora_comunicacao,endereco_fato,praticado_por_menor,local_periciado,tipo_local," +
                "objeto_meio_empregado,created,created_by,last_modified_by,last_modified" +
                ") VALUES " +
                "   ('C43719CB-5765-CA45-76E5-FD9394F89D4A','2327 8057 8',0,'Etiam laoreet,',0,'2009-12-07 05:51:38','2020-12-21 13:36:08','quis, pede. Praesent eu dui. Cum sociis','0','0','Proin','consectetuer adipiscing','2020-12-31 00:21:55','Alexander','Wesley','2004-06-01 08:09:04')," +
                "   ('12076CA5-2DB6-7CCC-60C7-1926F0C3F732','5715 0286 1',0,'ipsum. Curabitur',0,'2010-03-29 02:52:25','2008-02-04 04:25:19','turpis egestas. Fusce aliquet magna a neque.','1','1','porttitor','lectus. Cum','2019-04-25 15:21:18','Jackson','Nero','2009-07-06 12:49:10')," +
                "   ('B9530A51-7B8F-6090-F49D-08C940FE3C02','9752 8090 9',0,'Integer in',0,'2007-10-11 00:57:59','2002-11-27 19:34:36','blandit viverra. Donec tempus, lorem fringilla ornare','0','1','Integer','blandit viverra.','2003-10-02 07:30:41','Nolan','Wesley','2009-03-07 16:58:56')," +
                "   ('C79B465F-CF52-4872-2811-6F04E79334C2','4564 0885 6',0,'Class aptent',0,'2012-07-26 03:23:59','2019-05-10 11:38:47','enim commodo hendrerit. Donec porttitor tellus non','1','1','urna','egestas a,','2012-12-07 14:58:45','Colby','Tyler','2011-04-11 11:34:46')," +
                "   ('1F4D4A45-6AC3-2975-1AD8-F66390288B90','3430 1198 0',0,'dolor. Nulla',0,'2006-05-09 05:15:19','2019-07-15 15:33:15','Etiam imperdiet dictum magna. Ut tincidunt orci','1','0','justo','vel arcu.','2006-08-20 08:54:30','Eleanor','Herman','2011-01-06 16:47:49')," +
                "   ('1041974B-830B-933A-71B7-8494B198AB12','1386 8711 5',0,'risus. Quisque',0,'2016-10-22 19:54:55','2013-10-30 21:45:22','ultricies adipiscing, enim mi tempor lorem, eget','0','0','elementum','fermentum vel,','2020-04-07 12:43:45','Kimberly','Gareth','2006-02-28 00:04:10')," +
                "   ('C8DB60AD-0B57-E764-AA65-D0092ED594A3','5860 6944 7',0,'sodales elit',0,'2005-06-26 17:07:17','2009-04-11 13:26:38','dolor sit amet, consectetuer adipiscing elit. Etiam','1','0','cursus,','velit. Sed','2006-04-25 20:32:11','Hope','Wing','2020-07-13 22:45:41')," +
                "   ('FA9374BE-C6A8-85FC-9C34-2EF099A73C44','2690 9095 5',0,'vel nisl.',0,'2017-12-12 03:21:46','2013-02-04 10:46:44','Etiam ligula tortor, dictum eu, placerat eget,','1','1','ultrices.','Aliquam erat','2002-11-13 16:38:40','Ross','Anne','2009-12-16 12:00:40')," +
                "   ('40FE6954-52F7-E597-EB5F-17E20B62211C','9661 2354 4',0,'Aenean massa.',0,'2011-12-24 04:44:14','2013-04-01 13:18:02','tincidunt orci quis lectus. Nullam suscipit, est','1','1','facilisi.','Curabitur egestas','2006-03-23 11:43:04','Sydnee','Kevyn','2015-08-06 23:06:12')," +
                "   ('6065D7B2-12D6-32FC-085D-A89D63CAE681','8669 4310 9',0,'elit erat',0,'2018-04-04 15:15:42','2002-08-22 10:39:34','Duis gravida. Praesent eu nulla at sem','0','1','id,','velit eu','2009-10-14 09:45:05','Ira','Wynne','2006-03-04 07:46:40')," +
                "   ('2F645CD5-8A3C-613D-9111-51DC3D7CA1CF','5826 1071 7',0,'ridiculus mus.',0,'2008-12-19 17:16:16','2008-12-12 19:37:45','Donec felis orci, adipiscing non, luctus sit','1','1','In','tellus sem','2003-12-07 07:52:11','Omar','Jerome','2015-10-10 22:36:32')," +
                "   ('14224FA4-140E-B56F-FEA3-2A47D97A8357','7647 2797 4',0,'Aliquam fringilla',0,'2004-10-09 01:45:53','2019-07-22 06:03:00','fames ac turpis egestas. Fusce aliquet magna','0','0','nisi.','tempor bibendum.','2011-12-28 04:13:53','Brody','Aspen','2007-10-13 03:50:37')," +
                "   ('D6339E48-E4A0-02C3-C960-034DB4C84CF4','5892 9014 4',0,'vel nisl.',0,'2019-02-06 16:50:41','2014-08-25 19:53:32','Donec tempus, lorem fringilla ornare placerat, orci','0','1','malesuada','nunc. In','2004-05-02 07:08:37','Malachi','Jessica','2011-01-31 23:37:59')," +
                "   ('4A47A356-4AA5-4C40-8BEC-1C28DCC3F4C0','7987 2179 2',0,'vel, mauris.',0,'2008-03-28 19:05:07','2014-02-28 19:39:33','dis parturient montes, nascetur ridiculus mus. Donec','1','0','augue','at, velit.','2019-09-04 10:05:59','Howard','Denton','2013-01-30 11:28:20')," +
                "   ('1BA84D97-D3A9-D75A-5BF3-D6186E9A3112','7388 9803 1',0,'habitant morbi',0,'2007-09-22 01:19:00','2014-09-14 07:11:31','facilisis lorem tristique aliquet. Phasellus fermentum convallis','0','1','velit.','parturient montes,','2015-11-20 14:22:42','Quemby','Ray','2007-01-09 15:16:26')," +
                "   ('603E51A7-B092-68DF-1F75-D577393BB02D','1709 3211 9',0,'commodo ipsum.',0,'2008-07-03 14:06:58','2005-05-07 17:02:02','sagittis. Nullam vitae diam. Proin dolor. Nulla','0','1','tempor','porttitor eros','2010-05-10 15:50:22','Hunter','Maxwell','2011-04-09 06:03:31')," +
                "   ('460C2100-6901-E997-7CE0-A02A56216460','4751 3653 1',0,'ullamcorper. Duis',0,'2014-12-30 15:22:21','2010-09-18 22:34:40','Integer in magna. Phasellus dolor elit, pellentesque','1','1','primis','tincidunt nibh.','2011-11-11 04:49:24','Quin','Alvin','2003-09-21 02:00:05')," +
                "   ('CF074E8D-9EDD-7A5B-CADB-0F5145CB863D','2239 9345 4',0,'Sed auctor',0,'2018-02-25 08:26:55','2001-04-25 19:45:40','tellus id nunc interdum feugiat. Sed nec','1','1','aliquam','Cras eu','2003-11-14 01:30:18','Isaac','Arsenio','2021-02-08 20:57:04')," +
                "   ('38AD3FF3-9172-7F2F-E4C0-790AE3E2900C','0561 6452 9',0,'dictum augue',0,'2005-11-03 16:49:49','2010-03-02 22:20:57','faucibus orci luctus et ultrices posuere cubilia','0','0','felis','Quisque nonummy','2007-07-14 22:02:11','Indira','Ferris','2018-04-01 07:18:41')," +
                "   ('724824CE-B59D-4A94-B115-23F6E0E69320','1886 1259 8',0,'suscipit nonummy.',0,'2017-06-28 23:48:26','2018-12-15 00:22:48','hendrerit. Donec porttitor tellus non magna. Nam','0','0','sit','Sed eu','2009-04-12 20:42:58','Carly','Nyssa','2002-08-23 22:36:21')," +
                "   ('4F2D87DA-E5DC-9BD3-956C-4FA61F1D72EA','5932 1258 4',0,'elit, a',0,'2002-04-13 09:37:41','2001-05-15 19:01:14','eget magna. Suspendisse tristique neque venenatis lacus.','1','1','tristique','condimentum. Donec','2016-11-19 09:39:58','Keaton','Karleigh','2001-09-15 01:20:59')," +
                "   ('AAAA5D55-1B92-9C70-9D97-50209DC26981','5684 9583 1',0,'Proin eget',0,'2012-12-10 18:46:33','2011-07-07 21:01:15','semper egestas, urna justo faucibus lectus, a','0','0','arcu','adipiscing. Mauris','2010-03-19 01:56:49','Ivan','Xenos','2009-04-08 23:35:39')," +
                "   ('69FFD2A8-7AB2-396C-7C14-A8204EC53918','9553 1112 3',0,'metus urna',0,'2019-09-20 00:22:12','2001-03-18 16:32:16','commodo ipsum. Suspendisse non leo. Vivamus nibh','1','0','at','eu elit.','2011-03-19 22:22:58','Patience','Alexandra','2005-02-22 10:36:51')," +
                "   ('1CB8E63E-1B42-0766-E410-8ACB9BE48607','3580 1832 1',0,'malesuada vel,',0,'2013-02-14 04:58:39','2011-11-07 12:24:24','rutrum lorem ac risus. Morbi metus. Vivamus','0','1','ut,','cursus luctus,','2014-09-05 05:32:39','Paloma','Glenna','2009-02-17 19:18:28')," +
                "   ('31F365ED-B23B-4BF9-7F39-C3F0E125749B','4863 0482 4',0,'semper rutrum.',0,'2002-10-10 19:54:06','2014-10-01 18:57:17','Aliquam auctor, velit eget laoreet posuere, enim','1','1','Etiam','id, ante.','2014-02-18 21:33:33','Gareth','Clare','2005-09-01 19:55:38')," +
                "   ('5816E3CF-9FAB-71AC-04A0-A2C68CE43F23','4510 0320 7',0,'cursus luctus,',0,'2017-04-23 16:07:24','2005-05-06 08:16:31','Sed molestie. Sed id risus quis diam','1','1','Quisque','Nullam velit','2011-07-31 10:06:06','Lacy','Hedy','2011-07-01 04:54:21')," +
                "   ('6D934B33-4E07-E123-826A-0C4EC00D60A3','6879 0686 0',0,'hendrerit. Donec',0,'2003-04-15 21:09:37','2019-06-23 19:08:49','nascetur ridiculus mus. Proin vel nisl. Quisque','0','0','nibh','ut eros','2010-07-11 15:45:23','Jermaine','Angelica','2007-06-18 00:25:39')," +
                "   ('EFC4497C-69BC-5ADA-F684-7501F043BED1','2709 6150 9',0,'elit, a',0,'2009-06-15 07:00:20','2016-05-27 16:17:04','Suspendisse aliquet molestie tellus. Aenean egestas hendrerit','1','1','Curae;','quis diam','2001-08-01 04:12:10','Kaseem','Isaiah','2021-02-22 21:46:23')," +
                "   ('7C60154D-ACD0-447C-0C4C-DD6B11DB5987','5493 8574 6',0,'a tortor.',0,'2017-10-06 18:39:59','2014-10-12 04:19:01','lectus. Cum sociis natoque penatibus et magnis','1','0','ut','Fusce mi','2009-02-09 19:01:30','Micah','Brenna','2003-12-12 14:22:30')," +
                "   ('FE2EB822-3FFE-ECD8-AC7B-9F68F2613CE7','7823 4845 0',0,'odio a',0,'2020-04-13 15:41:43','2014-11-02 02:01:59','vulputate ullamcorper magna. Sed eu eros. Nam','1','0','nibh','velit. Sed','2013-06-18 14:25:45','Kelly','Walker','2018-09-15 13:57:25')," +
                "   ('EEE37D3D-A61B-A4CB-296A-063EC9289CDF','1835 0505 7',0,'hendrerit. Donec',0,'2006-09-25 02:39:32','2010-02-01 13:29:51','consectetuer adipiscing elit. Etiam laoreet, libero et','1','1','ultricies','posuere vulputate,','2016-12-01 11:39:30','Stewart','Ira','2007-03-03 14:00:58')," +
                "   ('5910F11F-0B1D-C15E-F6E3-CFA4CCA9873D','5857 5042 9',0,'habitant morbi',0,'2009-01-15 13:06:04','2008-06-19 02:49:19','lorem ut aliquam iaculis, lacus pede sagittis','0','0','massa.','Sed diam','2013-05-15 19:06:01','Silas','Melvin','2018-01-04 20:56:49')," +
                "   ('37153E45-161B-6FE8-DD15-75272D91EA4C','8107 9442 6',0,'Fusce aliquet',0,'2010-04-23 11:27:28','2011-11-24 18:36:21','Phasellus dolor elit, pellentesque a, facilisis non,','1','1','ac','Vestibulum ante','2019-03-16 10:10:59','Wade','Brady','2003-08-17 19:21:47')," +
                "   ('D8E3F3D2-EA80-B4DF-07B1-7C0428352EE8','3115 8270 7',0,'tincidunt congue',0,'2004-04-14 01:14:54','2007-03-18 01:07:05','Morbi vehicula. Pellentesque tincidunt tempus risus. Donec','1','0','consequat','nunc sed','2021-03-04 14:50:53','Nelle','Geoffrey','2008-07-15 03:32:57')," +
                "   ('E8F84F78-0F7F-1A34-FDEB-17777D0598FD','5758 1177 9',0,'sagittis. Duis',0,'2015-11-15 20:18:05','2017-08-14 03:22:16','ornare tortor at risus. Nunc ac sem','1','0','mauris.','venenatis a,','2002-12-17 06:25:02','Ciara','Upton','2006-01-06 04:17:22')," +
                "   ('010CA91B-481E-6535-F1B7-DE93A5D65B86','4000 0776 9',0,'ornare, facilisis',0,'2020-12-08 14:21:17','2014-02-03 18:25:49','nascetur ridiculus mus. Donec dignissim magna a','0','0','In','tellus. Suspendisse','2019-11-08 07:10:12','Warren','Hillary','2012-04-05 11:52:23')," +
                "   ('1F8562C6-746E-B976-05CC-1BAB3D56FBAA','3089 3114 2',0,'Nulla dignissim.',0,'2006-06-13 20:48:05','2015-02-11 19:10:43','rutrum urna, nec luctus felis purus ac','0','0','Nullam','tincidunt orci','2014-03-22 10:27:45','Charity','Justine','2007-01-08 16:49:09')," +
                "   ('34AE10C8-4192-CD5E-6922-9D5C806B0F1A','9862 3036 2',0,'Curabitur massa.',0,'2018-06-10 00:35:02','2013-08-18 17:17:43','orci, in consequat enim diam vel arcu.','1','1','ligula.','lectus convallis','2017-10-12 01:14:27','Rowan','Geoffrey','2008-10-26 23:13:47')," +
                "   ('59E2AC70-8217-458B-A8B4-944E7A3093FA','7651 2761 4',0,'Pellentesque ut',0,'2006-06-23 01:08:51','2004-03-04 07:11:17','egestas. Fusce aliquet magna a neque. Nullam','0','1','magna.','at lacus.','2009-05-14 11:26:23','Evan','Jasper','2001-04-19 20:56:40')," +
                "   ('A8AF13A3-FD6B-A60E-1930-2FEA527C3BDB','1798 0757 9',0,'Mauris quis',0,'2005-06-26 14:31:33','2016-08-25 17:49:23','Donec tempus, lorem fringilla ornare placerat, orci','1','0','aliquet','eu, euismod','2017-09-27 10:29:38','Cade','Chantale','2009-01-16 06:42:18')," +
                "   ('E4231958-3442-2EFF-7B43-805D88EAA54F','6670 1143 7',0,'odio. Phasellus',0,'2003-03-13 04:28:39','2012-05-25 04:44:08','sed leo. Cras vehicula aliquet libero. Integer','1','1','velit.','fermentum convallis','2013-06-01 09:13:52','Bert','Gillian','2010-07-21 17:13:37')," +
                "   ('5EC0DD48-F89F-7CF3-947F-8512AD162EBD','4482 6156 9',0,'leo. Morbi',0,'2016-01-10 18:11:22','2018-05-06 20:06:15','lorem tristique aliquet. Phasellus fermentum convallis ligula.','1','0','Nulla','aliquet libero.','2014-03-01 08:51:17','Aileen','Dorian','2002-05-16 18:43:13')," +
                "   ('E5854D05-FF19-0921-DEA9-F131B697D8DF','9500 8079 1',0,'tristique ac,',0,'2004-11-23 18:00:33','2006-10-11 10:32:58','ac mattis semper, dui lectus rutrum urna,','1','0','ac,','vulputate, nisi','2002-12-11 02:08:19','Dorian','Vernon','2012-06-07 09:55:15')," +
                "   ('962BF27D-355A-655E-F797-F1CDDA293DB6','2041 8622 2',0,'tempor, est',0,'2009-03-11 02:51:08','2016-07-11 20:41:37','non leo. Vivamus nibh dolor, nonummy ac,','0','1','Proin','non arcu.','2003-01-04 09:49:40','Elliott','Baxter','2018-08-29 16:20:32')," +
                "   ('4C3D9036-3182-7C6B-4EAF-C11DBFF2E093','5337 2745 7',0,'urna. Nunc',0,'2016-02-21 03:48:13','2016-04-04 14:13:39','odio. Nam interdum enim non nisi. Aenean','1','1','dui.','amet, risus.','2011-01-24 10:12:25','Whoopi','Magee','2021-02-18 03:53:32')," +
                "   ('A23340AE-615A-AF21-32A4-267E7910989E','4296 1081 3',0,'lacinia orci,',0,'2015-03-19 05:44:42','2014-02-13 02:53:51','Maecenas mi felis, adipiscing fringilla, porttitor vulputate,','1','1','feugiat.','facilisis, magna','2015-06-30 23:51:12','Felix','Chaim','2003-01-24 23:52:50')," +
                "   ('53AC8682-55B0-B944-9BBD-13647D5286F7','4192 1078 7',0,'Nunc sollicitudin',0,'2006-07-20 00:43:47','2011-12-02 21:05:32','mauris, rhoncus id, mollis nec, cursus a,','1','0','nibh','Nunc sed','2014-06-05 20:16:23','Jaden','Darryl','2003-12-04 01:37:57')," +
                "   ('4002EDE5-3055-59F8-2C91-03DB93BF0D6B','7969 9584 9',0,'lobortis, nisi',0,'2010-11-10 10:10:52','2017-06-19 03:21:05','dictum augue malesuada malesuada. Integer id magna','1','1','erat','senectus et','2012-11-23 12:04:12','Carl','Alec','2004-12-22 12:47:50')," +
                "   ('AC5A7B50-073D-70DE-6522-50FFC4FD3D0E','8245 7596 9',0,'vel est',0,'2002-10-09 15:58:34','2020-03-18 01:17:41','faucibus leo, in lobortis tellus justo sit','0','1','felis','purus. Maecenas','2001-08-26 07:09:13','Ursa','Burton','2015-03-23 10:38:19')," +
                "   ('72B41435-2248-28AF-930F-8EC5908B7049','9007 6008 9',0,'erat. Vivamus',0,'2017-09-27 11:02:06','2015-05-22 16:47:19','Suspendisse tristique neque venenatis lacus. Etiam bibendum','0','1','sed','ac turpis','2018-03-15 12:18:56','Axel','Tad','2003-09-25 14:04:53')," +
                "   ('5B7A7593-9867-1AE1-24C1-21F18C9EB7CE','6250 0834 2',0,'nec, leo.',0,'2009-04-24 14:29:41','2011-01-13 05:50:31','ante dictum mi, ac mattis velit justo','1','0','nunc.','Fusce dolor','2012-10-18 22:08:48','Adrienne','Cullen','2014-02-01 13:52:14')," +
                "   ('BE6221C5-0177-4734-7F96-E98E1F7E40FE','9134 0302 3',0,'luctus et',0,'2010-01-28 04:25:55','2014-08-01 16:41:15','et, lacinia vitae, sodales at, velit. Pellentesque','0','1','pede,','libero. Proin','2015-11-26 11:52:33','Hyatt','Vielka','2013-03-24 23:49:58')," +
                "   ('5D898B34-9143-7D24-2A66-F44E8231D5E5','9698 8844 3',0,'pretium aliquet,',0,'2015-12-27 00:04:57','2006-12-25 14:31:30','ante blandit viverra. Donec tempus, lorem fringilla','0','0','elit','ullamcorper, velit','2016-11-22 00:01:02','Lois','Patience','2017-03-06 09:38:57')," +
                "   ('2DFBE23B-B9F9-342B-F651-9F3086BDB32A','3706 5235 6',0,'justo sit',0,'2013-07-31 12:27:54','2002-12-19 02:21:18','Pellentesque ultricies dignissim lacus. Aliquam rutrum lorem','1','1','sit','eget varius','2010-01-25 13:23:21','Audrey','Carson','2008-04-10 03:23:43')," +
                "   ('FB9B448E-FB6F-904B-AB3F-D733BCD8BF8E','2884 2534 0',0,'nibh lacinia',0,'2013-05-21 23:17:00','2020-09-09 18:19:19','Ut sagittis lobortis mauris. Suspendisse aliquet molestie','0','0','non','Nullam ut','2007-07-29 13:24:58','Dennis','Lael','2014-01-13 00:32:30')," +
                "   ('98E321E7-F6BC-6084-CC75-9608B036457C','3250 9041 8',0,'Morbi sit',0,'2005-05-10 01:38:19','2013-02-11 20:56:11','gravida molestie arcu. Sed eu nibh vulputate','1','0','lectus','sapien, gravida','2013-10-22 02:05:43','Raymond','Barclay','2019-08-18 12:08:47')," +
                "   ('1F657F67-1B6C-EDB6-8E90-B801F4C9CC80','8400 8302 5',0,'nec, euismod',0,'2002-04-05 19:11:06','2005-06-15 00:05:13','tortor, dictum eu, placerat eget, venenatis a,','0','1','sit','nec luctus','2010-03-25 19:14:42','Stacey','Maris','2010-11-18 18:09:35')," +
                "   ('022C3B44-FCAC-FF85-E049-5C3F3C9CC712','7686 2932 6',0,'erat vel',0,'2016-03-05 16:41:34','2001-03-19 02:17:05','Integer sem elit, pharetra ut, pharetra sed,','0','0','tristique','eget metus.','2009-04-16 07:53:46','Dora','Kyla','2004-02-02 07:21:19')," +
                "   ('3D89F674-BD09-3ECC-6EA4-17BBDF1153F0','4354 9295 0',0,'ligula consectetuer',0,'2019-02-03 09:57:30','2013-06-07 11:46:27','aliquet lobortis, nisi nibh lacinia orci, consectetuer','0','1','nulla','ut, pharetra','2017-03-04 10:44:01','Bree','Cain','2006-10-27 19:04:29')," +
                "   ('3D6F1E4F-EB45-0BCB-FDA0-3CAB8BC6232F','3443 4862 9',0,'vitae erat',0,'2013-11-05 05:03:05','2006-02-07 05:16:09','et ipsum cursus vestibulum. Mauris magna. Duis','1','1','lorem','molestie tortor','2005-07-02 12:25:19','Ashely','Dora','2016-02-29 03:20:34')," +
                "   ('B1CC993C-DDD2-3415-82D4-2F9A87B25C04','2771 4955 6',0,'blandit mattis.',0,'2013-03-11 17:12:03','2008-02-26 12:44:36','luctus vulputate, nisi sem semper erat, in','1','0','eros.','rutrum urna,','2019-12-22 05:27:23','Cade','Connor','2009-03-24 19:34:56')," +
                "   ('93AC4F48-2283-E9DE-446F-EABB64642CC6','6037 6340 9',0,'Proin vel',0,'2019-02-13 09:41:25','2016-01-23 07:54:48','libero at auctor ullamcorper, nisl arcu iaculis','1','0','parturient','Nam tempor','2003-08-05 05:43:24','Keegan','Ciaran','2007-04-02 00:18:41')," +
                "   ('834F9EFB-4BCB-3FED-2BCB-0DD231EAB681','9536 8612 0',0,'arcu. Morbi',0,'2008-11-29 05:26:29','2017-10-10 17:43:17','nulla. In tincidunt congue turpis. In condimentum.','1','0','tincidunt','a feugiat','2007-12-01 11:06:19','Cailin','Isaiah','2016-08-01 18:03:24')," +
                "   ('580D67FA-7C6D-EA4C-D68A-E707341CE779','7679 3867 1',0,'Morbi neque',0,'2006-02-20 14:13:23','2010-03-30 23:57:27','Pellentesque ultricies dignissim lacus. Aliquam rutrum lorem','0','0','sed','consequat, lectus','2009-08-14 19:26:45','Leila','Phillip','2016-02-22 17:51:08')," +
                "   ('6305F20A-4928-92F9-640C-BCEB7A3EA1FB','9179 9071 9',0,'ac libero',0,'2019-04-23 03:18:18','2014-11-18 08:55:53','Sed eu eros. Nam consequat dolor vitae','1','0','amet','est. Nunc','2020-09-23 22:46:46','Ian','Helen','2017-01-10 16:19:43')," +
                "   ('3997604D-CDC9-D092-66ED-1A5A1C7C8B79','0573 4885 6',0,'mollis nec,',0,'2012-12-11 18:27:43','2019-01-15 01:08:26','elementum, dui quis accumsan convallis, ante lectus','1','0','cursus','In at','2017-05-04 18:45:24','Zeph','Aurelia','2013-07-31 16:50:03')," +
                "   ('AEC1440C-A2DF-6EF5-DE3C-4CB02FB3E894','0381 7736 6',0,'Duis dignissim',0,'2007-01-22 08:30:20','2008-07-22 03:49:56','consectetuer adipiscing elit. Etiam laoreet, libero et','1','0','mi.','auctor velit.','2004-08-09 22:02:25','Alden','Harlan','2009-12-09 14:32:06')," +
                "   ('BC26D06A-3C4D-63BB-5159-C0DFF2139866','2272 3391 1',0,'felis, adipiscing',0,'2020-12-18 20:12:21','2006-04-13 16:52:15','turpis nec mauris blandit mattis. Cras eget','0','0','tristique','nec, imperdiet','2007-03-03 09:34:32','Suki','Tatum','2008-05-22 18:43:02')," +
                "   ('FFEC85E4-6EC0-D7A8-71F5-3699BDE4F4B7','9476 6856 9',0,'et, commodo',0,'2012-09-27 13:47:32','2011-08-08 02:18:02','rutrum, justo. Praesent luctus. Curabitur egestas nunc','1','1','montes,','et malesuada','2019-03-24 15:05:56','Quin','Harrison','2016-07-04 15:57:26')," +
                "   ('283B955F-2B05-1277-B298-307FEB7A2C9D','6876 5420 2',0,'eleifend vitae,',0,'2009-07-14 17:15:42','2019-10-01 18:37:37','lacinia. Sed congue, elit sed consequat auctor,','1','0','Fusce','fermentum vel,','2013-08-07 10:55:25','Reese','Xyla','2008-05-07 14:01:00')," +
                "   ('25DB2D57-7FEA-CFF7-9082-613B9AD87B08','1540 1832 2',0,'fringilla est.',0,'2013-12-08 05:23:34','2005-12-17 14:23:13','Integer urna. Vivamus molestie dapibus ligula. Aliquam','1','0','consequat','aliquam arcu.','2001-11-15 23:00:55','Hadley','Bradley','2002-12-04 22:31:46')," +
                "   ('3E8FD4F3-E675-268C-7C36-046A2C14B3A0','9210 8723 7',0,'est. Nunc',0,'2015-04-28 16:29:16','2020-07-05 04:38:02','Nam porttitor scelerisque neque. Nullam nisl. Maecenas','0','0','non','Curabitur consequat,','2004-09-23 17:11:36','Olga','Ira','2005-09-03 19:15:42')," +
                "   ('EB53C1ED-FEFE-8CDD-582D-F6A5AC613D93','4061 5619 4',0,'non, luctus',0,'2014-09-07 08:09:13','2017-07-20 12:58:16','volutpat. Nulla facilisis. Suspendisse commodo tincidunt nibh.','0','0','est','ultricies ligula.','2005-11-20 16:21:20','Gwendolyn','Gloria','2017-11-11 12:41:35')," +
                "   ('D4EE178F-1545-89DB-1130-C0DDFB580562','4780 5459 8',0,'ac, fermentum',0,'2004-06-21 11:52:25','2004-01-24 12:28:18','et magnis dis parturient montes, nascetur ridiculus','0','1','Integer','Praesent eu','2020-06-02 19:04:59','Quinlan','Montana','2005-07-31 19:48:20')," +
                "   ('5B849B05-FD11-ED98-8328-67BBBB74FA8D','0758 4535 4',0,'orci. Donec',0,'2013-01-31 18:23:17','2001-01-25 15:30:29','egestas lacinia. Sed congue, elit sed consequat','1','0','pellentesque','nibh. Phasellus','2020-11-10 03:45:26','Graham','Ryan','2020-08-31 09:12:46')," +
                "   ('15E32B10-84D8-2A99-9BF7-35105DC4E3E0','0488 4487 3',0,'egestas. Sed',0,'2005-07-19 21:41:27','2013-03-19 05:23:20','parturient montes, nascetur ridiculus mus. Donec dignissim','1','1','ultrices.','accumsan sed,','2009-01-20 01:52:21','Gavin','Jakeem','2001-02-06 17:34:10')," +
                "   ('76E7A940-8F6C-0512-6018-3F6335A94EA9','5187 7842 3',0,'aliquet nec,',0,'2009-07-07 10:33:19','2014-08-17 01:08:46','In mi pede, nonummy ut, molestie in,','1','0','eu','nisl. Maecenas','2014-01-21 20:32:30','Nevada','Emery','2020-12-18 20:56:16')," +
                "   ('22AAAAAE-20A0-1CAB-BF69-D96E993B07FB','6195 7176 2',0,'pede. Nunc',0,'2005-03-01 22:14:12','2017-03-31 18:22:21','faucibus id, libero. Donec consectetuer mauris id','0','0','porttitor','feugiat non,','2017-05-31 11:31:49','Abraham','Amir','2007-09-03 20:00:50')," +
                "   ('2268B955-9F98-8CE4-4ED7-ED2EDF3CA4BE','7251 2721 9',0,'aliquam iaculis,',0,'2004-10-25 11:50:30','2013-10-07 16:20:30','Proin non massa non ante bibendum ullamcorper.','1','1','at','lobortis ultrices.','2008-04-09 21:25:01','Laith','Kyra','2019-09-15 17:24:24')," +
                "   ('07618A8E-49D5-6102-A081-CD55929C76AB','0814 3019 5',0,'auctor ullamcorper,',0,'2021-02-25 08:47:23','2018-04-30 06:45:37','sodales. Mauris blandit enim consequat purus. Maecenas','1','1','semper','dictum placerat,','2018-03-11 17:48:29','Tiger','Quentin','2012-06-11 23:58:13')," +
                "   ('4ACB7D5D-D22C-3E33-5782-8D3827A588F0','6879 4841 5',0,'dui. Suspendisse',0,'2014-01-17 04:23:57','2002-02-20 04:08:30','enim commodo hendrerit. Donec porttitor tellus non','0','1','elementum','Vivamus sit','2008-08-29 02:23:50','Chaim','Hector','2020-02-05 05:27:51')," +
                "   ('C953FC78-E85E-5BDA-20EA-AE415E8F8B89','8355 8497 2',0,'orci lacus',0,'2009-09-15 01:03:11','2014-12-03 11:37:03','pede sagittis augue, eu tempor erat neque','1','1','at,','turpis nec','2017-12-20 15:01:38','Pascale','Mariko','2005-11-07 07:20:46')," +
                "   ('1E2BDAF9-7780-0CC7-1D40-9E5EF81CBC29','3676 2704 8',0,'sem. Nulla',0,'2007-02-08 06:09:33','2003-06-08 23:13:30','sagittis augue, eu tempor erat neque non','0','1','est','Pellentesque habitant','2013-04-02 08:11:00','Marah','Gil','2010-07-29 22:55:24')," +
                "   ('5A622923-0266-9777-7A16-B5ABDBE0F38D','3908 1454 3',0,'vitae velit',0,'2017-09-14 17:44:44','2019-11-17 03:23:56','odio sagittis semper. Nam tempor diam dictum','0','1','dolor,','magnis dis','2011-11-17 01:44:50','Paul','Garrison','2001-06-01 22:51:11')," +
                "   ('A8FEBE5C-88A1-AB91-ED9B-D6BC2FDC891C','1671 4880 3',0,'Proin nisl',0,'2009-02-28 20:26:01','2016-03-20 18:44:19','cursus. Nunc mauris elit, dictum eu, eleifend','1','0','erat,','purus. Maecenas','2017-10-18 22:01:38','Cherokee','Howard','2014-09-28 20:36:19')," +
                "   ('DB9709AE-E967-0901-40E5-01F04118E6FE','0733 1770 8',0,'nunc, ullamcorper',0,'2012-03-14 22:40:23','2002-10-10 01:31:01','ac tellus. Suspendisse sed dolor. Fusce mi','1','0','inceptos','euismod in,','2008-12-23 12:34:00','Lani','Laura','2001-06-07 06:56:03')," +
                "   ('5897294C-D84F-753E-7831-16BC5EEB4438','8835 8151 4',0,'magna sed',0,'2003-12-13 23:13:42','2014-07-29 16:14:15','Nunc ullamcorper, velit in aliquet lobortis, nisi','1','1','sed','ullamcorper. Duis','2004-02-13 09:32:44','Rigel','Hamish','2016-12-30 07:05:53')," +
                "   ('73B79785-88C2-0BDB-D97D-0C49E4F56A4C','1000 9529 1',0,'Morbi neque',0,'2009-10-07 00:43:37','2020-11-28 15:36:55','sapien imperdiet ornare. In faucibus. Morbi vehicula.','1','0','nisl','ante ipsum','2005-10-27 04:31:40','Thaddeus','Katell','2019-12-20 18:27:28')," +
                "   ('D532A9FB-F1E9-4FB8-85B8-A2730A31920F','6598 7226 9',0,'amet lorem',0,'2002-06-22 16:17:35','2020-02-16 10:51:28','Nunc commodo auctor velit. Aliquam nisl. Nulla','1','0','mi','tincidunt adipiscing.','2001-03-31 09:08:59','Kylynn','Arsenio','2011-02-06 16:33:12')," +
                "   ('22BC96BC-00E9-F3C7-9D74-2159C354ECC2','1623 3546 4',0,'velit. Aliquam',0,'2011-09-27 17:38:54','2014-04-03 19:08:52','Donec egestas. Duis ac arcu. Nunc mauris.','1','0','aliquet','a, enim.','2002-11-25 03:29:33','Galvin','Troy','2013-06-04 03:31:26')," +
                "   ('B7313255-805A-4E3A-9F95-B4C31C21B4B6','6002 8433 5',0,'Sed neque.',0,'2004-07-25 05:41:27','2020-04-07 19:15:07','lacus. Quisque purus sapien, gravida non, sollicitudin','0','0','porttitor','arcu vel','2008-09-07 02:58:04','Mohammad','Idona','2020-11-26 12:32:50')," +
                "   ('68A23C91-6C25-1075-F23D-D3AB302D00EC','9379 7070 1',0,'ut nisi',0,'2007-06-02 10:32:40','2019-10-13 04:19:02','erat nonummy ultricies ornare, elit elit fermentum','1','0','sit','dolor. Quisque','2012-06-11 03:08:00','Camden','Laurel','2018-04-08 14:03:24')," +
                "   ('8447F079-C627-5C2F-FAB3-25AD3BBA4AF4','7243 5249 8',0,'posuere at,',0,'2013-11-04 14:15:32','2015-04-25 10:21:56','dapibus quam quis diam. Pellentesque habitant morbi','1','1','ante.','velit egestas','2017-06-25 08:33:54','Kitra','Dorian','2003-02-24 04:11:34')," +
                "   ('A0A88E25-95DE-AD45-A52A-2B942F720A17','4938 9151 0',0,'sagittis placerat.',0,'2011-07-10 15:10:20','2002-09-04 02:42:24','lacinia vitae, sodales at, velit. Pellentesque ultricies','0','1','ultrices','molestie orci','2003-03-10 20:34:47','Hasad','Jakeem','2015-04-28 02:21:53')," +
                "   ('811C1BD5-06CA-36E5-1E86-4CE95A04A132','9564 0237 0',0,'ante lectus',0,'2017-11-02 22:04:52','2013-01-08 03:42:35','ligula consectetuer rhoncus. Nullam velit dui, semper','1','1','cursus','arcu et','2003-09-26 06:57:31','Acton','Lawrence','2013-09-08 15:32:58')," +
                "   ('3E2AB5F3-E5AF-1B00-25B3-40C02AE7D855','7919 3263 8',0,'nunc id',0,'2013-04-27 06:31:42','2016-02-05 15:35:53','Nunc sed orci lobortis augue scelerisque mollis.','0','1','id','ut eros','2015-10-05 21:35:28','Jade','Uriel','2011-10-01 22:20:18')," +
                "   ('36A2BD5B-6F0C-031A-BEC6-48A72D3B0C33','7832 9781 6',0,'ut odio',0,'2017-04-26 04:21:25','2016-07-20 14:40:28','ipsum cursus vestibulum. Mauris magna. Duis dignissim','0','1','interdum','nec tellus.','2011-04-03 13:30:41','Imani','Dylan','2017-12-28 09:29:30')," +
                "   ('07C6FB8E-ED6C-CDBA-B452-BEC7322B1C6F','5481 8921 8',0,'Sed nunc',0,'2007-07-31 23:02:57','2005-05-05 11:45:54','elit, pharetra ut, pharetra sed, hendrerit a,','0','0','mi.','dictum eu,','2014-05-02 01:16:36','Dieter','Amaya','2013-05-04 07:27:06')," +
                "   ('DDFA6D05-D057-F1C8-A4A4-77CD6454C362','1739 6061 4',0,'Sed diam',0,'2019-03-12 10:27:47','2013-12-07 17:54:22','Nunc mauris elit, dictum eu, eleifend nec,','0','0','amet','metus. Aliquam','2011-07-18 03:08:47','Shelby','Ahmed','2008-07-19 20:41:01')," +
                "   ('1A68D24A-6811-86EF-7D64-A0628946CD32','6209 3056 6',0,'vestibulum. Mauris',0,'2012-08-01 01:35:56','2010-09-06 01:22:32','nulla. Integer urna. Vivamus molestie dapibus ligula.','0','1','neque.','dolor. Nulla','2008-12-24 01:22:34','Trevor','Audrey','2013-01-03 07:22:29');"
                , transaction: transaction);

            transaction.Commit();
        }
    }
}