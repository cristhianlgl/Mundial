using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Bussines
{
    public static class JugadorBO
    {
        public static List<JugadorEntity> GetAllJugadores()
        {
            return JugadorRepository.GetAll();
        }
    }
}
