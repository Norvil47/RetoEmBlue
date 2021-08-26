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
    public class RegistrarMulta
    {
        public class Request : IRequest<MessageJson<MultaResponse>>
        {
            public int localizacion { get; set; }
            public string imagen { get; set; }
            public int alturaVuelo { get; set; }
            public int velocidad { get; set; }
            public string matricula { get; set; }
            public string ciudadPertenece { get; set; }

        }

        public class CommandHandle : IRequestHandler<Request, MessageJson<MultaResponse>>
        {

            private readonly ModeloContext db;

            public CommandHandle(ModeloContext context)
            {
                db = context;


            }


            async Task<MessageJson<MultaResponse>> IRequestHandler<Request, MessageJson<MultaResponse>>.Handle(Request request, CancellationToken cancellationToken)
            {
                string multa = "";
                var ciudades = await db.ciudad.ToListAsync();
                var ciudad = "";
                if (request.matricula is null || request.matricula is "")
                    throw new ManageException(System.Net.HttpStatusCode.NotFound, "No existe matricula");

                if (request.localizacion > -500 && request.localizacion <= -200)
                    ciudad = "Ciudad Norte";
                else if (request.localizacion > -100 && request.localizacion <= 100)
                    ciudad = "Ciudad Sur";
                else if (request.localizacion > 100 && request.localizacion <= 200)
                    ciudad = "Ciudad Oeste";
                else if (request.localizacion > 100 && request.localizacion <= 500)
                    ciudad = "Ciudad Este";

                if (ciudad is "")
                    throw new ManageException(System.Net.HttpStatusCode.NotFound, "No se puede determinar posición");


                //validar carros voladores
                if (request.alturaVuelo is not 0)
                {
                    if (request.alturaVuelo > 50)
                        multa = "valida";
                    else if (request.velocidad > 120)
                        multa = "valida";
                    else
                        multa = "inválida";

                }
                else//validar carros terrestres
                {
                    if (request.velocidad > 120)
                        multa = "valida";
                    else
                        multa = "inválida";
                }

                var sensor = new DataSensor()
                {
                    alturaVuelo = request.alturaVuelo,
                    ciudadPertenece = request.ciudadPertenece,
                    imagen = request.imagen,
                    localizacion = request.localizacion,
                    matricula = request.matricula,
                    velocidad = request.localizacion,
                };
                await db.AddAsync(sensor);
                await db.SaveChangesAsync();

                var multaobj = new Multa()
                {
                    idDataSensor = sensor.id,
                    ismulta = multa == "valida" ? true : false,
                    observacion = $"La multa es {multa}",
                    ciudad=ciudad    ,
                    distancia=100
                };
                await db.AddAsync(multaobj);
                await db.SaveChangesAsync();

                return new MessageJson<MultaResponse> { mensaje = multaobj.observacion, objeto = new MultaResponse { ciudadAsignada = ciudad, distancia = multaobj.distancia.ToString(), multa = multaobj.observacion } };
            }
        }
    }
}
