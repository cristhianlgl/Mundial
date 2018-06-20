using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Partido
    {
        public string PartidoId { get; set; }
        public Equipo Equipo1 { get; set; }
        public Equipo Equipo2 { get; set; }
        public int MarcadorE1 { get; set; }
        public int MarcadorE2 { get; set; }
    }
}
