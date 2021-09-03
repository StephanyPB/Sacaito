using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sacaito
{
    public class Pila
    {
        public Cartas Cabeza;
        public int Elementos;

        public Pila()
        {
            Cabeza = null;
            Elementos = 0;
        }
		/// <summary>
		//cada vez que se hace push la cantidad de elementos aumenta

		public void Push(string nombre, int valor)
		{
			Cartas Cartas = new Cartas(nombre, valor);

			if (Cabeza == null)
				Cabeza = Cartas;
			else
			{
				Cartas.Siguiente = Cabeza;
				Cabeza = Cartas;
			}
			Elementos++;
		}
		//cada vez que se hace pop la cantidad de elementos disminuye
		public Cartas Pop()
		{
			Cartas aux = Cabeza;
			Cabeza = Cabeza.Siguiente;
			aux.Siguiente = null;
			Elementos--;
			return aux;
		}
		//para mostrar el objeto nodo que esta en la pila(el primer elemento)
		public Cartas Peek()
		{
			return Cabeza;
		}
		/// <summary>
		/// Para saber si la pila esta vacia
		/// </summary>
		public bool EsVacio()
		{
			return Cabeza == null;
		}
		/// <summary>
		/// obtener la cantidad de elementos que hay
		/// </summary>
		/// <returns></returns>
		public int ObtenerCantidadCartas()
		{
			return Elementos;
		}
	}
}
