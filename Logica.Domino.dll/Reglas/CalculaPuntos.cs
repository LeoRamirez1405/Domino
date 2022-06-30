namespace Logica.domino.dll;

public interface ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador);
}
public class CalcularPuntosGanoJugador_Clasico:ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {
        int totalPuntos = 0;
        for (int i = 0; i < puntosPorJugador.Count; i++)
        {
            if (i == jugadorGanador) continue;
            totalPuntos += puntosPorJugador[i];
        }
        return totalPuntos;
    }
}

public class CalcularPuntosGanoJugador_SoloYo: ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {        
        return puntosPorJugador[jugadorGanador];
    }
}

public class CalcularPuntosGanoJugador_Comunista: ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {
        int totalPuntos = 0;
        for (int i = 0; i < puntosPorJugador.Count; i++)
        {
            totalPuntos += puntosPorJugador[i];
        }
        return totalPuntos/puntosPorJugador.Count;
    }
}

public class CalcularPuntosGanoJugador_Capitalista : ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {
        int totalPuntos = 0;
        for (int i = 0; i < puntosPorJugador.Count; i++)
        {
            totalPuntos += puntosPorJugador[i];
        }
        return totalPuntos;
    }
}
