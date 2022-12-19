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

        public static int CalcularGoleador(JugadorEntity jugador, List<GoleadorEntity> resultados, string fase,bool puntoExtra = false, bool puntosGoleadorFinal = false )
        {
            int puntos = 0;
            var resultado = resultados.Where(x => x.Fase == fase).FirstOrDefault();
            var goleadorJugador = jugador.Goleadores.Where(x => x.Fase == fase).FirstOrDefault();
            int puntosGol = puntosGoleadorFinal ? 3 : 1;
            int puntosMarcador = puntosGoleadorFinal ? 2 : 1;

            if (goleadorJugador is null)
                return puntos; 
             
            if (resultado.IdGoleador == goleadorJugador.IdGoleador)
                puntos += puntosGol;

            if (resultado.GolesMarcados == goleadorJugador.GolesMarcados)
                puntos += puntosMarcador;

            if (puntos == (puntosGol + puntosGol) && puntoExtra)
                puntos++;
                        
            return puntos;
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
