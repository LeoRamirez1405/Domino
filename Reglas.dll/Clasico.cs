using Estructuras_Basicas;
using Reglas;

class Clasico : IReglas
{
    int cantJugadores;

    public bool FinalizoPartida(int cantFichasJugadorActual, int turnosSinJugar)
    {
        if(cantFichasJugadorActual == 0) return true;
        if(turnosSinJugar == cantJugadores) return true;
        return false;
    }

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
       
        return acumuladoEquipoA < acumuladoEquipoB ? (0,acumuladoEquipoB) : (0,acumuladoEquipoA);
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