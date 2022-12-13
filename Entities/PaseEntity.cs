using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PaseEntity
    {
        public string IdPase { get; set; }
        public int Idjugador { get; set; }
        public string Equipo { get; set; }
        public string EquipoNombre { get; set; }
        public string Grupo { get; set; }
        public string Fase { get; set; }
        public int Goles { get; set; }
        public int Puntos { get; set; }
    }
}
