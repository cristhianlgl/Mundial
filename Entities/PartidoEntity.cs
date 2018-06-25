using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PartidoEntity
    {
        public string PartidoId { get; set; }
        public EquipoEntity Equipo1 { get; set; }
        public EquipoEntity Equipo2 { get; set; }
        public int MarcadorE1 { get; set; }
        public int MarcadorE2 { get; set; }
    }
}
