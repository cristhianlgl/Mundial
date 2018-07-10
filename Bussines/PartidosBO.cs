using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Bussines
{
    public static class PartidosBO
    {
        public static List<PartidoEntity> GetMarcadoresPorJugador(int jugadorID, string fase) 
        {
            return PartidosRepository.GetMarcadoresPorJugador(jugadorID, fase);
        }

        public  static void SavePuntosPartido(PartidoEntity Marcador)
        {
            PartidosRepository.SavePuntosMarcadorPorJugador(Marcador);
        }
    }
}
