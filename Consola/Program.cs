using Logica.domino.dll;
using System.Reflection;
using System.Data;
class Program
{
    static string eliminar ="Logica.domino.dll";
    public static void Main()
    {
        Console.Clear();
        System.Console.WriteLine("Con cuantos jugadores desea jugar?: ");
        int noJug = int.Parse(Console.ReadLine());
        bool equipo = false;
        if(noJug <= 1 || noJug > 4)
        {
            noJug = 2;
            System.Console.WriteLine("Se escogio la cantidad de jugadores por defecto. Cant de jugadores : {0} ", 2);
            System.Console.WriteLine();
        }
        else if (noJug > 2 && noJug % 2 == 0)
        {
            System.Console.WriteLine("Desea jugar en equipo: Si - No");
            string resp = Console.ReadLine();
            if (resp.ToLower() == "si") equipo = true;
        }
        #region "Modo"
        IModo modo = null;
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
            else 
            {
                modo = new HastaX(100, noJug, equipo);
                System.Console.WriteLine("Se escogio la cantidad de puntos a terminar por defecto. Cantidad de puntos a terminar : {0}", 100);
                System.Console.WriteLine();
            }
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


#region Crear Arbitro
    static Arbitro CrearArbitro(int cantJugadores, IDomino domino, bool EnEquipo)//crear un arbitro
    {
        IReglas reglas = IniciaRegla(cantJugadores, domino, EnEquipo);
        List<Jugador> jugadores = IniciaJugadores(cantJugadores, reglas, domino);
        return new Arbitro(cantJugadores, EnEquipo, reglas, jugadores);//provisional 
    }
#endregion

#region Reglas
    static IReglas IniciaRegla(int cantJug, IDomino domino, bool EnEquipo)
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
        
        IReglas reglas = null;
        System.Console.WriteLine("Con qué reglas desea jugar ?: ");
        System.Console.WriteLine("1. Clásicas \n2. Personalizadas");

        int reg = int.Parse(Console.ReadLine());
        if(reg == 1)
            {reglas = new ClasicoIndividual(cantJug,noFichPorJug,FichasDomino, EnEquipo);}
        if(reg == 2)
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

#region "Iniciar reglas personalizadas"
    public static void RellenarReglaPersonalizada(ref IAccionDespuesDeLaJugada adj, ref IGanador ganador, ref IProximoJugador proximoJugador,
                                    ref IValidarJugada validarPartida, ref ICalculaPuntos calcula, ref IContarPuntos ContarPuntos)
    {
        #region Ganador
        Console.WriteLine("Como desea que se gane la partida: ");
        var creando = from t in Assembly.GetAssembly(typeof(IGanador)).GetTypes()
                                    where t.GetInterfaces().Contains(typeof(IGanador))
                                    && t.GetConstructor(Type.EmptyTypes) != null
                                    select Activator.CreateInstance(t) as IGanador;

            int x = 1;
        foreach (var item in creando)
        {
            
            System.Console.WriteLine(x+". "+item.ToString()[(eliminar.Length+".Ganador_".Length)..]);
            x++;
        }
        ganador = new Ganador_Clasico();
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
        #endregion

        #region Proximo Jugador
    Console.WriteLine("Como desea que se escoja el próximo jugador: ");
       proximoJugador = new ProximoJugador_Clasico();
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
        #endregion
        
        #region Validar Jugada

        validarPartida = new ValidarJugada_Clasica();

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
                    validarPartida = item;
                    index ++;
                }
        }
        catch { }       
        #endregion

        #region Calcular Mano
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

        #endregion
       
        #region Calcular Puntos Del Ganador
  
        
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

        #endregion
        
        #region Accion Despues de la Jugada
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


        #endregion
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

// #region Imprimir Tablero
//     public static void ImprimirTablero(Ficha[,] tablero)
//     {
//         for(int i = 0; i < tablero.GetLength(0); i++)
//         {
//             for(int j = 0; j < tablero.GetLength(1); j++)
//             {
//                 if(tablero[i,j] is null) continue;
//                 System.Console.WriteLine(tablero[i,j]);
//             }
//         }
//     }
// #endregion

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