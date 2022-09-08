namespace Logica.domino.dll;
public class ClasicoIndividual : ClaseComunReglas, IReglas
{
    
    protected IRepartir repartir = new Repartir_Clasico();
    protected IContarPuntos ContarPuntos => new ContarPuntos_Clasico();
    protected IFinalizarJugada finalizarPartida = new FinalizarJugada_Clasico();
    protected IGanador ganador = new Ganador_Clasico(); 
    protected IProximoJugador proximoJugador = new ProximoJugador_Clasico();
    protected IAccionDespuesDeLaJugada adj = new AccionDespuesDeLaJugada_Clasico();
    protected IValidarJugada validarPartida = new ValidarJugada_Clasica();
    public bool equipo => base.equipo;
    public bool invertido => base.invertido;
    public ICalculaPuntos calculaPuntos => new CalcularPuntosGanoJugador_Clasico();
    public ClasicoIndividual(int cantJugadores, int cantFichas, int valorMaxFichas,bool equipo) : base(cantFichas,cantJugadores,valorMaxFichas,equipo){}//aqui se deberia incializar de con valores
    public int CantidadJugadores => base.cantJugadoresEnJuego;
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<Jugador> jugadores, ref bool invertido)
    {
        adj.AccionDespuesDeLaJugada(jugadorActual,huboJugada,izquierda,derecha,ref puntosPorJugador,ref jugadores,ref invertido);
    }
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {
        return calculaPuntos.CalcularPuntosGanoJugador(jugadorGanador,puntosPorJugador,equipo);   
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
        return finalizarPartida.FinalizoPartida(cantFichasJugadorActual , turnosSinJugar , puntosPorJugador);
    }
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos, ICalculaPuntos calculaPuntos,bool equipo, List<int> puntosPorJugador,List<Jugador> jugadores)
    {
        return ganador.Ganador(definenGanador, cantidadJugadores, contarPuntos,calculaPuntos,equipo,puntosPorJugador,jugadores);
    }
    public int ProximoJugador(int jugadorActual, int totalJugadores,bool invertido)
    {
        return proximoJugador.ProximoJugador(jugadorActual, totalJugadores, invertido);    
    }
    public List<Ficha[]> Repartir(List<Ficha> todasFichas, int CantidadJugadores, int cantFichasPorJugador)
    {
        return repartir.Repartir(todasFichas,CantidadJugadores,base.cantFichasPorJugador);
    }
    public bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano)
    {
        return validarPartida.ValidarJugada(fichaMesa, fichaMano);
    }

    public IContarPuntos contarPuntos => ContarPuntos;
}