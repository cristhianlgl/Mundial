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
                if(reader.Read())
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


    }
}
