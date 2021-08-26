using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Entidades
{
    [Table("datasensor")]
    public  class DataSensor
    {
        [Key]
        public int id { get; set; }
        public int localizacion { get; set; }
        public string imagen { get; set; }
        [Column("alturavuelo")]
        public int alturaVuelo { get; set; }
        public int velocidad { get; set; }
        public string matricula { get; set; }
        [Column("ciudadpertenece")]
        public string ciudadPertenece { get; set; }
    }
}
