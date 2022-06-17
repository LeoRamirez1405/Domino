namespace Logica.domino.dll;
public class Match : IModo
{
    int cantidad;
    List<IJugar> jugadores;
    int[] PuntosJugadores;
    Arbitro arbitro;

    public Match(int cantidad,List<IJugar> jugadores,IReglas reglas,IDomino domino)
    {
        this.arbitro = new Arbitro(reglas,domino);
        this.jugadores = this.arbitro.GetJugadores();
        this.PuntosJugadores = new int[jugadores.Count];
        this.cantidad = cantidad;
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