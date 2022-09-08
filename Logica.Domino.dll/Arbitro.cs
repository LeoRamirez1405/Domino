namespace Logica.domino.dll;
public class Arbitro
{
    int cantJugadores;//la cant de jugadores debe estar en las reglas
    List<Jugador> jugadores;
    List<int> puntosPorJugadores;
    bool equipo;
    Mesa mesa = new Mesa();

    public void ImprimirMesa()
    {
        mesa.imprimirMesa();
    }
    IReglas reglas;
    int jugadorActual;
    int turnosSinJugar;


    public Arbitro(int cantJug, bool equipo, IReglas reglas, List<Jugador> jugadores)
    {
        this.equipo = equipo;
        this.puntosPorJugadores = new int[cantJug].ToList();
        this.cantJugadores = cantJug;
        this.reglas = reglas;
        this.jugadores = jugadores;
     
        this.turnosSinJugar = 0;
    }

    public List<Jugador> GetJugadores() => jugadores;

    public int JugadorActual => this.jugadorActual;
    
    
//Sabiendo si es la priemra jugada o no esta funcion es la encargada de ejecutar la jugada del proximo jugador que pasa a ser el jugador actual
    public Ficha Jugar(bool esLaPrimeraJugada)
    {
        (Ficha,int) jugada;
        bool huboJugada = true;
        if(esLaPrimeraJugada)
        {
            jugadorActual = reglas.JugadorInicial();
            jugada.Item1 = ((IEstrategiasSalir)this.jugadores[jugadorActual]).Jugar(ref jugadores[jugadorActual].fichas,this.reglas);
            mesa.AddDer(jugada.Item1,jugadorActual);
            return jugada.Item1;
        }
        else
        {
            jugadorActual = reglas.ProximoJugador(jugadorActual, cantJugadores,((ClaseComunReglas)reglas).invertido);//dice a que jugador le toca jugar
            //esta variable (jugada) almacena la ficha que va jugar el jugador y por que parte de la mesa va a  jugar
            jugada = ((IEstrategias)jugadores[jugadorActual]).Jugar(ref jugadores[jugadorActual].fichas, mesa.Extremos().Item1, mesa.Extremos().Item2, this.reglas, jugadorActual);

            if(jugada.Item2 == -1)//si el -1 por convencion es que el jugadore no lleva
            {
                turnosSinJugar ++;
                huboJugada = false;
                jugada.Item1 = null;
            }
            else
            {
                int posParaJugar = jugada.Item2 == 0 ? 0 : 1;
                if(posParaJugar == 0)
                {
                    //En la dos proximas lineas se ajuta la ficha (se crea una nueva) para hacer conicidir el extremo correspondiente de la ficha jugada con la ficha qu esta en la mesa
                    if( jugada.Item1.Abajo  != mesa.recorrido[0].Item1.Arriba )
                        jugada.Item1 = new Ficha(jugada.Item1.Abajo,jugada.Item1.Arriba);
                    mesa.AddIzq(jugada.Item1,jugadorActual);//se coloca la ficha en la mesa
                }
                else if(posParaJugar == 1)
                {
                    //En la dos proximas lineas se ajuta la ficha (se crea una nueva) para hacer conicidir el extremo correspondiente de la ficha jugada con la ficha qu esta en la mesa
                    if( mesa.recorrido[mesa.recorrido.Count-1].Item1.Abajo != jugada.Item1.Arriba )
                        jugada.Item1 = new Ficha(jugada.Item1.Abajo,jugada.Item1.Arriba);
                    mesa.AddDer(jugada.Item1,jugadorActual);//se coloca la ficha en la mesa
                }
                turnosSinJugar = 0;
            }
        }
        
        reglas.AccionDespuesDeLaJugada(jugadorActual, huboJugada, mesa.Extremos().Item1,mesa.Extremos().Item2, ref puntosPorJugadores, ref jugadores, ref ((ClaseComunReglas)reglas).invertido);
        foreach (var item in jugadores)
        {
            //Existe una estrategia que usa las fichas puestas en la mesa hasta el momento para dedcidir la proxima jugada. En l aproxima linea se proporciona la informacion necesarian para esa estrategia
            if(item is IJuegaConMesa) ((IJuegaConMesa)item).juegaConMesa(mesa,mesa.fichaActual_jugador,huboJugada);
        }
        return jugada.Item1;
    }

    public bool TerminoPartida()
    {
        int cantFichasJugadorActual = this.jugadores[jugadorActual].fichas.Count;
        return this.reglas.FinalizoPartida(cantFichasJugadorActual, this.turnosSinJugar, this.puntosPorJugadores);
    }

    public (int, List<int>) GetGanador()
    {
        Dictionary<ParametrosDefinenGanador,object> argumentos = new Dictionary<ParametrosDefinenGanador, object>();
                
        argumentos.Add(ParametrosDefinenGanador.TurnosSinJugar,turnosSinJugar);
        argumentos.Add(ParametrosDefinenGanador.IndexJugadorActual, jugadorActual);

        List<List<Ficha>> fichasJugadores = new List<List<Ficha>>();//En esta lista se guardaran las fichas pertenecientes a cada jugador luego de haber terminado la partida
        foreach(var x in jugadores)
            fichasJugadores.Add(x.fichas);
        argumentos.Add(ParametrosDefinenGanador.FichasJugadores, fichasJugadores);

        if(turnosSinJugar == cantJugadores) 
            argumentos.Add(ParametrosDefinenGanador.SeTrancoElJuego, true);
        else
            argumentos.Add(ParametrosDefinenGanador.SeTrancoElJuego, false);

        return this.reglas.Ganador(argumentos, cantJugadores, reglas.contarPuntos, reglas.calculaPuntos, equipo,puntosPorJugadores,jugadores);
    }
}

