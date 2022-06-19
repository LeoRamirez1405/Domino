namespace Logica.domino.dll;

public class Quincena : ClasicoIndividual
{
    public List<int> puntosPorJugador;
    public Quincena(int cantJugadores, int cantFichasPorJugador, int valorMaxFichas) : base(cantJugadores,cantFichasPorJugador, valorMaxFichas){
        this.puntosPorJugador = new List<int>();
        for(int i = 0; i < base.cantJugadoresEnJuego; i++)
        {
            puntosPorJugador.Add(0);
        }
    }

    public new (int,int) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador)
    {
        (int,int) resultAux = base.Ganador(definenGanador);
        if(definenGanador.ContainsKey(ParametrosDefinenGanador.SeTrancoElJuego) && (bool)definenGanador[ParametrosDefinenGanador.SeTrancoElJuego])
            return (resultAux.Item1, resultAux.Item1 + 5);
        
        return (resultAux.Item1, resultAux.Item2 + puntosPorJugador[resultAux.Item1] + 10);
    }

    public override void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha)
    {
        if(huboJugada && (izquierda.Valor + derecha.Valor) % 5 == 0)
        {
            puntosPorJugador[jugadorActual] += izquierda.Valor + derecha.Valor;
            int a = puntosPorJugador[jugadorActual];
            System.Console.WriteLine($"Jugador {jugadorActual} obtuvo {izquierda.Valor + derecha.Valor} puntos. Total {a}");
        }
    }

    public override bool FinalizoPartida(int cantFichasJugadorActual, int turnosSinJugar)
    {
        return base.FinalizoPartida(cantFichasJugadorActual, turnosSinJugar) || puntosPorJugador.Contains(100);
    }
}