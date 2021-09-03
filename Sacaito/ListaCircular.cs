using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sacaito
{
    //Lista circular Doblemente enlazada
    public class ListaCircular
    {
        private Jugadores Primero { get; set; }
        private Jugadores Ultimo { get; set; }

        public ListaCircular()
        {
            Primero = Ultimo = null;
        }

        public void Agregar(string Nombre)
        {
            Jugadores Nuevo = new Jugadores(Nombre);

            if (Primero == null)
                Primero = Ultimo = Nuevo;
            else
            {

                Ultimo.Siguiente = Nuevo;
                Nuevo.Anterior = Ultimo;
                Ultimo = Nuevo;
                Ultimo.Siguiente = Primero;
                Primero.Anterior = Ultimo;
            }
        }
        /// <summary>
        /// Para iniciar con el jugar que saco la carta mas alta para que inicie y inicia hacia su derecha
        /// Le pasamos la referencia de quien sera el primer jugador
        /// </summary>
        /// <param name="jugador"></param>
        public void InsertarPrimerJugador(Jugadores jugador)
        {
            Primero = jugador;
        }
        /// <summary>
        /// Le pasamos el primer nodo para que retorne el primer jugador de la lista
        /// </summary>
        /// <returns></returns>
        public Jugadores ObtenerPrimerNodo()
        {
            return Primero;
        }
    }
}
