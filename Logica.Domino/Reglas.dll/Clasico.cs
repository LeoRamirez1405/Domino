using System;
using Estructuras_Basicas;
namespace Reglas;
class Clasico<T> : IReglas<T>
{
    protected int cantJugadores;
    protected int cantFichas;//es protected para poder heredar de clasico y hacer un doble 6 (este es el doble 9)
    //private (int, int) dimensionTablero;

    public Clasico(int cantJugadores, int cantFichas)//aqui se deberia incializar de con valores
    {
        this.cantJugadores = 4;
        this.cantFichas = 10;
        //this.dimensionTablero = (5,8);//total de fichas que se pueden poner en un juego : cant jugadores * cant fichas = 40 
    }

    public int CantidadJugadores => this.CantidadJugadores;

    public (int, int) DimensionTablero => (5,8);//5 filas 8 columnas

    public List<Ficha<T>[]> Repartir(List<Ficha<T>> todasFichas)//la lista de fichas que se recibe como paarametro son todas las fichas del juego
    {
        //aqui se modifica la coleccion de fichas general para que ell arbitro solo se quede con las fichas sobrantes luego de repartir
        List<Ficha<T>[]> result = new List<Ficha<T>[]>();
        System.Random r = new Random();
        for(int i = 0; i < cantJugadores; i++)
        {
            Ficha<T>[] resultemporal = new Ficha<T>[cantFichas];
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
    public (int,int) Ganador(int fichasRestantesEquipoA, List<Ficha<T>> equipaA, int fichasRestantesEquipoB, List<Ficha<T>> equipoB)
    {
        int acumuladoEquipoA = 0;
        foreach(Ficha<T> x in equipaA)
            acumuladoEquipoA += x.Abajo.Valor + x.Arriba.Valor;
        int acumuladoEquipoB = 0;
        foreach(Ficha<T> x in equipoB) 
            acumuladoEquipoB += x.Abajo.Valor + x.Arriba.Valor;
        
        if(fichasRestantesEquipoA == 0) return (0,acumuladoEquipoB);
        if(fichasRestantesEquipoB == 0) return (1,acumuladoEquipoA);
       
        return acumuladoEquipoA < acumuladoEquipoB ? (0,acumuladoEquipoB) : (1,acumuladoEquipoA);
    }

    public int ProximoJugador(int jugadorActual)
    {
        if(jugadorActual == this.cantJugadores) return 0;
        return ++cantJugadores;
    }

    //Este metodo solo funciona se se juega en elmodo clasico con las fichas clasicas, si trato de jugar con otro tipo de ficha da error ent.. esto se soluciona redefinido el equals de PrteFicha
    //luegi, la jugada de parte ficha sera valida si parteFicha fichaMano.Equals(fichaMes) == true
    public bool ValidarJugada(ParteFicha<T> fichaMesa, ParteFicha<T> fichaMano)
    {
        return fichaMano.Equals(fichaMesa);
    }
    
    // public bool ValidarJugada(ParteFicha<T> fichaMesa, ParteFicha<T> fichaMano)
    // {
    //     return fichaMano == fichaMesa;
    // }

    public int JugadorInicial()
    {
        return 0;
    }
}