namespace Reglas;
using Estructuras_Basicas;

public interface IReglas 
{
    int JugadorInicial();
    int CantidadJugadores{ get; }
    (int,int) DimensionTablero { get; }
    List<Ficha[]> Repartir(List<Ficha> todasFichas);
    bool ValidarJugada(int cantFichasJugadorActual, int turnosSinJugar);
    bool FinalizoPartida(int jugoJugadorActual, int turnosSinJugar);
    int ProximoJugador(int jugadorActual);
    (int,int) Ganador(int fichasRestantesEquipoA, List<Ficha> equipaA, int fichasRestantesEquipoB, List<Ficha> equipoB);
}
