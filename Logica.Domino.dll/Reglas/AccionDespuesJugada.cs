namespace Logica.domino.dll;

public interface IAccionDespuesDeLaJugada
{
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<IJugar> jugadores);

}

public class AccionDespuesDeLaJugada_Quincena:IAccionDespuesDeLaJugada
{
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<IJugar> jugadores)
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
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<IJugar> jugadores)
    {
        return;
    }
}
public class AccionDespuesDeLaJugada_InvertirJugadores: IAccionDespuesDeLaJugada
{
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador,ref List<IJugar> jugadores)
    {
        if(!huboJugada)
        {
            puntosPorJugador.Reverse();
            jugadores.Reverse();
        }
    }
}


