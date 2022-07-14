namespace Logica.domino.dll;
public class HastaX : IModo
{
    int cantidad;
    int noJug;
    // List<IJugar> jugadores;
    int[] PuntosJugadores;
    int[] PuntosEquipo;
    //Arbitro arbitro;
    bool EnEquipo;
    (int,List<int>) ganadorEs;
    public HastaX(int cantidad, int noJug, bool EnEquipo)
    {
        this.EnEquipo = EnEquipo;
        //this.arbitro = new Arbitro(noJug,EnEquipo);
        this.noJug = noJug;
        this.PuntosEquipo = new int[2];
        // this.jugadores = this.arbitro.GetJugadores();
        this.PuntosJugadores = EnEquipo ? new int[noJug/2] : new int[noJug];
        this.cantidad = cantidad;
    }


    (int, List<int>) jugPuntos = (0, new List<int>());
    // public (int, int) Gana(bool EnEquipo)
    // {
    //     if (EnEquipo)
    //     {
    //         while (PuntosEquipo[jugPuntos.Item1%2] < cantidad)
    //         {
    //             if (jugPuntos.Item1 == -1) continue;
    //             jugPuntos = arbitro.Jugando();
    //             for (int i = 0; i < PuntosJugadores.Length; i++)
    //             {
    //                 if (i % 2 ==0)
    //                 {
    //                     PuntosEquipo[i] += jugPuntos.Item2[i];
    //                 }
    //                 else
    //                 {
    //                     PuntosEquipo[i] += jugPuntos.Item2[i];
    //                 }
    //             }
    //             if (PuntosEquipo[jugPuntos.Item1%2] < cantidad)
    //                 arbitro = new Arbitro(noJug,EnEquipo);
    //         }
    //         return (jugPuntos.Item1%2, PuntosEquipo[jugPuntos.Item1%2]);
    //     }
    //     else
    //     {
    //         while (PuntosJugadores[jugPuntos.Item1] < cantidad)
    //         {
    //             if (jugPuntos.Item1 == -1) continue;
    //             jugPuntos = arbitro.Jugando();
    //             //jugadorGanador tienen que empezarlo en 0
    //             PuntosJugadores[jugPuntos.Item1] += jugPuntos.Item2[jugPuntos.Item1];
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
        return EnEquipo ? (ganadorEs.Item1%2, PuntosEquipo[ganadorEs.Item1%2]) : (ganadorEs.Item1, PuntosJugadores[ganadorEs.Item1]);
    }

    public void TerminoUnaPratida(int ganador, List<int> puntosAcumulados)
    {
        if(EnEquipo) 
        {
            for(int i = 0; i < this.PuntosJugadores.Length; i++)
                PuntosJugadores[i%2] += puntosAcumulados[i];    
        }
        else
            PuntosJugadores[ganador] += puntosAcumulados[ganador];
        
        ganadorEs = (ganador, puntosAcumulados);
    }

    public int CantidadJugadores => noJug;

    public bool TerminoModo(int ganador, List<int> puntosAcumulados) 
    {
        if(puntosAcumulados is null) return false;
        return this.EnEquipo ? PuntosEquipo[ganador%2] < cantidad : PuntosJugadores[ganador] < cantidad;//dice si ya se acabo el modo
    }
}
