namespace Entities
{
    public  class GoleadorEntity
    {
        public int IdGoleador { get; set; }
        public int IdJugador { get; set; }
        public string Nombre { get; set; }
        public int Goles { get; set; }
        public int Puntos { get; set; }
        public string Fase { get; set; }
    }
}
