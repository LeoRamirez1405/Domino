namespace Logica.domino.dll;

public interface ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador,bool equipo);
}
public class CalcularPuntosGanoJugador_Clasico:ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador,bool equipo)
    {
            int totalPuntos = 0;
            for (int i = 0; i < puntosPorJugador.Count; i++)
            {
                if (i == jugadorGanador) continue;
                if (equipo && i % 2 == jugadorGanador % 2) continue;
                    totalPuntos += puntosPorJugador[i];
            }
            return totalPuntos;
    }
}
public class CalcularPuntosGanoJugador_Quincena:ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador,bool equipo)
    {
            int totalPuntos = puntosPorJugador[jugadorGanador];
            if(totalPuntos%5 == 0)
                totalPuntos*=2;
            else
                totalPuntos = totalPuntos - totalPuntos%5;
            
            System.Console.WriteLine("TotalPuntos"+totalPuntos);
            return totalPuntos;
    }
}
public class CalcularPuntosGanoJugador_SoloYo: ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador, bool equipo)
    {
        int totalPuntos = 0;
        for (int i = 0; i < puntosPorJugador.Count; i++)
        {
            if (equipo)
            {
                if (i % 2 != jugadorGanador % 2) continue;
            }
            else if (i != jugadorGanador) continue;
            totalPuntos += puntosPorJugador[i];
        }
        return totalPuntos;
    }
}

public class CalcularPuntosGanoJugador_Comunista: ICalculaPuntos
{
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador, bool equipo)
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
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador, bool equipo)
    {
        int totalPuntos = 0;
        for (int i = 0; i < puntosPorJugador.Count; i++)
        {
            totalPuntos += puntosPorJugador[i];
        }
        return totalPuntos;
    }
}
