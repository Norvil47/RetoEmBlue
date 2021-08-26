using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Reto.SeedWork
{
    public class ManageException : Exception
    {
        public HttpStatusCode Codigo { get; }
        public List<string> Errores { get; }
        public ManageException(HttpStatusCode codigo, string errores = "")
        {
            Codigo = codigo;
            var arrayerror = errores.Split("|");
            Errores = arrayerror.ToList();
        }
    }
}
