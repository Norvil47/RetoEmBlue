using MediatR;
using Microsoft.EntityFrameworkCore;
using Reto.Dto.Response;
using Reto.Entidades;
using Reto.Persistencia;
using Reto.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reto.Infraestructura.Sensor
{
    public class BuscarMultas
    {
        public class Request : IRequest<MultaDataListResponse>
        {
            public string matricula { get; set; }
        }

        public class CommandHandle : IRequestHandler<Request, MultaDataListResponse>
        {

            private readonly ModeloContext db;

            public CommandHandle(ModeloContext context)
            {
                db = context;


            }


            async Task<MultaDataListResponse> IRequestHandler<Request, MultaDataListResponse>.Handle(Request request, CancellationToken cancellationToken)
            {
                var response = new MultaDataListResponse();
                var sensor = new List<DataSensor>();
                if (request.matricula is not null || request.matricula is not "")
                    sensor = await db.dataSensor.Where(x => x.matricula == request.matricula).ToListAsync();
                else
                    sensor = await db.dataSensor.ToListAsync();

                if (sensor.Count == 0)
                    throw new ManageException(System.Net.HttpStatusCode.NotFound, "No existe registros con esta matricula");
                var multasvalidas = new List<MultaListResponse>();
                var multasinvalidas = new List<MultaListResponse>();
                foreach (var item in sensor)
                {
                    var multa = await db.multa.Where(x => x.idDataSensor == item.id).ToListAsync();
                    foreach (var item2 in multa)
                    {
                        if (item2.ismulta)
                            multasvalidas.Add(new MultaListResponse { matricula = item.matricula, info = new MultaInfoResponse() { ciudadAsignada = item2.ciudad, distancia = item2.distancia.ToString(), multa = item2.observacion } });
                        else
                            multasinvalidas.Add(new MultaListResponse { matricula = item.matricula, info = new MultaInfoResponse() { ciudadAsignada = item2.ciudad, distancia = item2.distancia.ToString(), multa = item2.observacion } });
                    }
                }
                response.multasValidas = multasvalidas;
                response.multasInvalidas = multasinvalidas;
                return response;
            }
        }
    }
}
