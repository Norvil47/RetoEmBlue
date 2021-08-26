using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.SeedWork
{
    public class MessageJson<TEntity>
    {
        public string mensaje { get; set; }
        public TEntity objeto { get; set; }
    }
}
