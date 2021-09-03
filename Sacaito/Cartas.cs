using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sacaito
{
    public class Cartas
    {
		public string Nombre { get; set; }
		public int Valor { get; set; }
		public Cartas Siguiente { get; set; }

		/// <summary>
		/// Construtor Vacio para inicializar valores.
		/// </summary>
		public Cartas()
		{
			Nombre = "";
			Valor = 0;
			Siguiente = null;
		}
		/// <summary>
		/// Constructor que recibe parametros para inicializar valores.
		/// </summary>
		/// <param name="Nombre">El nombre de la carta</param>
		/// <param name="Valor">El valor de la carta</param>
		public Cartas(string Nombre, int Valor)
		{
			this.Nombre = Nombre;
			this.Valor = Valor;
			this.Siguiente= null;
		}
		/// <summary>
		/// Este metodo se encarga de Crear la baraja con sus tipo y su valor.
		/// </summary>
		/// <param name="cartas">Recibe un arreglo por referencia de las cartas que va a crear.</param>
		public static void CrearBaraja(ref Cartas[] cartas)
        {
			string[] Tipos = { "Diamantes", "Picas", "Treboles", "Corazones" };///TIPOS DE CARTAS OBTENIDOS DE WIKIPEDIA :)
			int[] Valores = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };/// VALOR QUE TIENE CADA CARTA
			int ContadorCartas = 0;

			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 13; j++)
				{
					cartas[ContadorCartas] = new Cartas(Tipos[i], Valores[j]);
					ContadorCartas++;
				}
			}
		}
		/// <summary>
		/// Este metodo se encarga de mezclar las cartas.
		/// </summary>
		/// <param name="cartas">Recibi un arreglo por referencia de las cartas que va a mezclar.</param>
		public static void MezclarBaraja(ref Cartas[] cartas)
        {
			Random random = new Random();
			for (int i = 0; i < cartas.Length; i++)
			{
				Cartas temp = cartas[i];
				int index = random.Next(0, cartas.Length);
				cartas[i] = cartas[index];
				cartas[index] = temp;
			}
		}
		/// <summary>
		/// Este metodo se encarga de mover las cartas a la pila de cartas que usaremos para jugar.
		/// </summary>
		/// <param name="cartas">Arreglo de cartas que vamos a mover.</param>
		/// <param name="pila">La pila que sera el recipiente del arreglo de cartas.</param>
		public static void MoverCartas(ref Cartas[]cartas,ref Pila pila)
        {//se hace con referencia ya que le pasamos el valor original
			for (int i = 0; i < cartas.Length; i++)
			{
				if (cartas[i].Valor != 0)
				{
					pila.Push(cartas[i].Nombre, cartas[i].Valor);
				}
			}
		}
	}
}
