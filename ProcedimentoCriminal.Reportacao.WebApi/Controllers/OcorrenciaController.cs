using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchCategoriasVeiculo;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchEnvolvimentos;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchEstadosCivis;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchGrausInstrucao;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchMeiosEmpregados;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchNaturezas;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchNaturezasAcidente;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchOcorrenciaById;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchTiposObjeto;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchTiposVeiculo;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchUfs;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FilterOcorrencias;

namespace ProcedimentoCriminal.Reportacao.WebApi.Controllers
{
    public class OcorrenciaController : ApiControllerBase
    {
        [HttpPost("ocorrencia/filtro")]
        public async Task<ActionResult> FilterOcorrencias(FilterOcorrenciasQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        
        [HttpGet("ocorrencia/{id}")]
        public async Task<ActionResult> FetchOcorrenciaById(Guid id)
        {
            return Ok(await Mediator.Send(new FetchOcorrenciaByIdQuery {Id = id}));
        }

        [HttpGet("categoriasveiculo")]
        public async Task<ActionResult> FetchCategoriasVeiculo()
        {
            return Ok(await Mediator.Send(new FetchCategoriasVeiculoQuery()));
        }

        [HttpGet("envolvimentos")]
        public async Task<ActionResult> FetchEnvolvimentos()
        {
            return Ok(await Mediator.Send(new FetchEnvolvimentosQuery()));
        }

        [HttpGet("estadoscivis")]
        public async Task<ActionResult> FetchEstadosCivis()
        {
            return Ok(await Mediator.Send(new FetchEstadosCivisQuery()));
        }
        
        [HttpGet("grausinstrucao")]
        public async Task<ActionResult> FetchGrausIntrucao()
        {
            return Ok(await Mediator.Send(new FetchGrausInstrucaoQuery()));
        }
        
        [HttpGet("meiosempregados")]
        public async Task<ActionResult> FetchMeiosEmpregados()
        {
            return Ok(await Mediator.Send(new FetchMeiosEmpregadosQuery()));
        }
        
        [HttpGet("naturezas")]
        public async Task<ActionResult> FetchNaturezas()
        {
            return Ok(await Mediator.Send(new FetchNaturezasQuery()));
        }
        
        [HttpGet("naturezasacidente")]
        public async Task<ActionResult> FetchNaturezasAcidente()
        {
            return Ok(await Mediator.Send(new FetchNaturezasAcidenteQuery()));
        }
        
        [HttpGet("tiposobjeto")]
        public async Task<ActionResult> FetchTiposObjeto()
        {
            return Ok(await Mediator.Send(new FetchTiposObjetoQuery()));
        }
        
        [HttpGet("tiposveiculo")]
        public async Task<ActionResult> FetchTiposVeiculo()
        {
            return Ok(await Mediator.Send(new FetchTiposVeiculoQuery()));
        }
        
        [HttpGet("ufs")]
        public async Task<ActionResult> FetchUfs()
        {
            return Ok(await Mediator.Send(new FetchUfsQuery()));
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegistrarOcorrenciaCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}