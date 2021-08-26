using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reto.Entidades
{
    [Table("ciudad")]
    public class Ciudad
    {
        [Key]
        public int idciudad { get; set; }
        public string nombre { get; set; }
        [Column("coordenadainicial")]
        public int coordenadaInicial { get; set; }
        [Column("coordenadafinal")]

        public int coordenadaFinal { get; set; }

    }
}
