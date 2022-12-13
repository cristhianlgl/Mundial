using DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace Bussines
{
    public static class TipoPollaBO
    {
        private static TipoPollaRepository dal = new TipoPollaRepository();
        public static string TipoConsultado { get; set; }

        public static List<string>  GetTipos() {
            return dal.GetTipoPolla().ToList();
        }
    }
}
