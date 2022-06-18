using Logica.domino.dll;
class Program
{
    public static void Main()
    {
        IDomino domino = new Doble9();
        System.Console.WriteLine("Con qué domino desea jugar ?: ");
        System.Console.WriteLine("1. Normal (números)\n2. Emojis\n");
        int dom = int.Parse(Console.ReadLine());
            if(dom == 1)
                {domino = new Doble9();}
            else if(dom == 2)
                {domino = new Emojis();}

        Console.Clear();
        ClasicoIndividual reglas = new ClasicoIndividual();
        
        System.Console.WriteLine("Qué modo desea jugar ?: ");
        IModo modo = null;
        System.Console.WriteLine("1. Amistoso");
        System.Console.WriteLine("2. Hasta X");
        System.Console.WriteLine("3. Match");
        int respuesta = int.Parse(Console.ReadLine());

        if(respuesta == 1)
        {
            modo = new Amistoso(reglas,domino);
        }
        if(respuesta == 2)
        {
            System.Console.WriteLine("Hasta cuantos puntos desea jugar ?");
            int result = int.Parse(Console.ReadLine());
            if(result > 0) modo = new HastaX(result,reglas,domino);
            else modo = new HastaX(100,reglas,domino);
        }
        if(respuesta == 3)
        {
            System.Console.WriteLine("Hasta cuantos partidos desea jugar ?");
            int result = int.Parse(Console.ReadLine());
            if(result > 0) modo = new Match(result,reglas,domino);
            else modo = new Match(2,reglas,domino);
        }

        (int,int) ganador = modo.Gana();
        System.Console.WriteLine($"El ganador es el jugador {ganador.Item1} con {ganador.Item2} puntos.");
    }
}