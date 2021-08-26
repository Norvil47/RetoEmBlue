using Microsoft.EntityFrameworkCore;
using Reto.Entidades;
using System;

namespace Reto.Persistencia
{
    public class ModeloContext : DbContext
    {

        public DbSet<Ciudad> ciudad { get; set; }
        public DbSet<DataSensor> dataSensor { get; set; }
        public DbSet<Multa> multa { get; set; }

        public ModeloContext(DbContextOptions<ModeloContext> options)
         : base(options)
        {

        }
    }
}
