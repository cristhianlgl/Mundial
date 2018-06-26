using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PaseEntity
    {
        public string PaseId { get; set; }
        public int JugadorId { get; set; }
        public string Grupo { get; set; }
        public string Fase { get; set; }
        public string EquipoId { get; set; }
        public int Puntos { get; set; }
    }
}
