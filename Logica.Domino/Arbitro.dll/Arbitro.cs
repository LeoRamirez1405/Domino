namespace Arbitro.dll;
using Estructuras_Basicas;
using Reglas;
using Estrategias;
using System.Diagnostics;
public class Arbitro : GuiaJuego //no es estatico para poder variar las reglas y las estrategias de los jugadores
{
    EstadoJuego estadoJuego;
    IReglas reglas;
    int cantJugadores;
    List<IJugar> estrategiasJugadores;
    Ficha[,] tablero;

    public Arbitro()//IReglas reglas
    {
        this.estadoJuego = EstadoJuego.Null;
        // this.reglas = reglas;
        // this.cantJugadores = reglas.CantidadJugadores;
    }

    public bool CrearJuego(IReglas reglas, List<IJugar> estrategiasJugadores)
    {
        // if(this.estadoJuego != EstadoJuego.Null)
        //     return false;

        this.reglas = reglas;
        this.estrategiasJugadores = estrategiasJugadores;
        tablero = new Ficha[reglas.DimensionTablero.Item1,this.reglas.DimensionTablero.Item2];//el metodo retorna una tupla (contrarto jj)
        return true;
    }

    public bool ComenzarJuego()
    {
        if(this.estadoJuego != EstadoJuego.ListoParaComenzar)
            return false;

        Jugando();
        return true;
    }

    public void Jugando()
    {
        estadoJuego = EstadoJuego.EnCurso;
        // Stopwatch clock = new Stopwatch();
        int jugadorActual = 0;
        //Hay que tener algo que sea una abstraccion de tablero que me diga las fichas disponibles por donde se puede jugar
        // Ficha izquierda = 
        do
        {
            int proximoJugador = this.reglas.ProximoJugador(jugadorActual);
            estrategiasJugadores[proximoJugador].Jugar();
        }
        while(this.estadoJuego == EstadoJuego.EnCurso);
    }

}
