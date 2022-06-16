namespace Logica.domino.dll;
public class Match<T> : IModo<T>
{
    int cantidad;
    List<Jugador<T>> jugadores;
    int[] PuntosJugadores;
    Arbitro<T> arbitro;

    public Match(int cantidad,List<Jugador<T>> jugadores,IReglas<T> reglas,IDomino<T> domino)
    {
        this.cantidad = cantidad;
        this.jugadores = jugadores;
        this.PuntosJugadores = new int[jugadores.Count];
        this.arbitro = new Arbitro<T>(reglas,domino);
    }
        
    public (int,int) Gana()
    {
        (int,int) jugPuntos = (-1,-1);
        //jugadorGanador tienen que empezarlo en 0
        while(PuntosJugadores[jugPuntos.Item1]< cantidad)
        {
            jugPuntos = arbitro.Jugando();
            PuntosJugadores[jugPuntos.Item1] += 1;
        }
        return (jugPuntos.Item1,PuntosJugadores[jugPuntos.Item1]);
    }
}