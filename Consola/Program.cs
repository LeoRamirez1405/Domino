using Logica.domino.dll;
class Program
{
    public static void Main()
    {
        System.Console.WriteLine("Con cuantos jugadores desea jugar?: ");
        int noJug = int.Parse(Console.ReadLine());
        
        #region "Modo"
        System.Console.WriteLine("Qué modo desea jugar ?: ");
        IModo modo = null;
        System.Console.WriteLine("1. Amistoso");
        System.Console.WriteLine("2. Hasta X");
        System.Console.WriteLine("3. Match");
        int respuesta = int.Parse(Console.ReadLine());
        if(respuesta == 1)
        {
            modo = new Amistoso(noJug);
        }
        else if(respuesta == 2)
        {
            System.Console.WriteLine("Hasta cuantos puntos desea jugar ?");
            int result = int.Parse(Console.ReadLine());
            if(result > 0) modo = new HastaX(result,noJug);
            else modo = new HastaX(100,noJug);
        }
        else if(respuesta == 3)
        {
            System.Console.WriteLine("Hasta cuantos partidos desea jugar ?");
            int result = int.Parse(Console.ReadLine());
            if(result > 0) modo = new Match(result,noJug);
            else modo = new Match(2,noJug);
        }
        else
        {
           modo = new Amistoso(noJug); 
        }
        #endregion 
        
        (int,int) ganador = modo.Gana();
        System.Console.WriteLine($"El ganador es el jugador {ganador.Item1} con {ganador.Item2} puntos.");
    }
}