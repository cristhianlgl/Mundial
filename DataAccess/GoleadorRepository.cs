using Dapper;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class GoleadorRepository : BaseRepository
    {
        public List<GoleadorEntity> GetAll()
        {
            using (Conn)
            {
                string query = @"SELECT * FROM PollaGoleador inner join Goleador using(idGoleador);";
                return Conn.Query<GoleadorEntity>(query).ToList();
            }
        }

        public IEnumerable<GoleadorEntity> GetPasesPorJugador(int jugadorID, string fase)
        {
            using (Conn)
            {
                string query = @"SELECT * FROM PollaGoleador inner join Goleador using(idGoleador) 
                                WHERE fase =  @fase AND idjugador = @jugadorID;";
                return Conn.Query<GoleadorEntity>(query, new { jugadorID, fase });
            }
        }
    }
}
