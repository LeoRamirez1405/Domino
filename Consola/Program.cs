using Logica.domino.dll;
class Program
{
    public static void Main()
    {
        IDomino domino = new Doble9();
        // System.Console.WriteLine("Con qué domino desea jugar: ");
        // System.Console.WriteLine("1. Normal (números)\n2. Emojis\n");
        // int dom = int.Parse(Console.ReadLine());
        //     if(dom == 1)
        //         {domino = new Doble9();}
        //     else if(dom == 2)
        //         {domino = new Emojis();}

        ClasicoIndividual reglas = new ClasicoIndividual(4,10,9);
        
        Amistoso amistoso = new Amistoso(reglas,domino);
        (int,int) ganador = amistoso.Gana();
        System.Console.WriteLine($"El ganador es el jugador {ganador.Item1} con {ganador.Item2} puntos.");


        //// Metodo para imprimir la mano del jugador 
        // void imprimirMano(List<Ficha> mano)
        // {
        //     foreach(Ficha ficha in mano)
        //     {
        //         System.Console.Write(ficha.ToString()+" "); 
        //     }
        //     System.Console.WriteLine();
        // }

        // void imprimirManoJugadores(List<IJugar> jugadores, List<Ficha> mano)
        // {
        //     int i = 0;
        //     foreach (var jug in jugadores)
        //     {
        //         foreach(Ficha ficha in jug.ObtenerFichas())
        //         {
        //             System.Console.Write(ficha.ToString()+" "); 
        //         }
        //         System.Console.WriteLine();
        //         i++;
        //     }
        // }
        
    }
}