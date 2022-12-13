using System.Data;

namespace DataAccess
{
    public class BaseRepository
    {
        private IDbConnection conn;

        public IDbConnection Conn
        {
            get { return conn = ConnetionFactory.CreateConnection(conn); }
        }
    }
}
