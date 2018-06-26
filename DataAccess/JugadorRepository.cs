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
    public static class JugadorRepository
    {

        public static List<JugadorEntity> GetAll()
        {
            List<JugadorEntity> lista = new List<JugadorEntity>();
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM jugador";
                MySqlCommand cmd = new MySqlCommand(sql, conx);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while(reader.Read())
                {
                    lista.Add(LoadJugador(reader));
                }
            }
            return lista;
        }

        public static List<JugadorEntity> GetById(int jugadorId)
        {
            List<JugadorEntity> lista = new List<JugadorEntity>();
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM jugador WHERE idjugador = @ID ";
                MySqlCommand cmd = new MySqlCommand(sql, conx);
                cmd.Parameters.AddWithValue("ID", jugadorId);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    lista.Add(LoadJugador(reader));
                }
            }
            return lista;
        }

        private static JugadorEntity LoadJugador(IDataReader reader)
        {
            JugadorEntity item = new JugadorEntity();
            item.JugadorId = Convert.ToInt32(reader["idjugador"]);
            item.Nombre = reader["nombre"].ToString();
            item.PuntosEtapa1 = Convert.ToInt32(reader["puntosEtapa1"]);
            item.PuntosEtapa2 = Convert.ToInt32(reader["puntosEtapa2"]);
            return item;
        }
        
        public static void SavePuntosJugador(JugadorEntity jugador)
        {
            using(MySqlConnection conx =  new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"UPDATE jugador SET puntosEtapa1 = @puntosEtapa1, puntosEtapa2= @puntosEtapa2 
                               WHERE idjugador=@idjugador;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conx))
                {
                    cmd.Parameters.AddWithValue("idjugador", jugador.JugadorId);
                    cmd.Parameters.AddWithValue("puntosEtapa1", jugador.PuntosEtapa1);
                    cmd.Parameters.AddWithValue("puntosEtapa2", jugador.PuntosEtapa2);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
