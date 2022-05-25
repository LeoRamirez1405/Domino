namespace Reglas;
using Estructuras_Basicas;
using System;
class Clasico : IReglas
{
    int cantJugadores;
    int cantFichas;
    public Clasico(int cantJugadores, int cantFichas)
    {
        this.cantJugadores = cantJugadores;//4
        this.cantFichas = cantFichas;//10
    }

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
            acumuladoEquipoA += x.abajo + x.arriba;
        int acumuladoEquipoB = 0;
        foreach(Ficha x in equipoB) 
            acumuladoEquipoB += x.abajo + x.arriba;
        
        if(fichasRestantesEquipoA == 0) return (0,acumuladoEquipoB);
        if(fichasRestantesEquipoB == 0) return (1,acumuladoEquipoA);
       
        return acumuladoEquipoA < acumuladoEquipoB ? (0,acumuladoEquipoB) : (1,acumuladoEquipoA);
    }

    public int ProximoJugador(int jugadorActual)
    {
        if(jugadorActual == this.cantJugadores) return 0;
        return ++cantJugadores;
    }

    public bool ValidarJugada(int fichaMesa, int fichaMano)
    {
        if(fichaMesa != fichaMano) return false;
        return true;
    }
}