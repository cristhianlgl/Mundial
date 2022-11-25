using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Entities;

namespace DataAccess
{
    public class ConfiguracionDAL
    {
        private IDbConnection conn;

        public IDbConnection Conn
        {
            get { return conn = ConnetionFactory.CreateConnection(conn); }
        }
        
        public List<ConfiguracionEntity> ObtenerTodo()
        {
            using (Conn)
            {
                string query = @"SELECT idConfiguracion , Nombre, Valor FROM Configuracion;";
                return Conn.Query<ConfiguracionEntity>(query).ToList();
            }
        }

        public ConfiguracionEntity ObtenerPorNombre(string nombre)
        {
            using (Conn)
            {
                string query = @"SELECT idConfiguracion , Nombre, Valor FROM Configuracion WHERE Nombre = @Nombre;";
                return Conn.Query<ConfiguracionEntity>(query, new { Nombre = nombre }).FirstOrDefault();
            }
        }

        public int Update(ConfiguracionEntity conf) 
        {
            using (Conn) 
            {
                string query = @"UPDATE  Configuracion SET  Nombre = @Nombre, Valor = @Valor 
                                 WHERE idConfiguracion = @idConfiguracion ;";
                return Conn.Execute(query, conf);
            }
        }


    }
}
