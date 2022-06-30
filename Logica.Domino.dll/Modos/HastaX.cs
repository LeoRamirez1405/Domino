namespace Logica.domino.dll;
public class HastaX : IModo
{
    int cantidad;
    int noJug;
    // List<IJugar> jugadores;
    int[] PuntosJugadores;
    int[] PuntosEquipo;
    Arbitro arbitro;
    bool EnEquipo;
    public HastaX(int cantidad, int noJug, bool EnEquipo)
    {
        this.EnEquipo = EnEquipo;
        this.arbitro = new Arbitro(noJug);
        this.noJug = noJug;
        this.PuntosEquipo = new int[2];
        // this.jugadores = this.arbitro.GetJugadores();
        this.PuntosJugadores = new int[arbitro.GetJugadores().Count];
        this.cantidad = cantidad;
    }


    (int, List<int>) jugPuntos = (0, new List<int>());
    public (int, int) Gana(bool EnEquipo)
    {
        if (EnEquipo)
        {
            while (PuntosEquipo[jugPuntos.Item1%2] < cantidad)
            {
                if (jugPuntos.Item1 == -1) continue;
                jugPuntos = arbitro.Jugando();
                for (int i = 0; i < PuntosJugadores.Length; i++)
                {
                    if (i % 2 ==0)
                    {
                        PuntosEquipo[i] += jugPuntos.Item2[i];
                    }
                    else
                    {
                        PuntosEquipo[i] += jugPuntos.Item2[i];
                    }
                }
                if (PuntosEquipo[jugPuntos.Item1%2] < cantidad)
                    arbitro = new Arbitro(noJug);
            }
            return (jugPuntos.Item1%2, PuntosEquipo[jugPuntos.Item1%2]);
        }
        else
        {
            while (PuntosJugadores[jugPuntos.Item1] < cantidad)
            {
                if (jugPuntos.Item1 == -1) continue;
                jugPuntos = arbitro.Jugando();
                //jugadorGanador tienen que empezarlo en 0
                PuntosJugadores[jugPuntos.Item1] += jugPuntos.Item2[jugPuntos.Item1];
                for (int i = 0; i < PuntosJugadores.Length; i++)
                {
                    int pji = PuntosJugadores[i];
                    System.Console.WriteLine($"Jugador {i} = {pji}");
                }
                if (PuntosJugadores[jugPuntos.Item1] < cantidad)
                    arbitro = new Arbitro(noJug);
            }
            return (jugPuntos.Item1, PuntosJugadores[jugPuntos.Item1]);
        }

    }
}
