namespace Logica.domino.dll;
public class Personalizada : ClaseComunReglas, IReglas
{
    IRepartir repartir = new Repartir_Clasico();
    IContarPuntos ContarPuntos;
    IFinalizarJugada finalizarPartida;
    IGanador ganador;
    IProximoJugador proximoJugador;
    IValidarJugada validarPartida;
    ICalculaPuntos calcula;
    IAccionDespuesDeLaJugada adj;

    public Personalizada(int cantJugadores, int cantFichas, int valorMaxFichas, IContarPuntos ContarPuntos,IGanador ganador,IProximoJugador proximoJugador,
                         IValidarJugada validarPartida,ICalculaPuntos calcula, IAccionDespuesDeLaJugada adj) : base(cantFichas, cantJugadores, valorMaxFichas) 
    {
        this.ganador = ganador ;
        this.proximoJugador = proximoJugador ;
        this.validarPartida = validarPartida ;
        this.calcula = calcula ;
        this.ContarPuntos = ContarPuntos;
        this.adj = adj;

    }//aqui se deberia incializar de con valores

    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<IJugar> jugadores)
    {
        adj.AccionDespuesDeLaJugada(jugadorActual, huboJugada, izquierda, derecha, ref puntosPorJugador, ref jugadores);
    }
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {
        return calcula.CalcularPuntosGanoJugador(jugadorGanador, puntosPorJugador);
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
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos)
    {
        return ganador.Ganador(definenGanador, cantidadJugadores, contarPuntos);
    }
    public int ProximoJugador(int jugadorActual, int totalJugadores)
    {
        return proximoJugador.ProximoJugador(jugadorActual, totalJugadores);
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