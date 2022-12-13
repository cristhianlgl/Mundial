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
		private static JugadorRepository repository = new JugadorRepository();
		public static List<JugadorEntity> GetAllJugadores()
		{
			return repository.GetAll(TipoPollaBO.TipoConsultado).ToList();
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

		        

		public static void SavePuntosJugador(JugadorEntity jugador)
		{
			JugadorRepository.SavePuntosJugador(jugador);
		}
	}
}
