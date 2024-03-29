﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PartidoEntity
    {
        public string PartidoId { get; set; }
        public int JugadorId { get; set; }
        public string Equipo1 { get; set; }
        public string NombreE1 { get; set; }
        public int MarcadorE1 { get; set; }
        public string Equipo2 { get; set; }
        public int MarcadorE2 { get; set; }
        public string NombreE2 { get; set; }
        public string Fase { get; set; }
        public int Puntos { get; set; }
    }
}
