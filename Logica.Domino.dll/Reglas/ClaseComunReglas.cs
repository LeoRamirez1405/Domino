namespace Logica.domino.dll;

public class ClaseComunReglas
{
    public int cantMinJugadores   {get;}
    public int cantMaxJugadores   {get;}
    public int cantJugadoresEnJuego{get;}
    public int cantFichasPorJugador{get;}
    public List<int> puntosPorJugador;
    //public IContarPuntos contarPuntos;

    public ClaseComunReglas(int cantFichas, int cantJugadores, int valorMaximoFichaDomino)
    {
        //this.contarPuntos = contarPuntos;
        this.cantMinJugadores = 2;
        this.cantMaxJugadores = 4;

        if(cantJugadores < cantMinJugadores || cantJugadores > cantMaxJugadores)
            this.cantJugadoresEnJuego = 4;
        else
            this.cantJugadoresEnJuego = cantJugadores;

        if(cantFichas < 1 || cantFichas > valorMaximoFichaDomino + 1)//En el doble 9 se juegan con 10 fichas en el doble 6 con 7 y asi en dependecia del valor de la fichas
            this.cantFichasPorJugador = valorMaximoFichaDomino + 1;
        else
            this.cantFichasPorJugador = cantFichas;

        this.puntosPorJugador = new List<int>();
        for (int i = 0; i < cantJugadores; i++)
        {
            puntosPorJugador.Add(0);
        }
    }

    public int JugadorInicial()
    {
        Random a = new Random();
        return a.Next(0, cantJugadoresEnJuego);
    }
}