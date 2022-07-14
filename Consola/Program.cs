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
#endregion

        int ganador = -1;
        while (!modo.TerminoModo(ganador, null))
        {
            IDomino domino = IniciaDomino();
            Arbitro arbitro = CrearArbitro(modo.CantidadJugadores, domino, equipo);
            int numJugada = 1;

            while(!arbitro.TerminoPartida())
            {
                System.Console.WriteLine(arbitro.JugadorActual);//dice el jugador que va a jugar
                ImprimirFichasJugador(arbitro.GetFichasJugador());// se imprimen las fichas del jugador q va a jugar arbitro.GetFichasJugador() : me da la Mano del jugador actual

                arbitro.Jugar(numJugada == 1);
                ImprimirTablero(arbitro.GetTablero);

                numJugada++;
            }

            (int, List<int>) resulPartida = arbitro.GetGanador();//da el ganador y una lista con los puntos de cada jugador
            ganador = resulPartida.Item1;//es el ganador
        }

        //(int, int) ganador = modo.Gana(equipo);
        //if (ganador.Item1 == -1) System.Console.WriteLine("El juego quedó empatado");
        //System.Console.WriteLine($"El ganador es el jugador {ganador.Item1} con {ganador.Item2} puntos.");
    }


#region Crear Arbitro
    static Arbitro CrearArbitro(int cantJugadores, IDomino domino, bool EnEquipo)//crear un arbitro
    {
        IReglas reglas = IniciaRegla(cantJugadores, domino, EnEquipo);
        List<Jugador> jugadores = IniciaJugadores(cantJugadores, reglas, domino);
        return new Arbitro(cantJugadores, EnEquipo, reglas, jugadores);//provisional 
    }
#endregion

#region Reglas
    public static IReglas IniciaRegla(int cantJug, IDomino domino, bool EnEquipo)
    {
        System.Console.WriteLine("Hasta que doble desea jugar (9 máx)?: ");
        int FichasDomino;
        try
        {
            FichasDomino = int.Parse(Console.ReadLine());
            if(FichasDomino > domino.maxFichas()  || FichasDomino < 6) 
                FichasDomino = domino.maxFichas();
        } catch{FichasDomino = domino.maxFichas();}
        System.Console.WriteLine("Con cuantas fichas por jugador?: ");
        int noFichPorJug;
        try
        {
            noFichPorJug = int.Parse(Console.ReadLine());
            // if(noFichPorJug > FichasDomino+1  || noFichPorJug < 2) 
                // noFichPorJug = FichasDomino+1;
        } catch{noFichPorJug = FichasDomino+1;}
        
        IReglas reglas = null;
        System.Console.WriteLine("Con qué reglas desea jugar ?: ");
        System.Console.WriteLine("1. Clásicas \n2. Quincena\n3. Personalizadas");

        int reg = int.Parse(Console.ReadLine());
        if(reg == 1)
            {reglas = new ClasicoIndividual(cantJug,noFichPorJug,FichasDomino, EnEquipo);}
        if(reg == 2)
            {reglas = new Quincena(cantJug,noFichPorJug,FichasDomino, EnEquipo);}
        if(reg == 3)
        {
            //IRepartir repartir = new Repartir_Clasico();
            IGanador ganador = new Ganador_Clasico(); 
            IProximoJugador proximoJugador = new ProximoJugador_Clasico();
            IValidarJugada validarPartida = new ValidarJugada_Clasica();
            ICalculaPuntos calcula = new CalcularPuntosGanoJugador_Clasico();
            IContarPuntos ContarPuntos = new ContarPuntos_Clasico();
            IAccionDespuesDeLaJugada adj = new AccionDespuesDeLaJugada_Clasico();
            RellenarReglaPersonalizada(ref adj,ref ganador,ref proximoJugador,ref validarPartida,ref calcula,ref ContarPuntos);
            reglas = new Personalizada(cantJug, noFichPorJug, FichasDomino, EnEquipo,ContarPuntos, ganador, proximoJugador,validarPartida, calcula, adj);
        }
        else 
            {reglas = new ClasicoIndividual(cantJug,noFichPorJug,FichasDomino, EnEquipo);}

        System.Console.Clear();
        return reglas;   
    }
#endregion

#region "Iniciar regls personalizadas"
    public static void RellenarReglaPersonalizada(ref IAccionDespuesDeLaJugada adj, ref IGanador ganador, ref IProximoJugador proximoJugador,
                                    ref IValidarJugada validarPartida, ref ICalculaPuntos calcula, ref IContarPuntos ContarPuntos)
    {
        Console.WriteLine("Como desea que se gane la partida: ");
        Console.WriteLine("1. Clásico\n2. Inverso al Clásico\n3. Estilo Quincena\n4. Si se tranca se acumulan los puntos");
        string resp = Console.ReadLine();
        ganador = new Ganador_Clasico();

        try
        {
            int r = int.Parse(resp);
            if (r == 2) { ganador = new Ganador_Inverso(); }
            if (r == 3) { ganador = new Ganador_Quincena(); }
            if (r == 4) { ganador = new Ganador_SiEmpataAcumulaSusPuntos(); }
        }
        catch { }



       proximoJugador = new ProximoJugador_Clasico();

        Console.WriteLine("Como desea que se escoja el próximo jugador: ");
        Console.WriteLine("1. Clásico\n2. Aleatorio");
        resp = Console.ReadLine();
        try
        {
            int r = int.Parse(resp);
            if (r == 2) { proximoJugador = new ProximoJugador_Aleatorio(); }
        }
        catch { }

        //validarPartida = new ValidarJugada_Clasico();

        Console.WriteLine("Como desea que se valide la jugada: ");
        Console.WriteLine("1. Clásico(si las fichas son iguales )\n2. Si son menores o iguales\n3. Si son mayores o iguales");
        resp = Console.ReadLine();
        try
        {
            int r = int.Parse(resp);
            if (r == 2) { validarPartida = new ValidarJugada_Menor(); }
            if (r == 3) { validarPartida = new ValidarJugada_Mayor(); }
        }
        catch { }

        ContarPuntos = new ContarPuntos_Clasico();

        Console.WriteLine("Como desea que se calcule la mano: ");
        Console.WriteLine("1. Clásico\n2. Los dobles valen dobles\n3. Los puntos se multiplican por la cantidad de fichas en mano");
        resp = Console.ReadLine();
        try
        {
            int r = int.Parse(resp);
            if (r == 2) { ContarPuntos = new ContarPuntos_DobleDoble(); }
            if (r == 3) { ContarPuntos = new ContarPuntos_ManoDura(); }
        }
        catch { }


        calcula = new CalcularPuntosGanoJugador_Clasico();

        Console.WriteLine("Como desea que se calculen los puntos del ganador: ");
        Console.WriteLine("1. Clásico(suma todos menos el de él)\n2. Solo el de él\n3. El promedio de todo\n4.El total");
        resp = Console.ReadLine();
        try
        {
            int r = int.Parse(resp);
            if (r == 2) { calcula = new CalcularPuntosGanoJugador_SoloYo(); }
            if (r == 3) { calcula = new CalcularPuntosGanoJugador_Comunista(); }
            if (r == 4) { calcula = new CalcularPuntosGanoJugador_Capitalista(); }
        }
        catch { }

        adj = new AccionDespuesDeLaJugada_Quincena();

        Console.WriteLine("Que desea que pase luego de una jugada?: ");
        Console.WriteLine("1. Clásico(nada)\n2.Si pasa el jugador se invierte la mesa\n3. Si es multipo de 5 la suma de las cabezas se reciben dichos puntos");
        resp = Console.ReadLine();
        try
        {
            int r = int.Parse(resp);
            if (r == 2) { adj = new AccionDespuesDeLaJugada_InvertirJugadores(); }
            if (r == 3) { adj = new AccionDespuesDeLaJugada_Quincena(); }
        }
        catch { }

        System.Console.Clear();
    }
#endregion

#region CrearJugadores
    public static List<Jugador> IniciaJugadores(int noJug, IReglas reglas, IDomino domino)
    {
        List<Jugador> ListaJugadores = new List<Jugador>();
        int cantJugadores = noJug;

        System.Console.WriteLine("1. Aleatorio");
        System.Console.WriteLine("2. Botagorda");
        System.Console.WriteLine("3. Leo");
        System.Console.WriteLine("4. Matemático");
        System.Console.WriteLine("5. Humano");
        System.Console.WriteLine("6. Pasador");
        //Aqui se empieza
        List<Ficha[]> fichasJugadores = reglas.Repartir(domino.fichas(reglas.CantFichasPorJugador()), cantJugadores, reglas.CantFichasPorJugador());
        for (int i = 0; i < cantJugadores; i++)
        {
            System.Console.WriteLine($"Escoja la estrategia del jugador {i}");
            int jug = int.Parse(Console.ReadLine());

            while (true)
            {
                if (jug == 1) { ListaJugadores.Add(new Aleatorio(i,$"Aleatorio{i}",fichasJugadores[i].ToList())); break; }
                else if (jug == 2) { ListaJugadores.Add(new Botagorda(i,$"Botagorda{i}",fichasJugadores[i].ToList())); break; }
                else if (jug == 3) { ListaJugadores.Add(new Leo(i,$"Leo{i}",fichasJugadores[i].ToList())); break; }
                else if (jug == 4) { ListaJugadores.Add(new Matematico(i,$"Matemático{i}",fichasJugadores[i].ToList())); break; }
                else if (jug == 5) { ListaJugadores.Add(new Humano(i,$"Humano{i}",fichasJugadores[i].ToList())); break; }
                else if (jug == 6) { ListaJugadores.Add(new Pasador(i,$"Pasador{i}",fichasJugadores[i].ToList())); break; }
                else
                {
                    System.Console.WriteLine("Dato incorrecto. Vuelva a intentarlo: ");
                    jug = int.Parse(Console.ReadLine());
                }
            }
        }

        System.Console.Clear();
        return ListaJugadores;
    }
#endregion

#region Imprimir Tablero
    public static void ImprimirTablero(Ficha[,] tablero)
    {
        for(int i = 0; i < tablero.GetLength(0); i++)
        {
            for(int j = 0; j < tablero.GetLength(1); j++)
                System.Console.WriteLine(tablero[i,j]);
        }
    }
#endregion

#region Crear fichas
    public static IDomino IniciaDomino()
    {
        IDomino domino = new Doble9();
        System.Console.WriteLine("Con qué domino desea jugar ?: ");
        System.Console.WriteLine("1. Normal (números)\n2. Emojis\n");
         int dom = int.Parse(Console.ReadLine());
            if(dom == 1)
                {domino = new Doble9();}
            else if(dom == 2)
                {domino = new Emojis();}
                
        return domino;
    }
#endregion

#region Imprimir fichas jugador
    public static void ImprimirFichasJugador(List<Ficha> mano)
    {
        foreach (Ficha ficha in mano)
        {
            System.Console.Write(ficha.ToString() + " ");
        }
        System.Console.WriteLine();
    }
#endregion
}