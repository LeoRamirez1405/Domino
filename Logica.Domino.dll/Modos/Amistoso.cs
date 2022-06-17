namespace Logica.domino.dll;
public class Amistoso : IModo
{
    List<IJugar> jugadores;
    Arbitro arbitro;
    int[] PuntosJugadores;

    public Amistoso(IReglas reglas,IDomino domino)
    {
        this.arbitro = new Arbitro(reglas,domino);
        this.jugadores = this.arbitro.GetJugadores();
        this.PuntosJugadores = new int[jugadores.Count];
    }
    public (int,int) Gana()
    {
        return arbitro.Jugando();
    }
}
