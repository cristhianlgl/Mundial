using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Bussines
{
    public class ConfiguracionBO
    {
        ConfiguracionDAL dal = new ConfiguracionDAL();

        public bool BloquearModificarMarcadores(bool valor) 
        {
            var conf = ObtenerConfiguracionModificarMarcadores();
            conf.Valor = valor.ToString();
            return Convert.ToBoolean(dal.Update(conf));            
        }

        public bool EstaBloqueadoModidicarMarcadores()
        {
            var conf = ObtenerConfiguracionModificarMarcadores();
            return Convert.ToBoolean(conf.Valor); 
        }

        private ConfiguracionEntity ObtenerConfiguracionModificarMarcadores()
        {
            var conf = dal.ObtenerPorNombre("BloquearActualizacion");
            if (conf == null)
                throw new Exception("No se ha definido la configuracion BloquearActualizacion");
            return conf;
        }

    }
}
