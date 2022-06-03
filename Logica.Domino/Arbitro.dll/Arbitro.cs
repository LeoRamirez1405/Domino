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
    ParteFicha derecha;
    ParteFicha izquierda;

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
        (int,int)[] pos = new (int,int)[2];//por el monento solo se puede jugar x dos lados pues las fichas son las tradicionales

        // Stopwatch clock = new Stopwatch();
        int jugadorActual = 0;
        //Hay que tener algo que sea una abstraccion de tablero que me diga las fichas disponibles por donde se puede jugar
        if(this.derecha == null && this.izquierda == null)
        {
            estrategiasJugadores[reglas.JugadorInicial()].Jugar(this.derecha,this.izquierda);
        }
        do
        {
            int proximoJugador = this.reglas.ProximoJugador(jugadorActual);
            //aqui solol deberia pasar las dos fichas por donde se puede jugar y tendria que devolver la ficha que va a jugar y por donde la va ajugar
            (Ficha,int) jugada = estrategiasJugadores[proximoJugador].Jugar(tablero[pos[0].Item1,pos[0].Item2], tablero[pos[1].Item1,pos[1].Item2]);//aqui no es necesario pasarle las reglas al los jugadores, pues las estrategias dben poder funcionar (No necesariamente se eficientes) con cualquier juego o regla
            
        }
        while(this.estadoJuego == EstadoJuego.EnCurso);
    }

}
