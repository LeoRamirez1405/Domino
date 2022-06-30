namespace Logica.domino.dll;

public interface IProximoJugador
{
    int ProximoJugador(int jugadorActual,int totalJugadores);

}

public class ProximoJugador_Clasico:IProximoJugador
{
    public int ProximoJugador(int jugadorActual,int totalJugadores)
    {
        if(jugadorActual == totalJugadores - 1) return 0;//cantJugadores
        return jugadorActual + 1;
    }
}

public class ProximoJugador_Aleatorio:IProximoJugador
{
    public int ProximoJugador(int jugadorActual,int totalJugadores)
    {
        Random r = new Random();
        int num = r.Next(0,totalJugadores);

        return num;
    }
}


