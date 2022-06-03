namespace Reglas;
using Estructuras_Basicas;
using System;
class Clasico : IReglas
{
    protected int cantJugadores;
    protected int cantFichas;//es protected para poder heredar de clasico y hacer un doble 6 (este es el doble 9)
    //private (int, int) dimensionTablero;

    public Clasico(int cantJugadores, int cantFichas)
    {
        this.cantJugadores = 4;
        this.cantFichas = 10;
        //this.dimensionTablero = (5,8);//total de fichas que se pueden poner en un juego : cant jugadores * cant fichas = 40 
    }

    public int CantidadJugadores => this.CantidadJugadores;

    public (int, int) DimensionTablero => (5,8);//5 filas 8 columnas

    public List<Ficha[]> Repartir(List<Ficha> todasFichas)//la lista de fichas que se recibe como paarametro son todas las fichas del juego
    {
        List<Ficha[]> result = new List<Ficha[]>();
        System.Random r = new Random();
        for(int i = 0; i < cantJugadores; i++)
        {
            Ficha[] resultemporal = new Ficha[cantFichas];
            for(int j = 0; j < cantFichas; j++)
            {
                int pos = r.Next(todasFichas.Count);
                resultemporal[j] = todasFichas[pos];
                todasFichas.RemoveAt(pos);
            }
            result.Add(resultemporal);
        }
        return result;
    }

    public bool FinalizoPartida(int cantFichasJugadorActual, int turnosSinJugar)
    {
        if(cantFichasJugadorActual == 0) return true;
        if(turnosSinJugar == cantJugadores) return true;
        return false;
    }

    //Retorna dos enteros. El primer entero simboliza el equipo ganador y el segun la cant de puntos que gana
    public (int,int) Ganador(int fichasRestantesEquipoA, List<Ficha> equipaA, int fichasRestantesEquipoB, List<Ficha> equipoB)
    {
        int acumuladoEquipoA = 0;
        foreach(Ficha x in equipaA)
            acumuladoEquipoA += x.ValorAbajo + x.ValorArriba;
        int acumuladoEquipoB = 0;
        foreach(Ficha x in equipoB) 
            acumuladoEquipoB += x.ValorAbajo + x.ValorArriba;
        
        if(fichasRestantesEquipoA == 0) return (0,acumuladoEquipoB);
        if(fichasRestantesEquipoB == 0) return (1,acumuladoEquipoA);
       
        return acumuladoEquipoA < acumuladoEquipoB ? (0,acumuladoEquipoB) : (1,acumuladoEquipoA);
    }

    public int ProximoJugador(int jugadorActual)
    {
        if(jugadorActual == this.cantJugadores) return 0;
        return ++cantJugadores;
    }

    public bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano)
    {
        if(fichaMesa != fichaMano) return false;
        return true;
    }

    public int JugadorInicial()
    {
        return 0;
    }
}