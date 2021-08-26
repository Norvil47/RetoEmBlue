using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Reto.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Reto.Api.Middleware
{
    public class ErrorMiddleware
    {
      
            private readonly RequestDelegate _next;
            private readonly ILogger<ErrorMiddleware> _logger;
            public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    await ManejadorExcepcionAsincrono(context, ex, _logger);
                }
            }

            private async Task ManejadorExcepcionAsincrono(HttpContext context, Exception ex, ILogger<ErrorMiddleware> logger)
            {
                object errores = null;
                List<string> errores500 = new List<string>();
                switch (ex)
                {
                    case ManageException me:
                        //logger.LogError(ex.Message, "Error Manejado");
                        errores = me.Errores;
                        context.Response.StatusCode = (int)me.Codigo;
                        break;
                    case Exception e:
                        errores500.Add(e.Message);
                        //errores500.Add(e.InnerException.ToString());
                        errores = errores500;
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                context.Response.ContentType = "application/json";
                if (errores != null)
                {
                    var resultados = JsonConvert.SerializeObject(new { errores });
                    await context.Response.WriteAsync(resultados);
                }

            }
        }
    
}
