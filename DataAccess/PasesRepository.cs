using Dapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class PasesRepository : BaseRepository
    {
        public List<PaseEntity> GetPasesAll()
        {
            using (Conn)
            {
                string query = @"SELECT *, e.nombre as EquipoNombre FROM PollaPases as pp
                    INNER JOIN Pase using(idPase) inner join Equipos as e on pp.Equipo = e.codEquipo ;";
                return Conn.Query<PaseEntity>(query).ToList();
            }
        }

        public IEnumerable<PaseEntity> GetPasesPorJugador(int jugadorID, string fase)
        {
            using (Conn)
            {
                string query = @"SELECT PollaPases.*, Equipos.grupo  FROM Pase INNER JOIN (PollaPases inner join
                                   Equipos on PollaPases.Equipo = Equipos.codEquipo) 
                                   ON Pase.idPase = PollaPases.idPase
                                   where idjugador = @jugadorID AND Fase = @fase;";
                return Conn.Query<PaseEntity>(query, new {jugadorID, fase});
            }
        }

        public int SaveOne(PaseEntity pase)
        {
            using (Conn)
            {
                string query = "UPDATE PollaPases SET Equipo=@Equipo WHERE idPase=@IdPase AND idjugador=@Idjugador;";
                return Conn.Execute(query, pase);
            }
        }

        public void SavePuntosPasesPorJugador(PartidoEntity Pases)
        {
            throw new NotImplementedException();
        }
    }
}
