namespace Logica.domino.dll;
public class HastaX : IModo
{
    int cantidad;
    List<Jugador> jugadores;
    int[] PuntosJugadores;
    Arbitro arbitro;
    public HastaX(int cantidad,List<Jugador> jugadores,IReglas reglas,IDomino domino)
    {
        this.cantidad = cantidad;
        this.jugadores = jugadores;
        this.PuntosJugadores = new int[jugadores.Count];
        this.arbitro = new Arbitro(reglas,domino);
    }
    public (int,int) Gana()
    {
        (int,int) jugPuntos = (-1,-1);
        //equipoGanador tienen que empezarlo en 0
        while(PuntosJugadores[jugPuntos.Item1]< cantidad)
        {
            jugPuntos = arbitro.Jugando();
            PuntosJugadores[jugPuntos.Item1] += jugPuntos.Item2;
        }
        return (jugPuntos.Item1,PuntosJugadores[jugPuntos.Item1]);
    }
}
