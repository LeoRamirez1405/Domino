namespace Logica.domino.dll;

public interface IAccionDespuesDeLaJugada
{
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<Jugador> jugadores,ref bool invertido);

}
public class AccionDespuesDeLaJugada_Quincena:IAccionDespuesDeLaJugada
{
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<Jugador> jugadores,ref bool invertido)
    {
        if (huboJugada && (izquierda.Valor + derecha.Valor) % 5 == 0)
        {
            puntosPorJugador[jugadorActual] += izquierda.Valor + derecha.Valor;
            int a = puntosPorJugador[jugadorActual];
            System.Console.WriteLine($"Jugador {jugadorActual} obtuvo {izquierda.Valor + derecha.Valor} puntos. Total {a}");
        }
    }
}

public class AccionDespuesDeLaJugada_Clasico: IAccionDespuesDeLaJugada
{
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<Jugador> jugadores,ref bool invertido)
    {
        return;
    }
}
public class AccionDespuesDeLaJugada_InvertirJugadores: IAccionDespuesDeLaJugada
{
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador,ref List<Jugador> jugadores,ref bool invertido)
    {
        if(!huboJugada)
        {
            invertido = !invertido;
        }
    }
}


