using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.DeletarOcorrencia;
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
        public async Task<ActionResult> FilterOcorrencias(FilterOcorrenciasQuery query) => Ok(await Mediator.Send(query));

        [HttpGet("ocorrencia/{id}")]
        public async Task<ActionResult> FetchOcorrenciaById(Guid id) => Ok(await Mediator.Send(new FetchOcorrenciaByIdQuery {Id = id}));

        [HttpGet("categoriasveiculo")]
        public async Task<ActionResult> FetchCategoriasVeiculo() => Ok(await Mediator.Send(new FetchCategoriasVeiculoQuery()));

        [HttpGet("envolvimentos")]
        public async Task<ActionResult> FetchEnvolvimentos() => Ok(await Mediator.Send(new FetchEnvolvimentosQuery()));

        [HttpGet("estadoscivis")]
        public async Task<ActionResult> FetchEstadosCivis() => Ok(await Mediator.Send(new FetchEstadosCivisQuery()));

        [HttpGet("grausinstrucao")]
        public async Task<ActionResult> FetchGrausIntrucao() => Ok(await Mediator.Send(new FetchGrausInstrucaoQuery()));

        [HttpGet("meiosempregados")]
        public async Task<ActionResult> FetchMeiosEmpregados() => Ok(await Mediator.Send(new FetchMeiosEmpregadosQuery()));

        [HttpGet("naturezas")]
        public async Task<ActionResult> FetchNaturezas() => Ok(await Mediator.Send(new FetchNaturezasQuery()));

        [HttpGet("naturezasacidente")]
        public async Task<ActionResult> FetchNaturezasAcidente() => Ok(await Mediator.Send(new FetchNaturezasAcidenteQuery()));

        [HttpGet("tiposobjeto")]
        public async Task<ActionResult> FetchTiposObjeto() => Ok(await Mediator.Send(new FetchTiposObjetoQuery()));

        [HttpGet("tiposveiculo")]
        public async Task<ActionResult> FetchTiposVeiculo() => Ok(await Mediator.Send(new FetchTiposVeiculoQuery()));

        [HttpGet("ufs")]
        public async Task<ActionResult> FetchUfs() => Ok(await Mediator.Send(new FetchUfsQuery()));

        [HttpPost]
        public async Task<ActionResult> Create(RegistrarOcorrenciaCommand command)
        {
            var id = await Mediator.Send(command);
            return CreatedAtAction(nameof(Create), id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Create(Guid id)
        {
            await Mediator.Send(new DeletarOcorrenciaCommand {Id = id});
            return NoContent();
        }
    }
}