namespace Logica.domino.dll;
public class Match : IModo
{
    int cantidad;
    int noJug;
    int[] PuntosJugadores;
    //int[] PuntosEquipo;
    //Arbitro arbitro;
    bool EnEquipo;
    // int cantPartidasJugadas;
    (int,List<int>) ganadorEs;//guarda el ganador por partida
    public Match(int cantidad, int noJug, bool EnEquipo)
    {
        this.EnEquipo = EnEquipo;
        //this.PuntosEquipo = new int[2];
        this.noJug = noJug;
        //this.arbitro = new Arbitro(noJug,EnEquipo);
        this.PuntosJugadores = EnEquipo ? new int[2] : new int[noJug];
        this.cantidad = cantidad;
        // this.cantPartidasJugadas = 0;
    }

    (int, List<int>) jugPuntos = (0, new List<int>());

    public (int, int) GetGanador(bool EnEquipo)
    {
        return EnEquipo ? (jugPuntos.Item1 % 2, PuntosJugadores[jugPuntos.Item1 % 2]) : (jugPuntos.Item1, PuntosJugadores[jugPuntos.Item1]);
    }

    public void TerminoUnaPratida(int ganador, List<int> puntosAcumulados)
    {
        // this.cantPartidasJugadas++;
        if(EnEquipo) 
        {
            for(int i = 0; i < this.PuntosJugadores.Length; i++)
                PuntosJugadores[i%2] ++;    
            // PuntosJugadores[ganador%2] ++;
        }
        else
        {
            for(int i = 0; i < this.PuntosJugadores.Length; i++)
                PuntosJugadores[i] ++;
            // PuntosJugadores[ganador] ++;

        }        
        ganadorEs = (ganador, PuntosJugadores.ToList());
    }

    public int CantidadJugadores => noJug;
    public bool TerminoModo(int ganador, List<int> puntosAcumulados)
    {
        if(puntosAcumulados is null) return false;
        if(ganador == -1) return false;
        return this.EnEquipo ? PuntosJugadores[ganador%2] == cantidad : PuntosJugadores[ganador] == cantidad;//dice si ya se acabo el modo
    }
}