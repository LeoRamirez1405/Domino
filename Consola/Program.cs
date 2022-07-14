using Logica.domino.dll;
class Program
{
    public static void Main()
    {
         Console.Clear();
        System.Console.WriteLine("Con cuantos jugadores desea jugar?: ");
        int noJug = int.Parse(Console.ReadLine());
        
        bool equipo = false;

        if (noJug > 2 && noJug % 2 == 0)
        {
            System.Console.WriteLine("Desea jugar en equipo: Si - No");
            string resp = Console.ReadLine();
            if (resp.ToLower() == "si") equipo = true;
        }
        IModo modo = null;

        #region "Modo"
        System.Console.WriteLine("Qué modo desea jugar ?: ");
        System.Console.WriteLine("1. Amistoso");
        System.Console.WriteLine("2. Hasta X");
        System.Console.WriteLine("3. Match");
        int respuesta = int.Parse(Console.ReadLine());
        if (respuesta == 1)
        {
            modo = new Amistoso(noJug, equipo);
        }
        else if (respuesta == 2)
        {
            System.Console.WriteLine("Hasta cuantos puntos desea jugar ?");
            int result = int.Parse(Console.ReadLine());
            if (result > 0) modo = new HastaX(result, noJug, equipo);
            else modo = new HastaX(100, noJug, equipo);
        }
        else if (respuesta == 3)
        {
            System.Console.WriteLine("Hasta cuantos partidos desea jugar ?");
            int result = int.Parse(Console.ReadLine());
            if (result > 0) modo = new Match(result, noJug, equipo);
            else modo = new Match(2, noJug, equipo);
        }
        else
        {
            modo = new Amistoso(noJug, equipo);
        }
        #endregion*/

        (int, int) ganador = modo.Gana(equipo);
        if (ganador.Item1 == -1) System.Console.WriteLine("El juego quedó empatado");
        else if(!equipo)
            System.Console.WriteLine($"El ganador es el jugador {ganador.Item1} con {ganador.Item2} puntos.");
        else
            System.Console.WriteLine($"El ganador es el equipo {ganador.Item1%2} con {ganador.Item2} puntos.");
    }
}