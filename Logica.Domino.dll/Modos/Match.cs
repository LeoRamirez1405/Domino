namespace Logica.domino.dll;
public class Match : IModo
{
    int cantidad;
    int noJug;
    // List<IJugar> jugadores;
    int[] PuntosJugadores;
    int[] PuntosEquipo;
    Arbitro arbitro;
    bool EnEquipo;
    public Match(int cantidad, int noJug, bool EnEquipo)
    {
        this.EnEquipo = EnEquipo;
        this.PuntosEquipo = new int[2];
        this.arbitro = new Arbitro(noJug,EnEquipo);
        // this.jugadores = this.arbitro.GetJugadores();
        this.PuntosJugadores = new int[arbitro.GetJugadores().Count];
        this.cantidad = cantidad;

    }

    (int, List<int>) jugPuntos = (0, new List<int>());

    public (int, int) Gana(bool EnEquipo)
    {
        if (EnEquipo)
        {
            while (PuntosEquipo[jugPuntos.Item1 % 2] < cantidad)
            {
                jugPuntos = arbitro.Jugando();
                if (jugPuntos.Item1 == -1) continue;
                if (jugPuntos.Item1 % 2 == 0)
                {
                    PuntosEquipo[0] += 1;
                }
                else
                {
                    PuntosEquipo[1] += 1;
                }

                if (PuntosEquipo[jugPuntos.Item1 % 2] < cantidad)
                    arbitro = new Arbitro(noJug,EnEquipo);
            }
            return (jugPuntos.Item1 % 2, PuntosEquipo[jugPuntos.Item1 % 2]);
        }
        else
        {
            while (PuntosJugadores[jugPuntos.Item1] < cantidad)
            {
                if (jugPuntos.Item1 == -1) continue;
                jugPuntos = arbitro.Jugando();
                PuntosJugadores[jugPuntos.Item1] += 1;
                for (int i = 0; i < PuntosJugadores.Length; i++)
                {
                    int pji = PuntosJugadores[i];
                    System.Console.WriteLine($"Jugador {i} = {pji}");
                }
                if (PuntosJugadores[jugPuntos.Item1] < cantidad)
                    arbitro = new Arbitro(noJug,EnEquipo);
            }
            return (jugPuntos.Item1, PuntosJugadores[jugPuntos.Item1]);
        }

    }
}