using System.Collections.Generic;
using System.Linq;
using Entities;
using DataAccess;

namespace Bussines
{
    public static class PasesBO
    {
        private static PasesRepository repo = new PasesRepository();
        public static List<PaseEntity> GetPasesPorJugador(int jugadorID, string fase)
        {
            return repo.GetPasesPorJugador(jugadorID, fase).ToList();
        }

        public static void SavePuntosPases(PartidoEntity Pases)
        {
            repo.SavePuntosPasesPorJugador(Pases);
        }

        public static List<PaseEntity> GetPasesAll()
        {
            return  repo.GetPasesAll();
        }

        public static string SaveOneByJugador(PaseEntity pase)
        {
            if (pase == null)
                return "Error el pase esta nulo";
            
            repo.SaveOne(pase);
            return null;
        }

        public static void CalcularPasesE1(JugadorEntity jugador, List<PaseEntity> PasesResultados, string fase)
        {
            List<PaseEntity> pasesJugador = GetPasesPorJugador(jugador.JugadorId, fase).ToList();
            int igualPosicion;
            int puntos;
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
                        if (pase.Equipo == result.Equipo)
                        {
                            puntos += 1;
                            if (pase.IdPase == result.IdPase)
                            {
                                igualPosicion += 1;
                            }
                        }
                    }
                }
                if (igualPosicion == 2) puntos += 1;
                jugador.PuntosEtapa1 += puntos;

            }
        }

        public static void CalcularPasesE2(JugadorEntity jugador, List<PaseEntity> PasesResultados, string fase)
        {
            List<PaseEntity> pasesJugador = GetPasesPorJugador(jugador.JugadorId, fase).ToList();
            foreach (PaseEntity paseJ in pasesJugador)
            {
                foreach (PaseEntity result in PasesResultados)
                {
                    if (paseJ.IdPase == result.IdPase)
                    {
                        if (paseJ.Equipo == result.Equipo)
                        {
                            //puntos adicionales
                            switch (paseJ.IdPase)
                            {
                                case "ZCAM":
                                    paseJ.Puntos += 5; //En total 5 puntos - Campeon
                                    break;
                                case "ZTER":
                                    paseJ.Puntos += 3; //En Total 3 puntos -  Tercer Puesto
                                    break;
                                case "GOL":
                                    paseJ.Puntos += 3; //En Total 3 puntos - Nombre Goleador 
                                    break;
                                default:
                                    paseJ.Puntos += 2; //Acierto de pase de un Equipo
                                    break;
                            }
                        }

                        // se valida que el pase se Goleador y se compra los goles marcados coincidan con los resultados 
                        //ya que puede ganar puntos aunque no coincida el goleador
                        if (paseJ.IdPase == "GOL")
                        {
                            if (paseJ.Goles == result.Goles)  //Cantidade de Goles
                                paseJ.Puntos += 2;
                        }
                    }
                }
                jugador.PuntosEtapa2 += paseJ.Puntos;
            }
        }
    }
}
