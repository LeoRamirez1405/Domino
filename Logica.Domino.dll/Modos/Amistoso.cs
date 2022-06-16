namespace Logica.domino.dll;
public class Amistoso : IModo
{
    List<Jugador> jugadores;
    Arbitro arbitro;
    int[] PuntosJugadores;

    public Amistoso(List<Jugador> jugadores,IReglas reglas,IDomino domino)
    {
        this.jugadores = jugadores;
        this.arbitro = new Arbitro(reglas,domino);
        this.PuntosJugadores = new int[jugadores.Count];
    }
    public (int,int) Gana()
    {
        return arbitro.Jugando();
    }
}
