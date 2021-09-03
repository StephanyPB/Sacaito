using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sacaito
{
    public class QuitarCarta
    {   
        /// <summary>
        /// Este metodo se encarga de obtener la carta de un jugador
        /// </summary>
        /// <param name="Jugador"></param>
        public void ObtenerCarta(Jugadores Jugador)//recibe el jugador que tiene el poder
        {
            Cartas cartas;
            bool YaUsoElPoder = false;

            Jugadores Auxiliar = Jugador.Siguiente;

            while (!Jugador.Poder.EsVacio() && !YaUsoElPoder)//Condicion hasta que el jugador tenga un poder
            {
                Console.WriteLine($"El jugador que perdera la carta es: {Auxiliar.Nombre}");
                Console.WriteLine($"La carta es : {Auxiliar.Carta.Peek().Valor } de {Auxiliar.Carta.Peek().Nombre}");
                Console.WriteLine("1.Siguiente Jugador     2.Obtener carta 3. No usar el poder");

                
                int.TryParse(Console.ReadLine(),out int Opcion);

                switch (Opcion)
                {
                    case 1:
                        Auxiliar = Auxiliar.Siguiente;
                        //Si llegamos al punto en el que estamos en la misma posicion que el jugador de turno , nos movemos a la siguiente posicion.
                        if (Auxiliar == Jugador)
                            Auxiliar = Auxiliar.Siguiente;
                        break;

                    case 2:
                        cartas = Auxiliar.Carta.Pop();//le quito la carta al jugador y le resto los puntos
                        Auxiliar.Puntos -= cartas.Valor;

                        Jugador.Carta.Push(cartas.Nombre, cartas.Valor);
                        Jugador.Puntos += cartas.Valor;
                        //Aqui se elimina el poder de la pila de poderes del jugador.
                        Jugador.Poder.Pop();
                        YaUsoElPoder = true;
                        break;
                    case 3:
                        YaUsoElPoder = true;
                        break;
                }
            }
        }
    }
}



