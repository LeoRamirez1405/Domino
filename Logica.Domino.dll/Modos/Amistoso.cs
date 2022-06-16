namespace Logica.domino.dll;
public class Amistoso<T> : IModo<T>
{
    List<Jugador<T>> jugadores;
    Arbitro<T> arbitro;
    int[] PuntosJugadores;

    public Amistoso(List<Jugador<T>> jugadores,IReglas<T> reglas,IDomino<T> domino)
    {
        this.jugadores = jugadores;
        this.arbitro = new Arbitro<T>(reglas,domino);
        this.PuntosJugadores = new int[jugadores.Count];
    }
    public (int,int) Gana()
    {
        return arbitro.Jugando();
    }
}
