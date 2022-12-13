using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bussines
{
    public static class GoleadorBO
    {
        private static GoleadorRepository repo = new GoleadorRepository();

        public static void CalcularGoleador(JugadorEntity jugador, List<GoleadorEntity> resultados, string fase)
        {
            int puntos = 0;
            var resultado = resultados.Where(x => x.Fase == fase).FirstOrDefault();
            var goleadorJugador = jugador.Goleadores.Where(x => x.Fase == fase).FirstOrDefault();

            if (resultado.IdGoleador == goleadorJugador.IdGoleador)
                puntos++;

            if (resultado.GolesMarcados == goleadorJugador.GolesMarcados)
                puntos++;

            if (puntos == 2)
                puntos++;
                        
            jugador.PuntosEtapa1 += puntos;
        }

        public static List<GoleadorEntity> GetAll()
        {
            return repo.GetAll().ToList();
        }



        public static List<GoleadorEntity> GetPorJugador(int jugadorID, string fase)
        {
            return repo.GetPasesPorJugador(jugadorID, fase).ToList();
        }
    }
}
