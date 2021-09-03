using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sacaito
{
    public class Jugadores
    {
        public string Nombre { get; set; }
        public Jugadores Siguiente { get; set; }
        public Jugadores Anterior { get; set; }
        public Pila Carta { get; set; }
        public Pila Poder { get; set; }
        public int Puntos { get; set; }

        public Jugadores(string Nombre)
        {
            this.Nombre = Nombre;
            Carta = new Pila();
            Poder = new Pila();
            this.Siguiente = null;
            this.Anterior = null;
        }

        public Jugadores()
        {
            this.Nombre = "";
            Carta = new Pila();
            Poder = new Pila();
            this.Siguiente = null;
            this.Anterior = null;
        }
    }
}
