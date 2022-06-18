namespace Logica.domino.dll;
public class Match : IModo
{
    int cantidad;
    List<IJugar> jugadores;
    int[] PuntosJugadores;
    Arbitro arbitro;
    IReglas reglas;
    IDomino domino;
    public Match(int cantidad,IReglas reglas,IDomino domino)
    {
        this.arbitro = new Arbitro(reglas,domino);
        this.jugadores = this.arbitro.GetJugadores();
        this.PuntosJugadores = new int[jugadores.Count];
        this.cantidad = cantidad;
        this.domino = domino;
        this.reglas = reglas;
    }
        
    (int,int) jugPuntos = (0,0);
    public (int,int) Gana()
    {
        //jugadorGanador tienen que empezarlo en 0
        while(PuntosJugadores[jugPuntos.Item1]< cantidad)
        {
            jugPuntos = arbitro.Jugando();
            PuntosJugadores[jugPuntos.Item1] += 1;
            for (int i = 0; i < jugadores.Count; i++)
            {
                int pji =  PuntosJugadores[i];
                System.Console.WriteLine($"Jugador {i} = {pji}");   
            }
            if(PuntosJugadores[jugPuntos.Item1]< cantidad)
                arbitro = new Arbitro(reglas,domino);
        }
   
        return (jugPuntos.Item1,PuntosJugadores[jugPuntos.Item1]);
    }
}