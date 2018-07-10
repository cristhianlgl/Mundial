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

		public static void CalcularPartidos(JugadorEntity jugador, List<PartidoEntity> PartidosResultados, string Fase) 
		{
			if (Fase == "E1")
				jugador.PuntosEtapa1 = 0;
			else
				jugador.PuntosEtapa2 = 0;

			List<PartidoEntity> marcadoresJugador =  PartidosBO.GetMarcadoresPorJugador(jugador.JugadorId, Fase);

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
				if(Fase == "E1")
					jugador.PuntosEtapa1 += marcadorJ.Puntos; 
				else
					jugador.PuntosEtapa2 += marcadorJ.Puntos; 
			}        
		}

		public static void CalcularPasesE1(JugadorEntity jugador, List<PaseEntity> PasesResultados)
		{
			List<PaseEntity> pasesJugador = PasesRepository.GetPasesPorJugador(jugador.JugadorId, "E1");
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

		public static void CalcularPasesE2(JugadorEntity jugador, List<PaseEntity> PasesResultados)
		{
			List<PaseEntity> pasesJugador = PasesRepository.GetPasesPorJugador(jugador.JugadorId, "E2");
			foreach (PaseEntity paseJ in pasesJugador)
			{
				foreach (PaseEntity result in PasesResultados)
				{
					if(paseJ.PaseId ==  result.PaseId )
					{
						if (paseJ.EquipoId == result.EquipoId)
						{
							paseJ.Puntos += 2;
							//puntos adicionales
							switch (paseJ.PaseId)
							{
								case "GOL": if (paseJ.Goles == result.Goles)
											    paseJ.Puntos += 2;
											break;
								case "CAM": paseJ.Puntos += 3; //En total 5 puntos
											break;
								case "TER": paseJ.Puntos += 1;
											break;
							}
						}
					}
				}
				jugador.PuntosEtapa2 += paseJ.Puntos;
			}      
		}        

		public static void SavePuntosJugador(JugadorEntity jugador)
		{
			JugadorRepository.SavePuntosJugador(jugador);
		}
	}
}
