using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Bussines
{
    public static class JugadorBO
    {
        public static List<JugadorEntity> GetAllJugadores()
        {
            return JugadorRepository.GetAll();
        }

        public static List<JugadorEntity> GetByJugador(int JugadorId)
        {
            return JugadorRepository.GetById(JugadorId);
        }

        public static void CalcularPartidosE1(JugadorEntity jugador, List<PartidoEntity> PartidosResultados) 
        {
            jugador.PuntosEtapa1 = 0;
            List<PartidoEntity> marcadoresJugador =  PartidosBO.GetMarcadoresPorJugador(jugador.JugadorId);

            foreach (PartidoEntity marcadorJ in marcadoresJugador)
            {
                marcadorJ.Puntos = 0;
                foreach (PartidoEntity resultado in PartidosResultados)
                {
                    if (marcadorJ.PartidoId == resultado.PartidoId)
                    {
                        if (marcadorJ.MarcadorE1 == resultado.MarcadorE1)
                        {
                            marcadorJ.Puntos += 1; 
                        }

                        if(marcadorJ.MarcadorE2 == resultado.MarcadorE2)
                        {
                            marcadorJ.Puntos += 1; 
                        }

                        if(marcadorJ.Puntos == 2)
                        {
                            marcadorJ.Puntos += 1; 
                        }
                    }
                }
                //PartidosBO.SavePuntosPartido(marcadorJ);
                jugador.PuntosEtapa1 += marcadorJ.Puntos; 
            }        
        }

        public static void CalcularPasesE1(JugadorEntity jugador, List<PaseEntity> PasesResultados)
        {
            List<PaseEntity> pasesJugador = PasesRepository.GetPasesPorJugador(jugador.JugadorId);
            int igualPosicion;
            int puntos ;
            string[] grupos = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };

            foreach (var grupo in grupos)
            {
                puntos = 0;
                igualPosicion = 0;
                var pasesPorGrupo = pasesJugador.Where(x => x.Grupo == grupo);
                var resultadoPorGrupo = PasesResultados.Where(x => x.Grupo == grupo);
                foreach (PaseEntity pase in pasesPorGrupo)
                {
                    foreach (PaseEntity result in resultadoPorGrupo)
                    {
                        if (pase.EquipoId == result.EquipoId)
                        {
                            puntos += 1;
                            if (pase.PaseId == result.PaseId)
                            {
                                igualPosicion += 1;
                            }
                        }                        
                    }
                }
                if(igualPosicion == 2) puntos += 1;
                jugador.PuntosEtapa1 += puntos;

            }     
        }        


        public static void SavePuntosJugador(JugadorEntity jugador)
        {
            JugadorRepository.SavePuntosJugador(jugador);
        }
    }
}
