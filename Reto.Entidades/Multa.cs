using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.Entidades
{
    [Table("multa")]

    public class Multa
    {
        [Key]
        public int id { get; set; }
        [Column("iddatasensor")]
        public int idDataSensor { get; set; }
        public string observacion { get; set; }
        public bool ismulta { get; set; }
        public int distancia { get; set; }
        public string ciudad { get; set; }
    }
}
