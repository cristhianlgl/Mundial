using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using Dapper;


namespace DataAccess
{
    public class JugadorRepository : BaseRepository
    {

        public IEnumerable<JugadorEntity> GetAll(string TipoPolla)
        {
            using (Conn) 
            {
                string query = @"SELECT idjugador as JugadorId, nombre, puntosEtapa1, puntosEtapa2 
                                FROM jugador WHERE idPolla = @idPolla OR  idjugador = 0;";
                return Conn.Query<JugadorEntity>(query, new { idPolla = TipoPolla });
            }
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
