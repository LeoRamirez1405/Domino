namespace Logica.domino.dll;
public class Personalizada : ClaseComunReglas, IReglas
{
    IRepartir repartir = new Repartir_Clasico();
    IContarPuntos ContarPuntos;
    IFinalizarJugada finalizarPartida = new FinalizarJugada_Clasico();
    IGanador ganador;
    IProximoJugador proximoJugador;
    IValidarJugada validarPartida;
    ICalculaPuntos calcula;
    IAccionDespuesDeLaJugada adj;
    public bool equipo => base.equipo;
    public bool invertido => base.invertido;
    public ICalculaPuntos calculaPuntos => calcula;


    public Personalizada(int cantJugadores, int cantFichas, int valorMaxFichas, bool equipo ,IContarPuntos ContarPuntos,IGanador ganador,IProximoJugador proximoJugador,
                         IValidarJugada validarPartida,ICalculaPuntos calcula, IAccionDespuesDeLaJugada adj) : base(cantFichas, cantJugadores, valorMaxFichas,equipo) 
    {
        this.ganador = ganador ;
        this.proximoJugador = proximoJugador ;
        this.validarPartida = validarPartida ;
        this.calcula = calcula ;
        this.ContarPuntos = ContarPuntos;
        this.adj = adj;

    }//aqui se deberia incializar de con valores

    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<Jugador> jugadores, ref bool invertido)
    {
        adj.AccionDespuesDeLaJugada(jugadorActual, huboJugada, izquierda, derecha, ref puntosPorJugador, ref jugadores, ref invertido);
    }
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador,bool equipo)
    {
        return calcula.CalcularPuntosGanoJugador(jugadorGanador, puntosPorJugador,equipo);
    }
    public int CantFichasPorJugador()
    {
        return cantFichasPorJugador;
    }
    public int CantFichasTotalJuego()
    {
        return CantFichasPorJugador() * CantidadJugadores;
    }
    public bool FinalizoPartida(int cantFichasJugadorActual, int turnosSinJugar, List<int> puntosPorJugador)
    {
        return finalizarPartida.FinalizoPartida(cantFichasJugadorActual, turnosSinJugar, puntosPorJugador);
    }
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos,ICalculaPuntos calculaPuntos,bool equipo, List<int> puntosPorJugador,List<Jugador> jugadores)
    {
        return ganador.Ganador(definenGanador, cantidadJugadores, contarPuntos,calculaPuntos,equipo, puntosPorJugador,jugadores);
    }
    public int ProximoJugador(int jugadorActual, int totalJugadores,bool invertido)
    {
        return proximoJugador.ProximoJugador(jugadorActual, totalJugadores, invertido);
    }
    public List<Ficha[]> Repartir(List<Ficha> todasFichas, int CantidadJugadores, int cantFichasPorJugador)
    {
        return repartir.Repartir(todasFichas, CantidadJugadores, base.cantFichasPorJugador);
    }
    public bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano)
    {
        return validarPartida.ValidarJugada(fichaMesa, fichaMano);
    }

    public int CantidadJugadores => base.cantJugadoresEnJuego;
    public (int, int) DimensionTablero => (5, 8);//5 filas 8 columnas
    public IContarPuntos contarPuntos => ContarPuntos;
}