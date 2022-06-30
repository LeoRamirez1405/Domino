namespace Logica.domino.dll;

public interface IFinalizarJugada
{
    public bool FinalizoPartida(int cantFichasJugadorActual, int turnosSinJugar,List<int> puntosPorJugador);
    
}

public class FinalizarJugada_Llegue100 : IFinalizarJugada
{
    public bool FinalizoPartida(int cantFichasJugadorActual, int turnosSinJugar, List<int> puntosPorJugador)
    {
        if (cantFichasJugadorActual == 0 || turnosSinJugar == puntosPorJugador.Count || puntosPorJugador.Contains(100)) return true;

        return false;
    }
}

public class FinalizarJugada_Clasico: IFinalizarJugada
{
    public bool FinalizoPartida(int cantFichasJugadorActual, int turnosSinJugar, List<int> puntosPorJugador)
    {
        if (cantFichasJugadorActual == 0 || turnosSinJugar == puntosPorJugador.Count) return true;
        return false;
    }
}



