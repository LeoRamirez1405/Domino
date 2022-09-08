using Logica.domino.dll;
using System.Reflection;
using System.Data;
class Program
{
    static string eliminar ="Logica.domino.dll";

    public static void Main()
    {
        //Console.Clear();
        int noJugadores = AsignandoCantidadJugadores();
        bool juegoEnEquipo = ChequeandoJuegoEnEquipo(noJugadores);
        DesarrollarModo(CreandoModo(noJugadores , juegoEnEquipo), juegoEnEquipo);
    }

#region "Asignando la cantidad de jugadores"
    public static int AsignandoCantidadJugadores()
    {
        System.Console.WriteLine("Con cuantos jugadores desea jugar?: ");
        int noJug = int.Parse(Console.ReadLine());
        bool equipo = false;
        if(noJug <= 1 || noJug > 4)
        {
            noJug = 2;
            System.Console.WriteLine("Se escogio la cantidad de jugadores por defecto. Cant de jugadores : {0} ", 2);
            // System.Console.WriteLine();
        }
        System.Console.WriteLine();
        return noJug;
    }
#endregion

#region "Chequeando si se juega en equipo"
    public static bool ChequeandoJuegoEnEquipo(int noJug)
    {
        bool equipo = false;
        if(noJug > 2 && noJug % 2 == 0)
        {
            System.Console.WriteLine("Desea jugar en equipo: Si - No");
            string resp = Console.ReadLine();
            if (resp.ToLower() == "si") equipo = true;
        }
        System.Console.WriteLine();
        return equipo;
    }
#endregion

#region "Creando modo"
    public static IModo CreandoModo(int noJug, bool equipo)
    {
        System.Console.WriteLine("Qué modo desea jugar ?: ");
        System.Console.WriteLine("1. Amistoso");
        System.Console.WriteLine("2. Hasta X");
        System.Console.WriteLine("3. Match");
        int respuesta = int.Parse(Console.ReadLine());
       
        if (respuesta == 2)
        {
            System.Console.WriteLine("Hasta cuantos puntos desea jugar ?");
            int result = int.Parse(Console.ReadLine());
            if (result > 0) 
            {
                System.Console.WriteLine();
                return new HastaX(result, noJug, equipo);
            }
            else 
            {
                System.Console.WriteLine("Se escogio la cantidad de puntos a terminar por defecto. Cantidad de puntos a terminar : {0}", 100);
                System.Console.WriteLine();
                return new HastaX(100, noJug, equipo);
            }
        }
        else if (respuesta == 3)
        {
            System.Console.WriteLine("Hasta cuantos partidos desea jugar ?");
            int result = int.Parse(Console.ReadLine());
            System.Console.WriteLine();
            return result > 0 ? new Match(result, noJug, equipo) : new Match(2, noJug, equipo);
        }
        
        System.Console.WriteLine();
        //Aqui llega si la respuesta es 1 o si la respuesta no es valida
        return new Amistoso(noJug, equipo);
    }
#endregion

#region Crear Arbitro
    static Arbitro CrearArbitro(int cantJugadores, IDomino domino, bool EnEquipo)//crear un arbitro
    {
        IReglas reglas = IniciaRegla(cantJugadores, domino, EnEquipo);
        List<Jugador> jugadores = IniciaJugadores(cantJugadores, reglas, domino);
        return new Arbitro(cantJugadores, EnEquipo, reglas, jugadores);//provisional 
    }
#endregion

#region "Escogiendo hasta que doble jugar
    public static int EscogiendoHastaQueDobleJugar(IDomino domino)
    {
        System.Console.WriteLine("Hasta que doble desea jugar (9 máx)?: ");
        int FichasDomino;
        try
        {
            FichasDomino = int.Parse(Console.ReadLine());
            if(FichasDomino > domino.maxFichas()  || FichasDomino < 6) 
            {
                FichasDomino = domino.maxFichas();
                System.Console.WriteLine("Se escogio el doble maximo por defecto. Doble Maximo : {0} ", domino.maxFichas());
                System.Console.WriteLine();
            }
        } catch{FichasDomino = domino.maxFichas();}

    System.Console.WriteLine();
        return FichasDomino;
    }
#endregion

#region "Escogiendo la cantidad de fichas por jugador"
    public static int EscogiendoCantidadFichasPorJugador(int FichasDomino)
    {
        System.Console.WriteLine("Con cuantas fichas por jugador?: ");
        int noFichPorJug;
        try
        {
            noFichPorJug = int.Parse(Console.ReadLine());
            if(noFichPorJug > FichasDomino+1  || noFichPorJug < 2) 
            {
                System.Console.WriteLine("Se escogio la cantidad de fichas por defecto. Cantidad de fichas : {0} ", FichasDomino + 1);
                noFichPorJug = FichasDomino+1;
            }
        } catch{noFichPorJug = FichasDomino+1;}

        System.Console.WriteLine();
        return noFichPorJug;
    }
#endregion

#region Reglas
    static IReglas IniciaRegla(int cantJug, IDomino domino, bool EnEquipo)
    {
        int FichasDomino = EscogiendoHastaQueDobleJugar(domino);
        int noFichPorJug = EscogiendoCantidadFichasPorJugador(FichasDomino);
        
        IReglas reglas = new ClasicoIndividual(cantJug,noFichPorJug,FichasDomino, EnEquipo);//este es el por defecto
        System.Console.WriteLine("Con qué reglas desea jugar ?: ");
        System.Console.WriteLine("1. Clásicas \n2. Personalizadas");

        int reg = int.Parse(Console.ReadLine());
        if(reg == 2)
        {
            IGanador ganador = new Ganador_Clasico(); 
            IProximoJugador proximoJugador = new ProximoJugador_Clasico();
            IValidarJugada validarJugada = new ValidarJugada_Clasica();
            ICalculaPuntos calcula = new CalcularPuntosGanoJugador_Clasico();
            IContarPuntos ContarPuntos = new ContarPuntos_Clasico();
            IAccionDespuesDeLaJugada adj = new AccionDespuesDeLaJugada_Clasico();
            
            System.Console.WriteLine();
            RellenarReglaPersonalizada(ref adj,ref ganador,ref proximoJugador,ref validarJugada,ref calcula,ref ContarPuntos);
            reglas = new Personalizada(cantJug, noFichPorJug, FichasDomino, EnEquipo,ContarPuntos, ganador, proximoJugador,validarJugada, calcula, adj);
        }
        
        System.Console.WriteLine();
        return reglas;   
    }
#endregion

#region Cuando desea que se gane la partida en las reglas personalizadas
    public static void ComoEscogerGanador( ref IGanador ganador)
    {
        Console.WriteLine("Como desea que se gane la partida: ");
        var creando = from t in Assembly.GetAssembly(typeof(IGanador)).GetTypes()
                                    where t.GetInterfaces().Contains(typeof(IGanador))
                                    && t.GetConstructor(Type.EmptyTypes) != null
                                    select Activator.CreateInstance(t) as IGanador;

        int x = 1;
        foreach (var item in creando)
        {
            System.Console.WriteLine(x + ". " + item.ToString()[(eliminar.Length+".Ganador_".Length)..]);
            x++;
        }

        string resp = Console.ReadLine();
        try
        {
            int r = int.Parse(resp);
            int index = 0;
            foreach (var item in creando)
            {
                if(index == r) break;
                ganador = item;
                index ++;
            }
        }
        catch { }
        System.Console.WriteLine();
    }
#endregion

#region Como escoger al proximo jugador en las reglas personalizadas
    public static void ComoEscogerProximoJugador(ref IProximoJugador proximoJugador)
    {
        Console.WriteLine("Como desea que se escoja el próximo jugador: ");

        var creando2 = from t in Assembly.GetAssembly(typeof(IProximoJugador)).GetTypes()
                                    where t.GetInterfaces().Contains(typeof(IProximoJugador))
                                    && t.GetConstructor(Type.EmptyTypes) != null
                                    select Activator.CreateInstance(t) as IProximoJugador;

        int x2 = 1;
        foreach (var item in creando2)
        {
            System.Console.WriteLine(x2+". "+item.ToString()[(eliminar.Length+".ProximoJugador_".Length)..]);
            x2++;
        }

        string resp2 = Console.ReadLine();
        try
        {
            int r = int.Parse(resp2);
            int index = 0;
                foreach (var item in creando2)
                {
                    if(index == r) break;
                    proximoJugador = item;
                    index ++;
                }
        }
        catch { }
        System.Console.WriteLine();
    }
#endregion

#region Cuando desea que una jugada sea valida en las reglas personalizadas
    public static void ValidarJugada(ref IValidarJugada validarJugada)
    {
        Console.WriteLine("Como desea que se valide la jugada: ");        
         var creando3 = from t in Assembly.GetAssembly(typeof(IValidarJugada)).GetTypes()
                                    where t.GetInterfaces().Contains(typeof(IValidarJugada))
                                    && t.GetConstructor(Type.EmptyTypes) != null
                                    select Activator.CreateInstance(t) as IValidarJugada;

            int x3 = 1;
        foreach (var item in creando3)
        {
            System.Console.WriteLine(x3+". "+item.ToString()[(eliminar.Length+".ValidarJugada_".Length)..]);
            x3++;
        }
        string resp3 = Console.ReadLine();
        try
        {
            int r = int.Parse(resp3);
            int index = 0;
                foreach (var item in creando3)
                {
                    if(index == r) break;
                    validarJugada = item;
                    index ++;
                }
        }
        catch { }
        System.Console.WriteLine();
    }      
#endregion

#region Como calcular los puntos que representa una ficha en las reglas personalizadas (ejm los dobles van atener el doble de su valor)
    public static void ComoDeseaHacerLaEquivalenciaEntrePuntosYFichas(ref IContarPuntos ContarPuntos)
    {
        Console.WriteLine("Como desea que se calcule la mano: ");
        ContarPuntos = new ContarPuntos_Clasico();
                 var creando4 = from t in Assembly.GetAssembly(typeof(IContarPuntos)).GetTypes()
                                    where t.GetInterfaces().Contains(typeof(IContarPuntos))
                                    && t.GetConstructor(Type.EmptyTypes) != null
                                    select Activator.CreateInstance(t) as IContarPuntos;

            int x4 = 1;
        foreach (var item in creando4)
        {
            System.Console.WriteLine(x4+". "+item.ToString()[(eliminar.Length+".ContarPuntos_".Length)..]);
            x4++;
        }
        string resp4 = Console.ReadLine();
        try
        {
            int r = int.Parse(resp4);
            int index = 0;
                foreach (var item in creando4)
                {
                    if(index == r) break;
                    ContarPuntos = item;
                    index ++;
                }
        }
        catch { }  
        System.Console.WriteLine();
    }     
#endregion

#region como calcular los puntos del ganador en las reglas personalizadas
    public static void ComoCalcularlospuntosAsignadosAlGanador(ref ICalculaPuntos calcula)
    {
        Console.WriteLine("Como desea que se calculen los puntos del ganador: ");
        calcula = new CalcularPuntosGanoJugador_Clasico();

                 var creando5 = from t in Assembly.GetAssembly(typeof(ICalculaPuntos)).GetTypes()
                                    where t.GetInterfaces().Contains(typeof(ICalculaPuntos))
                                    && t.GetConstructor(Type.EmptyTypes) != null
                                    select Activator.CreateInstance(t) as ICalculaPuntos;

            int x5 = 1;
        foreach (var item in creando5)
        {
            System.Console.WriteLine(x5+". "+item.ToString()[(eliminar.Length+".CalcularPuntosGanoJugador_".Length)..]);
            x5++;
        }
        string resp5 = Console.ReadLine();
        try
        {
            int r = int.Parse(resp5);
            int index = 0;
                foreach (var item in creando5)
                {
                    if(index == r) break;
                    calcula = item;
                    index ++;
                }
        }
        catch { }    
        System.Console.WriteLine();   
    }
#endregion

#region Que hacer despues de cada jugada enlas reglas personalizadas
    public static void QueHacerDespuesdeCadaJugada(ref IAccionDespuesDeLaJugada adj)
    {
        Console.WriteLine("Que desea que pase luego de una jugada?: ");
        adj = new AccionDespuesDeLaJugada_Clasico();

                 var creando6 = from t in Assembly.GetAssembly(typeof(IAccionDespuesDeLaJugada)).GetTypes()
                                    where t.GetInterfaces().Contains(typeof(IAccionDespuesDeLaJugada))
                                    && t.GetConstructor(Type.EmptyTypes) != null
                                    select Activator.CreateInstance(t) as IAccionDespuesDeLaJugada;

            int x6 = 1;
        foreach (var item in creando6)
        {
            System.Console.WriteLine(x6+". "+item.ToString()[(eliminar.Length+".AccionDespuesDeLaJugada_".Length)..]);
            x6++;
        }
        string resp6 = Console.ReadLine();
        try
        {
            int r = int.Parse(resp6);
            int index = 0;
                foreach (var item in creando6)
                {
                    if(index == r) break;
                    adj = item;
                    index ++;
                }
        }
        catch { }   
        System.Console.WriteLine();    
    }
#endregion

#region "Iniciar reglas personalizadas"
    public static void RellenarReglaPersonalizada(ref IAccionDespuesDeLaJugada adj, ref IGanador ganador, ref IProximoJugador proximoJugador,
                                    ref IValidarJugada validarJugada, ref ICalculaPuntos calcula, ref IContarPuntos contarPuntos)
    {
        QueHacerDespuesdeCadaJugada(ref adj);
        ComoEscogerGanador(ref ganador);
        ComoEscogerProximoJugador(ref proximoJugador);
        ValidarJugada(ref validarJugada);
        ComoCalcularlospuntosAsignadosAlGanador(ref calcula);
        ComoDeseaHacerLaEquivalenciaEntrePuntosYFichas(ref contarPuntos);
    }
#endregion
    
#region CrearJugadores
    public static List<Jugador> IniciaJugadores(int noJug, IReglas reglas, IDomino domino)
    {
        int cantJugadores = noJug;
        List<Jugador> ListaJugadores = new List<Jugador>();

        var creando = ImprimirEstrategiasGenerales();
        var creandoSalir = ImprimirEstrategiasJugadaInicial();

        //Aqui se empieza
        List<Ficha[]> fichasJugadores = reglas.Repartir(domino.fichas(reglas.CantFichasPorJugador()), cantJugadores, reglas.CantFichasPorJugador());
        for (int i = 0; i < cantJugadores; i++)
        {
            System.Console.WriteLine($"Escoja la estrategia del jugador {i}");
            string jugString = Console.ReadLine();
            List<int> jug = new List<int>();
            try
            {
                foreach (var item in jugString.Split(' ',StringSplitOptions.RemoveEmptyEntries))
                    jug.Add(int.Parse(item));

                if(jug.Max() >= creando.Count() || jug.Min() < 0)
                    jug.Add(0); 
            }
            catch { jug.Add(0);}
            

            System.Console.WriteLine($"Escoja la estrategia de salir del jugador {i}");
            string jugStringSalir = Console.ReadLine();
            int jugSalir = 0;
            try
            {
                jugSalir = int.Parse(jugStringSalir);
                if(jugSalir >= creandoSalir.Count() || jugSalir < 0)
                    jugSalir = 0;
            }
            catch{ jugSalir = 0;}

            int y = 0;
            string nombre = "";
            List<IEstrategias> estrategiasjugador = new List<IEstrategias>();
            IEstrategiasSalir estrategiasSalirjugador = creandoSalir.ElementAt(jugSalir);

            foreach (var item in jug)
            {
                estrategiasjugador.Add(creando.ElementAt(item));
                nombre += creando.ElementAt(item).ToString()[(eliminar.Length+".E".Length)..];
            }

            ListaJugadores.Add(new Jugador(y,nombre,fichasJugadores[i].ToList(),estrategiasjugador,estrategiasSalirjugador));
            System.Console.WriteLine();
        }

        //System.Console.Clear();
        return ListaJugadores;
    }
#endregion

#region "Imprimeindo estrategias de salir y del jugador en general
    //Esta funcion tiene como objetivo imprimir la lista de las estrategias que cada jugador puede utilizar pero a su vez brinda un 
    public static IEnumerable<IEstrategias> ImprimirEstrategiasGenerales()
    {
        var creando = from t in Assembly.GetAssembly(typeof(IEstrategias)).GetTypes()
                                    where t.GetInterfaces().Contains(typeof(IEstrategias))
                                    && t.GetConstructor(Type.EmptyTypes) != null
                                    select Activator.CreateInstance(t) as IEstrategias;

        int x = 1;

        System.Console.WriteLine("Estas son las estrategias disponibles para cada jugadpr. Usted puede escoger mas de 1 estrategia");
        foreach (var item in creando)
        {
            System.Console.WriteLine(x+". "+item.ToString()[(eliminar.Length+".".Length)..]);
            x++;
        }
        System.Console.WriteLine();
        return creando;
    }

    public static IEnumerable<IEstrategiasSalir> ImprimirEstrategiasJugadaInicial()
    {
        System.Console.WriteLine("Estas son las estrategias disponibles paracada jugador en la primera jugada");
        var creandoSalir = from t in Assembly.GetAssembly(typeof(IEstrategiasSalir)).GetTypes()
                                    where t.GetInterfaces().Contains(typeof(IEstrategiasSalir))
                                    && t.GetConstructor(Type.EmptyTypes) != null
                                    select Activator.CreateInstance(t) as IEstrategiasSalir;
        int xSalir = 1;
        foreach (var item in creandoSalir)
        {
            System.Console.WriteLine(xSalir+". "+item.ToString()[(eliminar.Length+".ES".Length)..]);
            xSalir++;
        } 
        System.Console.WriteLine();

        return creandoSalir;
    }
#endregion

#region Crear fichas
    public static IDomino IniciaDomino()
    {
        IDomino domino = new Doble9();
        System.Console.WriteLine("Con qué domino desea jugar ?: ");
        System.Console.WriteLine("1. Normal (números)\n2. Emojis");
        try{
            int dom = int.Parse(Console.ReadLine());
            if(dom == 1)
                {domino = new Doble9();}
            else if(dom == 2)
                {domino = new Emojis();}
        }
        catch{ domino = new Doble9();}

        System.Console.WriteLine();
        return domino;
    }
#endregion

#region "Desarrollar el modo escogido"
    public static void DesarrollarModo(IModo modo, bool equipo)
    {
        (int, List<int>) ganador = (-1, null);
        while (!modo.TerminoModo(ganador.Item1, ganador.Item2))
        {
            IDomino domino = IniciaDomino();
            Arbitro arbitro = CrearArbitro(modo.CantidadJugadores, domino, equipo);
            bool numJugada = true;//dice si es la primera jugada o no

            while(!arbitro.TerminoPartida())
            {
                System.Console.WriteLine();
                Ficha fichaActual = arbitro.Jugar(numJugada);
                if(fichaActual is null)
                    System.Console.WriteLine("El jugador "+arbitro.GetJugadores()[arbitro.JugadorActual].nombre+" no lleva.");
                else 
                {   
                    System.Console.WriteLine("Jugó el jugador {0} ", arbitro.GetJugadores()[arbitro.JugadorActual].nombre);//dice el jugador que va a jugar
                    arbitro.ImprimirMesa();
                }
                numJugada = false;
            }
            System.Console.WriteLine();
            foreach (var jugador in arbitro.GetJugadores())
            {
                System.Console.WriteLine(jugador.nombre);
                foreach (var ficha in jugador.fichas)
                {
                    System.Console.Write(ficha+" ");
                }
                System.Console.WriteLine("\n");
            }

            (int, List<int>) resulPartida = arbitro.GetGanador();//da el ganador y una lista con los puntos de cada jugador
            ganador = resulPartida;//es el ganador
            modo.TerminoUnaPratida(resulPartida.Item1, resulPartida.Item2);
            string stringEquipo = equipo ? "equipo" : "jugador";
            if (resulPartida.Item1 == -1)
                System.Console.WriteLine("Hubo empate");
            else
            {
                System.Console.WriteLine("El ganador de la partida es el {0} {1} con {2} puntos", stringEquipo, arbitro.GetJugadores()[resulPartida.Item1].nombre , resulPartida.Item2[resulPartida.Item1]);
            }
            System.Console.WriteLine();
        }
        
        (int, int) ganadorModo = modo.GetGanador(equipo);
        string stringEquipoModo = equipo ? "equipo" : "jugador";
        if (ganadorModo.Item1 == -1)
            System.Console.WriteLine("Hubo empate");
        else
            System.Console.WriteLine("El ganador del Modo es el {0} {1} con {2} puntos", stringEquipoModo, ganadorModo.Item1, ganadorModo.Item2);
        System.Console.WriteLine();
    }
#endregion
}