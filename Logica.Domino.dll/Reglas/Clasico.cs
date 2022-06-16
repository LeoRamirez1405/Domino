using Estructuras_Basicas;
using System;
namespace Reglas;
class ClasicoIndividual<T> : IReglas<T>
{
    protected int cantJugadores;
    protected int cantFichas;//es protected para poder heredar de clasico y hacer un doble 6 (este es el doble 9)
    //private (int, int) dimensionTablero;

    public ClasicoIndividual(int cantJugadores, int cantFichas)//aqui se deberia incializar de con valores
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

    public (int, int) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador)
    {
        int jugadorGanador = -1;
        List<int> valorFichasPorJugador = new List<int>();
        if(definenGanador.ContainsKey(ParametrosDefinenGanador.FichasJugadores) && definenGanador.ContainsKey(ParametrosDefinenGanador.IndexJugadorActual))
        {
            // int cantFichasJugadorActual = ((List<Ficha<T>>)definenGanador[ParametrosDefinenGanador.FichasJugadores])[];
            List<List<Ficha<T>>> fichasJugadores = (List<List<Ficha<T>>>)definenGanador[ParametrosDefinenGanador.FichasJugadores];
            int jugadorActual = (int)definenGanador[ParametrosDefinenGanador.IndexJugadorActual];

            foreach(var x in fichasJugadores)
            {
                int puntos = 0;
                foreach(var y in x)
                    puntos += y.Valor;
                valorFichasPorJugador.Add(puntos);
            }

            //estas condiciones no son necesarias 

            if(fichasJugadores[jugadorActual].Count == 0)//esto seria equivalente a ver si al jugador actual le quedan 0 fichas
            {
                jugadorGanador = jugadorActual;
                // int puntos = 0;
                // for(int i = 0; i < cantJugadores; i++)
                // {
                //     if(i == jugadorActual) continue;
                //     puntos += valorFichasPorJugador[i];
                // }
                // return (jugadorActual,puntos);
                //CalcularPuntosGanoJugador(jugadorActual,valorFichasPorJugador);
            }
            else if(definenGanador.ContainsKey(ParametrosDefinenGanador.TurnosSinJugar))//else
            {
                int min = int.MaxValue;
                for(int i = 0; i < cantJugadores; i++)
                {
                    if(valorFichasPorJugador[i] < min)
                    {
                        min = valorFichasPorJugador[i];
                        jugadorGanador = i;
                    }
                }
            }
        }

        return (jugadorGanador, CalcularPuntosGanoJugador(jugadorGanador, valorFichasPorJugador));//Ojo buscar los tipos de excepciones
    }

    int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {
        int totalPuntos = 0;
        for(int i = 0; i < puntosPorJugador.Count; i++)
        {
            if(i == jugadorGanador) continue;
            totalPuntos += puntosPorJugador[i];
        }
        return totalPuntos;
    }

    public int ProximoJugador(int jugadorActual)
    {
        if(jugadorActual == this.cantJugadores) return 0;
        return ++cantJugadores;
    }

    //Este metodo solo funciona se se juega en elmodo clasico con las fichas clasicas, si trato de jugar con otro tipo de ficha da error ent.. esto se soluciona redefinido el equals de PrteFicha
    //luego, la jugada de parte ficha sera valida si parteFicha fichaMano.Equals(fichaMes) == true
    public bool ValidarJugada(ParteFicha<T> fichaMesa, ParteFicha<T> fichaMano)
    {
        return fichaMano == fichaMesa;//se redefinio el ==
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