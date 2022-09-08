namespace Logica.domino.dll;
public class HastaX : IModo
{
    int cantidad;
    int noJug;
    int[] PuntosJugadores;
    bool EnEquipo;
    (int,List<int>) ganadorEs;

    public HastaX(int cantidad, int noJug, bool EnEquipo)
    {
        this.EnEquipo = EnEquipo;
        this.noJug = noJug;
        this.PuntosJugadores = EnEquipo ? new int[2] : new int[noJug];
        this.cantidad = cantidad;
    }

    (int, List<int>) jugPuntos = (0, new List<int>());
    public (int, int) GetGanador(bool EnEquipo)
    {
        return EnEquipo ? (ganadorEs.Item1%2, PuntosJugadores[ganadorEs.Item1%2]) : (ganadorEs.Item1, PuntosJugadores[ganadorEs.Item1]);
    }

    public void TerminoUnaPratida(int ganador, List<int> puntosAcumulados)
    {
        if(EnEquipo) 
        {
            for(int i = 0; i < this.PuntosJugadores.Length; i++)
                PuntosJugadores[i%2] += puntosAcumulados[i];    
        }
        else
        {
            for(int i = 0; i < this.PuntosJugadores.Length; i++)
                PuntosJugadores[i] += puntosAcumulados[i];
        }        
        ganadorEs = (ganador, PuntosJugadores.ToList());
    }

    public int CantidadJugadores => noJug;

    public bool TerminoModo(int ganador, List<int> puntosAcumulados) 
    {
        if(puntosAcumulados is null) return false;
        if(ganador == -1) return false;
        return this.EnEquipo ? PuntosJugadores[ganador%2] >= cantidad : PuntosJugadores[ganador] >= cantidad;//dice si ya se acabo el modo
    }
}
