using Dapper;
using System.Collections.Generic;

namespace DataAccess
{
    public class TipoPollaRepository : BaseRepository
    {
        public IEnumerable<string> GetTipoPolla() 
        {
            using (Conn)
            {
                string query = "SELECT idPolla as tipo FROM Mundial.PollaPartidos group by idPolla ;";
                return Conn.Query<string>(query);
            }
        }

    }
}
