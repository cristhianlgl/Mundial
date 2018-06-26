using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;


namespace DataAccess
{
    public static class PartidosRepository
    {
        public static List<PartidoEntity> GetPartidosAJugar()
        {
            List<PartidoEntity> lista = new List<PartidoEntity>();
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT partido.*, E1.nombre as nombreE1 , E1.grupo as grupoE1, E2.nombre as nombreE2,
                              E1.grupo as grupoE2 FROM equipos as E2 inner join (partido inner join equipos as E1 on
                              E1.codEquipo = partido.equipo1) on E2.codEquipo = partido.equipo2 ;";
                MySqlCommand cmd = new MySqlCommand(sql, conx);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    lista.Add(LoadPartidoAjugar(reader));
                }
            }
            return lista;
        }

        private static PartidoEntity LoadPartidoAjugar(IDataReader reader)
        {
           
            
                PartidoEntity item = new PartidoEntity()
                {
                    PartidoId = Convert.ToString(reader["idPartido"]),
                    Equipo1 =  new EquipoEntity()
                                            {
                                                EquipoId = Convert.ToInt32(reader["equipo1"]),
                                                Nombre = Convert.ToString(reader["nombreE1"]),
                                                Grupo = Convert.ToString(reader["grupoE1"])
                                            },

                    Equipo2 =  new EquipoEntity()
                                            {
                                                EquipoId = Convert.ToInt32(reader["equipo1"]),
                                                Nombre = Convert.ToString(reader["nombreE1"]),
                                                Grupo = Convert.ToString(reader["grupoE1"])
                                            },                    
                    Fase = Convert.ToString(reader["fase"]),
                    MarcadorE1 = 0,
                    MarcadorE2 = 0
                };


                return item;

        }

        
        public static List<PartidoEntity> GetMarcadoresPorJugador(int jugadorID)
        {
            List<PartidoEntity> lista = new List<PartidoEntity>();
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM pollapartidos WHERE idjugador = @idjugador;";
                MySqlCommand cmd = new MySqlCommand(sql, conx);
                cmd.Parameters.AddWithValue("idjugador", jugadorID);
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
    }
}
