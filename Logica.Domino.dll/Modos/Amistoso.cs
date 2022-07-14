namespace Logica.domino.dll;
public class Amistoso : IModo
{
    // List<IJugar> jugadores;
    //Arbitro arbitro;
    int[] PuntosJugadores;
    bool EnEquipo;
    (int,List<int>) ganadorEs;
    int cantPartidas;
    int cantJugadores;
    public Amistoso(int noJug,bool EnEquipo)
    {
        this.EnEquipo = EnEquipo;
        this.EnEquipo = EnEquipo;
        this.cantJugadores = noJug;
        //this.arbitro = new Arbitro(noJug,EnEquipo);
        this.PuntosJugadores = EnEquipo ? new int[noJug/2] : new int[noJug];
        this.ganadorEs = (-1, null);
        this.cantPartidas = 0;
    }

    public (int, int) GetGanador(bool EnEquipo)
    {
        if (ganadorEs.Item1 == -1) return (-1, -1);
        
        if(EnEquipo)
        {
            if (ganadorEs.Item1 % 2 == 0)
                return (0, ganadorEs.Item2[ganadorEs.Item1]);
            return (1, ganadorEs.Item2[ganadorEs.Item1]);
        }

        return (ganadorEs.Item1, ganadorEs.Item2[ganadorEs.Item1]);
    }

    public void TerminoUnaPratida(int ganador, List<int> puntosAcumulados)
    {
        this.cantPartidas++;
        ganadorEs = (ganador, puntosAcumulados);
    }

    public bool TerminoModo(int ganador, List<int> puntosAcumulados)
    {
        if(puntosAcumulados is null) return false;
        return this.cantPartidas == 1;
    }

    public int CantidadJugadores => cantJugadores;
    //public bool TerminoModo => this.cantPartidas == 1;
}
