using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Pase
    {
        public string PaseId { get; set; }
        public string Fase { get; set; }
        public Equipo Equipo { get; set; }
    }
}
