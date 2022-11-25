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
        private static ConfiguracionBO confBO = new ConfiguracionBO(); 


        public static List<PartidoEntity> GetMarcadoresPorJugador(int jugadorID, string fase) 
        {
            return PartidosRepository.GetMarcadoresPorJugador(jugadorID, fase);
        }

        public  static void SavePuntosPartido(PartidoEntity Marcador)
        {
            PartidosRepository.SavePuntosMarcadorPorJugador(Marcador);
        }

        public static List<PartidoEntity> GetAll()
        {
            return PartidosRepository.GetPartidosAJugar();
        }

        public static string SaveAllByJugador(JugadorEntity jugador)
        {
            if (confBO.EstaBloqueadoModidicarMarcadores() && jugador.JugadorId != 0)
                return "El Marcador No puede ser Modificado porque esta Bloqueado";

            if( jugador.Marcadores != null && jugador.Marcadores.Count > 0)
            {
                var marcadorConDatosInvalidos = jugador.Marcadores.Where(x => x.MarcadorE1 < 0 || x.MarcadorE2 < 0).ToList();
                if( marcadorConDatosInvalidos.Count > 0)
                    return "Hay marcadores que tienen valores incorrectos por favor revise";

                PartidosRepository.SaveAllByJugador(jugador);
                return null;

            }
            else
                return "El jugador " + jugador.Nombre + " NO tiene marcadores por favor revise";
             
           
        }

        public static string SaveOneByJugador(PartidoEntity marcador)
        {
            if (confBO.EstaBloqueadoModidicarMarcadores() && marcador.JugadorId != 0)
                return "El Marcador No puede ser Modificado por esta Bloqueado";

            if (marcador.MarcadorE1 < 0 || marcador.MarcadorE2 < 0)
                return "Hay marcadores que tienen valores incorrectos por favor revise";

            PartidosRepository.SaveOneByJugador(marcador);
            return null;
        }
    }
}
