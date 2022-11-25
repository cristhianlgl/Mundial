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

        public static List<PaseEntity> GetPasesPorJugador(int jugadorID, string fase)
        {
            List<PaseEntity> lista = new List<PaseEntity>();
            using (MySqlConnection conx = new MySqlConnection(ConfigurationManager.ConnectionStrings["Mysql"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT PollaPases.*, Equipos.grupo  FROM Pase INNER JOIN (PollaPases inner join
                               Equipos on PollaPases.Equipo = Equipos.codEquipo) 
                               ON Pase.idPase = PollaPases.idPase
                               where idjugador = @idjugador AND Fase = @fase;";
                MySqlCommand cmd = new MySqlCommand(sql, conx);
                cmd.Parameters.AddWithValue("idjugador", jugadorID);
                cmd.Parameters.AddWithValue("fase", fase);
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
            item.Goles = 0;
            if (item.PaseId == "GOL")
            {
                item.Goles = item.Puntos;
                item.Puntos = 0;
            }
            return item;
        }

    }
}
