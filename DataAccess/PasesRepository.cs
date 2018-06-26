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
    public static class PasesRepository
    {

        public static List<PaseEntity> GetPasesPorJugador(int jugadorID)
        {
            List<PaseEntity> lista = new List<PaseEntity>();
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT pollapases.*, equipos.grupo  FROM pollapases inner join
                               equipos on pollapases.Equipo = equipos.codEquipo 
                               where idjugador = @idjugador;";
                MySqlCommand cmd = new MySqlCommand(sql, conx);
                cmd.Parameters.AddWithValue("idjugador", jugadorID);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                   lista.Add(LoadPase(reader));
                }
            }
            return lista;
        }

        public static void SavePuntosPasesPorJugador(PartidoEntity Pases)
        {
            throw new NotImplementedException();
        }


        private static PaseEntity LoadPase(IDataReader reader)
        {
            PaseEntity item = new PaseEntity();
            item.EquipoId = reader["Equipo"].ToString();
            item.PaseId = Convert.ToString(reader["idPase"]);
            item.JugadorId = Convert.ToInt32(reader["idjugador"]);
            item.Grupo = Convert.ToString(reader["grupo"]);
            item.Puntos = Convert.ToInt32(reader["puntos"]);
            return item;
        }

    }
}
