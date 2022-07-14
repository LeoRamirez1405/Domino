namespace Logica.domino.dll;
public class Match : IModo
{
    int cantidad;
    int noJug;
    int[] PuntosJugadores;
    int[] PuntosEquipo;
    //Arbitro arbitro;
    bool EnEquipo;
    int cantPartidasJugadas;
    (int,List<int>) ganadorEs;//guarda el ganador por partida
    public Match(int cantidad, int noJug, bool EnEquipo)
    {
        this.EnEquipo = EnEquipo;
        this.PuntosEquipo = new int[2];
        //this.arbitro = new Arbitro(noJug,EnEquipo);
        this.PuntosJugadores = EnEquipo ? new int[noJug/2] : new int[noJug];
        this.cantidad = cantidad;
        this.cantPartidasJugadas = 0;
    }

    (int, List<int>) jugPuntos = (0, new List<int>());

    // public (int, int) Gana(bool EnEquipo)
    // {
    //     if (EnEquipo)
    //     {
    //         while (PuntosEquipo[jugPuntos.Item1 % 2] < cantidad)
    //         {
    //             jugPuntos = arbitro.Jugando();
    //             if (jugPuntos.Item1 == -1) continue;
    //             if (jugPuntos.Item1 % 2 == 0)
    //             {
    //                 PuntosEquipo[0] += 1;
    //             }
    //             else
    //             {
    //                 PuntosEquipo[1] += 1;
    //             }

    //             if (PuntosEquipo[jugPuntos.Item1 % 2] < cantidad)
    //                 arbitro = new Arbitro(noJug,EnEquipo);
    //         }
    //         return (jugPuntos.Item1 % 2, PuntosEquipo[jugPuntos.Item1 % 2]);
    //     }
    //     else
    //     {
    //         while (PuntosJugadores[jugPuntos.Item1] < cantidad)
    //         {
    //             if (jugPuntos.Item1 == -1) continue;
    //             jugPuntos = arbitro.Jugando();
    //             PuntosJugadores[jugPuntos.Item1] += 1;
    //             for (int i = 0; i < PuntosJugadores.Length; i++)
    //             {
    //                 int pji = PuntosJugadores[i];
    //                 System.Console.WriteLine($"Jugador {i} = {pji}");
    //             }
    //             if (PuntosJugadores[jugPuntos.Item1] < cantidad)
    //                 arbitro = new Arbitro(noJug,EnEquipo);
    //         }
    //         return (jugPuntos.Item1, PuntosJugadores[jugPuntos.Item1]);
    //     }

    // }

    public (int, int) GetGanador(bool EnEquipo)
    {
        return EnEquipo ? (jugPuntos.Item1 % 2, PuntosEquipo[jugPuntos.Item1 % 2]) : (jugPuntos.Item1, PuntosJugadores[jugPuntos.Item1]);
    }

    public void TerminoUnaPratida(int ganador, List<int> puntosAcumulados)
    {
        this.cantPartidasJugadas++;
        ganadorEs = (ganador, puntosAcumulados);
    }

    public int CantidadJugadores => noJug;
    public bool TerminoModo(int ganador, List<int> puntosAcumulados)
    {
        if(puntosAcumulados is null) return false;
        return cantPartidasJugadas == this.cantidad;
    }
}