using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;
using MySql.Data.MySqlClient;
using System.Configuration;
using Dapper;


namespace DataAccess
{
    public static class PartidosRepository
    {
        private static readonly string queryUpdateMarcador = @"UPDATE PollaPartidos  
                                            SET marcador1 = @MarcadorE1 , marcador2 = @MarcadorE2 
                                            WHERE idjugador = @Idjugador AND idPartido= @PartidoId";


        public static List<PartidoEntity> GetPartidosAJugar()
        {
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT idjugador as JugadorId, idPartido as PartidoId, equipo1, E1.nombre as NombreE1,
                                    marcador1 as MarcadorE1, equipo2, E2.nombre as NombreE2, marcador2 as MarcadorE2,
                                    puntos, fase
                               FROM PollaPartidos INNER JOIN Partido USING (idPartido)
                                   INNER JOIN Equipos as E2 ON E2.codEquipo = Partido.equipo2
                                   INNER JOIN Equipos as E1 ON E1.codEquipo = Partido.equipo1
                               ORDER BY idjugador, idPartido;";
                return conx.Query<PartidoEntity>(sql).ToList();      
            }
        }
        
        public static List<PartidoEntity> GetMarcadoresPorJugador(int jugadorID, string fase)
        {
            List<PartidoEntity> lista = new List<PartidoEntity>();
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM PollaPartidos inner join Partido
                                on Partido.idPartido = PollaPartidos.idPartido
                                WHERE idjugador =@idjugador and fase =@fase ;";
                MySqlCommand cmd = new MySqlCommand(sql, conx);
                cmd.Parameters.AddWithValue("idjugador", jugadorID);
                cmd.Parameters.AddWithValue("fase", fase);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    lista.Add(LoadPartido(reader));
                }
            }
            return lista;
        }

        private static PartidoEntity LoadPartido(IDataReader reader)
        {
            PartidoEntity item = new PartidoEntity();
            item.PartidoId = reader["idPartido"].ToString();
            item.JugadorId = Convert.ToInt32(reader["idjugador"]);
            item.MarcadorE1 = Convert.ToInt32(reader["marcador1"]);
            item.MarcadorE2 = Convert.ToInt32(reader["marcador2"]);
            item.Puntos = 0;
            return item;
        }

        public static void  SavePuntosMarcadorPorJugador(PartidoEntity PartidoMarcadorJugador)
        {
            List<PartidoEntity> lista = new List<PartidoEntity>();
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"UPDATE pollapartidos SET puntos=@puntos 
                                WHERE idjugador=@jugador and idPartido =@partido ;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conx)) 
                {
                    cmd.Parameters.AddWithValue("partido", PartidoMarcadorJugador.PartidoId);
                    cmd.Parameters.AddWithValue("jugador", PartidoMarcadorJugador.JugadorId);
                    cmd.Parameters.AddWithValue("puntos", PartidoMarcadorJugador.Puntos);
                    cmd.ExecuteNonQuery();             
                }
            }         
        
        }

        public static bool SaveAllByJugador(JugadorEntity jugador)
        {
            bool resp = false;
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                try
                {
                    conx.Open();
                    
                    foreach (var marcador in jugador.Marcadores)
                    {
                        conx.Execute(queryUpdateMarcador, marcador);
                    }
                    resp = true;
                }
                catch
                {
                    throw;
                }
                finally 
                {
                    conx.Close();                
                }
            }
            return resp;
        }

        public static bool SaveOneByJugador(PartidoEntity marcador)
        {
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                try
                {
                    return Convert.ToBoolean(conx.Execute(queryUpdateMarcador, marcador));
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conx.Close();
                }
            }
        }
    }
}
