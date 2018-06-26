using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Bussines
{
    public static class PasesBO
    {
        public static List<PaseEntity> GetPasesPorJugador(int jugadorID)
        {
            return PasesRepository.GetPasesPorJugador(jugadorID);
        }

        public static void SavePuntosPases(PartidoEntity Pases)
        {
            PasesRepository.SavePuntosPasesPorJugador(Pases);
        }
    }
}
