namespace Logica.domino.dll;
public class Match : IModo
{
    int cantidad;
    int noJug;
    // List<IJugar> jugadores;
    int[] PuntosJugadores;
    Arbitro arbitro;
    public Match(int cantidad,int noJug)
    {
        this.arbitro = new Arbitro(noJug);
        // this.jugadores = this.arbitro.GetJugadores();
        this.PuntosJugadores = new int[arbitro.GetJugadores().Count];
        this.cantidad = cantidad;
        
    }
        
    (int,int) jugPuntos = (0,0);
    public (int,int) Gana()
    {
        //jugadorGanador tienen que empezarlo en 0
        while(PuntosJugadores[jugPuntos.Item1]< cantidad)
        {
            jugPuntos = arbitro.Jugando();
            PuntosJugadores[jugPuntos.Item1] += 1;
            for (int i = 0; i < PuntosJugadores.Length; i++)
            {
                int pji =  PuntosJugadores[i];
                System.Console.WriteLine($"Jugador {i} = {pji}");   
            }
            if(PuntosJugadores[jugPuntos.Item1]< cantidad)
                arbitro = new Arbitro(noJug);
        }
   
        return (jugPuntos.Item1,PuntosJugadores[jugPuntos.Item1]);
    }
}