using System.Collections.Generic;

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
        public List<PartidoEntity> Marcadores { get; set; }
        public List<PaseEntity> Pases { get; set; }
        public List<GoleadorEntity> Goleadores { get; set; }

        public JugadorEntity()
        {
            Marcadores = new List<PartidoEntity>();
            Pases = new List<PaseEntity>();
            Goleadores = new List<GoleadorEntity>();
        }

    }
}
