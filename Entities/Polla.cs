using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Polla
    {
        public JugadorEntity Jugador { get; set; }
        public List<PartidoEntity> PartidosE1 { get; set; }
        public List<PaseEntity> PasesE1 { get; set; }
        public int PuntosPartidosE1 { get; set; }
        public int PuntosPasessE1 { get; set; }
    }
}
