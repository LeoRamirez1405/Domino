using Logica.domino.dll;
class Program
{
    public static void Main()
    {
        IDomino domino = new Doble9();
        System.Console.WriteLine("Con qué domino desea jugar: ");
        System.Console.WriteLine("1. Normal (números)\n2. Emojis\n");
        int dom = int.Parse(Console.ReadLine());
            if(dom == 1)
                {domino = new Doble9();}
            else if(dom == 2)
                {domino = new Emojis();}

        System.Console.WriteLine("Con cuantos jugadores desea jugar (2 - 4): ");
        int cantJugadores = int.Parse(Console.ReadLine());
        List<Jugador> ListaJugadores = new List<Jugador>();
       
       //Aqui se empieza
        for (int i = 0; i < cantJugadores; i++)
        {
            System.Console.WriteLine($"Escoja la estrategia del jugador {i}");
            System.Console.WriteLine("1. Aleatorio");
            System.Console.WriteLine("2. Botagorda");
            System.Console.WriteLine("3. Leo");
            System.Console.WriteLine("4. Matemático");

            int jug = int.Parse(Console.ReadLine());
            while(true)
            {
                if(jug == 1) {ListaJugadores.Add(new Aleatorio()); break;}
                else if(jug == 2) {ListaJugadores.Add(new Botagorda()); break;}
                else if(jug == 3) {ListaJugadores.Add(new Leo()); break;}
                else if(jug == 4) {ListaJugadores.Add(new Matemático()); break;}
                else
                {
                    System.Console.WriteLine("Dato incorrecto. Vuelva a intentarlo: ");
                    jug = int.Parse(Console.ReadLine());
                }
            }
        }

        ClasicoIndividual reglas = new ClasicoIndividual(cantJugadores,9);

        Amistoso amistoso = new Amistoso(ListaJugadores,reglas,domino);
        (int,int) ganador = amistoso.Gana();

        System.Console.WriteLine($"El ganador es el jugador {ganador.Item1} con {ganador.Item2} puntos.");
    }
}