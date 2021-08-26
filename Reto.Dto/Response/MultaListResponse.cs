using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Dto.Response
{
    public class MultaDataListResponse
    {
        public List<MultaListResponse> multasValidas { get; set; }
        public List<MultaListResponse> multasInvalidas { get; set; }
    }
    public class MultaListResponse
    {
        public string matricula { get; set; }
        public MultaInfoResponse info { get; set; }
    }

    public class MultaInfoResponse
    {
        public string distancia { get; set; }
        public string multa { get; set; }
        public string ciudadAsignada { get; set; }
    }
}
