using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchNaturezas;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchOcorrenciaById;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchOrgaos;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchTipos;
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

        [HttpGet("naturezas")]
        public async Task<ActionResult> FetchNaturezas()
        {
            return Ok(await Mediator.Send(new FetchNaturezasQuery()));
        }

        [HttpGet("orgaos")]
        public async Task<ActionResult> FetchOrgaos()
        {
            return Ok(await Mediator.Send(new FetchOrgaosQuery()));
        }

        [HttpGet("tipos")]
        public async Task<ActionResult> FetchTipos()
        {
            return Ok(await Mediator.Send(new FetchTiposQuery()));
        }

        [HttpPost]
        public async Task<ActionResult> Create(AbrirOcorrenciaCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}