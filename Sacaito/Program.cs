using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sacaito
{
    public class Program
    {
        public static void JuegoPrincipal()
        {
            ListaCircular Lista = new ListaCircular(); // Lista de jugadores
            QuitarCarta quitarCarta = new QuitarCarta(); // Objeto utilizado para cuando el jugador quiere utilizar el poder de robar carta
            Pila PilaCartas = new Pila(); //Pila principal de cartas
            Cartas[] cartas = new Cartas[52]; // Arreglo de las cartas

            //ESTE METODO SE ENCARGA DE BARAJAR LAS CARTAS
            Cartas.CrearBaraja(ref cartas);

            MenuPrincipal();

            int.TryParse(Console.ReadLine(), out int op);

            switch (op)
            {
                case 1:
                    Console.Clear();
                    //Agregamos los 4 jugadores
                    for (int i = 0; i < 4; i++)
                    {
                        Lista.Agregar($"Jugador {i + 1}");
                    }

                    //Metodo para mezclar las cartas
                    Cartas.MezclarBaraja(ref cartas);//le pasamos el objeto original

                    Console.WriteLine("Comienzan a seleccionar Cartas");
                    //Aqui declaramos el jugador y el que sera nuestro primer jugador.
                    Jugadores jugador = Lista.ObtenerPrimerNodo();
                    Jugadores PrimerTurno = new Jugadores();//para alamacenar el primer tirno
                    int ContadorCartas = 0; // Iterador para poder recorrer el arreglo de cartas
                    int Mayor = 0; // Variable para almacenar el valor mas alto.

                    do
                    {
                        //Elegimos el jugador con la carta mas alta.
                        jugador.Carta.Push(cartas[ContadorCartas].Nombre, cartas[ContadorCartas].Valor);
                        jugador.Puntos = cartas[ContadorCartas].Valor;

                        //Comparamos los puntos del juego
                        if (jugador.Puntos > Mayor)
                        {
                            Mayor = jugador.Puntos;
                            PrimerTurno = jugador;
                        }

                        Console.WriteLine($"Nombre: {jugador.Nombre } Valor de la carta {jugador.Carta.Peek().Valor} de {jugador.Carta.Peek().Nombre}");

                        jugador.Carta.Pop();
                        jugador.Puntos = 0;
                        jugador = jugador.Siguiente;

                        ContadorCartas++;

                    } while (jugador != Lista.ObtenerPrimerNodo());

                    Console.WriteLine($"\nPrimer Jugador: {PrimerTurno.Nombre }");
                    PresionaParaContinuar();

                    Lista.InsertarPrimerJugador(PrimerTurno);

                    //Metodo para mezclar las cartas
                    Cartas.MezclarBaraja(ref cartas);

                    //Metodo para mover cartas de un arreglo a la pila.
                    Cartas.MoverCartas(ref cartas, ref PilaCartas);

                    //Empezamos el juego.
                    int JugadoresPorRonda = 0;
                    bool EsReversa = false;
                    Cartas CartaJuego;
                    jugador = PrimerTurno;

                    while (!PilaCartas.EsVacio())
                    {
                        Console.Clear();

                        ///Recorrido normal
                        //Mostramos la informacion del estado de los jugadores , es decir sus puntos y la cantidad de cartas restantes.
                        MostrarInformacionDelJuego(Lista.ObtenerPrimerNodo(), PilaCartas);
                        Console.WriteLine($"\nTurno del Jugador: { jugador.Nombre }");

                        //Sacamos una carta del mazo del juego
                        CartaJuego = PilaCartas.Pop();

                        //si la carta es diferente de 14 (14 = A), almacena la carta en la pila de cartas del jugador.
                        if (CartaJuego.Valor != 14)
                        {
                            jugador.Carta.Push(CartaJuego.Nombre, CartaJuego.Valor); // Agregamos esa carta a la pila de cartas del jugador
                            jugador.Puntos += CartaJuego.Valor; //Sumamos los puntos al jugador

                            ImprimirJugador(jugador, CartaJuego);
                        }
                        else
                        {
                            //si la carta es igual de 14 (14 = A), almacena la carta en la pila de poder del jugador.
                            jugador.Poder.Push(CartaJuego.Nombre, CartaJuego.Valor);
                            ImprimirJugador(jugador, CartaJuego);
                        }
                        //Verificamos si se puede utilizar el poder
                        if (!jugador.Poder.EsVacio())
                        {
                            Console.WriteLine("¿Utilizar el poder disponible?");
                            Console.WriteLine("1.Si\n2.No");
                            int.TryParse(Console.ReadLine(), out int usarPoder);

                            if (usarPoder == 1)
                                quitarCarta.ObtenerCarta(jugador);
                        }

                        //Si los jugadores aun no llegan al final , pasamos al siguiente.
                        if (JugadoresPorRonda < 4 && !EsReversa)
                        {
                            jugador = jugador.Siguiente;
                            JugadoresPorRonda++;

                            if (JugadoresPorRonda == 4)// Si ya estamos en una posicion mas adelante del ultimo jugador entonces debemos volver un paso y poner el juego en reserva 
                            {                           // ya que esto indica que llegamos al ultimo jugador
                                jugador = jugador.Anterior;
                                EsReversa = true;
                            }
                        }
                        else
                        {
                            //Si los jugadores aun no llegan al final , pasamos al anterior por que este es el recorrido inverso.
                            if (JugadoresPorRonda > 0 && EsReversa)
                            {
                                jugador = jugador.Anterior;
                                JugadoresPorRonda--;

                                if (JugadoresPorRonda == 0)// Si ya estamos en una posicion mas adelante del ultimo jugador entonces debemos volver un paso y poner el juego en reserva 
                                {                           // ya que esto indica que llegamos al ultimo jugador
                                    jugador = jugador.Siguiente;
                                    EsReversa = false;
                                }
                            }
                        }
                        PresionaParaContinuar();
                    }

                    ///indica el jugador ganador
                    Jugadores temporal = Lista.ObtenerPrimerNodo();
                    Jugadores ganador = new Jugadores();
                    Mayor = 0;

                    do
                    {
                        if (temporal.Puntos > Mayor)
                        {
                            Mayor = temporal.Puntos;
                            ganador = temporal;
                        }

                        temporal = temporal.Siguiente;

                    } while (temporal != Lista.ObtenerPrimerNodo());

                    Console.WriteLine($"El Ganador Es :{ganador.Nombre } con {ganador.Puntos } puntos");


                    Console.WriteLine("\n\n1.Volver al menu principal");
                    Console.WriteLine("2.Finalizar juego");

                    int.TryParse(Console.ReadLine(), out int Decision);
                    if (Decision == 1)
                        Main(null);
                    else
                        Environment.Exit(0);
                    break;
                default:
                    Environment.Exit(0);
                    break;

            }
            Console.ReadKey();
        }
        public static void Main(string[] args)
        {
            Console.Clear();
            JuegoPrincipal();
        }
        public static void ImprimirJugador(Jugadores jugador, Cartas Carta)
        {
            Console.WriteLine($"Jugador: {jugador.Nombre } Toma la carta: {Carta.Valor } de {Carta.Nombre }");
        }
        /// <summary>
        /// Esta funcion se encarga de mostrar las opciones del Menu Principal.
        /// </summary>
        public static void MenuPrincipal()
        {
            ///MENU PRINCIPAL
            Console.WriteLine("Elija un opcion");
            Console.WriteLine("1.Empezar Juego");
            Console.WriteLine("2.Terminar Juego");
        }
       
        /// <summary>
        /// Esta funcion pausa la pantalla.
        /// </summary>
        public static void PresionaParaContinuar()
        {
            Console.WriteLine("\nPresiona para continuar...");
            Console.ReadKey();
        }
        /// <summary>
        /// Se encarga de mostrar la informacion del progreso del juego
        /// </summary>
        /// <param name="Jugador">Recibe un jugador para poder recorrer la lista</param>
        /// <param name="PilaCartas">Recibe una pila de cartas.</param>
        public static void MostrarInformacionDelJuego(Jugadores Jugador, Pila PilaCartas)
        {
            Console.WriteLine("Progreso:");
            Console.WriteLine($"Cantidad de cartas en la pila:{ PilaCartas.ObtenerCantidadCartas() }");

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"{ Jugador.Nombre } Puntos: { Jugador.Puntos } Cantidad de cartas en pila: { Jugador.Carta.ObtenerCantidadCartas()} Cantidad de poderes disponible : { Jugador.Poder.ObtenerCantidadCartas()}");
                Jugador = Jugador.Siguiente;
            }
        }
    }

}
