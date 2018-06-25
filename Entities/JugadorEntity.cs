using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class JugadorEntity
    {
        public int JugadorId { get; set; }
        public string Nombre { get; set; }
        public int PuntosEtapa1 { get; set; }
        public int PuntosEtapa2 { get; set; }

        public int TotalPuntos {    get
                                    {
                                        return PuntosEtapa1 + PuntosEtapa2;
                                    }
                                }
    }
}
