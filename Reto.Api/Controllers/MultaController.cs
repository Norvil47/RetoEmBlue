using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reto.Dto.Response;
using Reto.Infraestructura.Sensor;
using Reto.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultaController : baseController
    {
        [HttpPost]
        [Route("LevantarMulta")]
        public async Task<MessageJson<MultaResponse>> GuardarOrden([FromQuery] string matricula, [FromBody] RegistrarMulta.Request obj)
        {
            obj.matricula = matricula;
            return await _mediator.Send(obj);
        }
        [HttpGet]        
        public async Task<MultaDataListResponse> BuscarMultasBatch([FromQuery] string matricula)
        {
            
            return await _mediator.Send(new BuscarMultas.Request() { matricula=matricula});
        }
    }
}
