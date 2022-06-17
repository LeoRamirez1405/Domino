namespace Logica.domino.dll;
public class HastaX : IModo
{
    int cantidad;
    List<IJugar> jugadores;
    int[] PuntosJugadores;
    Arbitro arbitro;
    public HastaX(int cantidad,IReglas reglas,IDomino domino)
    {
        this.arbitro = new Arbitro(reglas,domino);
        this.jugadores = this.arbitro.GetJugadores();
        this.PuntosJugadores = new int[jugadores.Count];
        this.cantidad = cantidad;
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
