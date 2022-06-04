namespace Reglas;
using Estructuras_Basicas;

public interface IReglas<T>
{
    int JugadorInicial();
    int CantidadJugadores{ get; }
    (int,int) DimensionTablero { get; }
    List<Ficha<T>[]> Repartir(List<Ficha<T>> todasFichas);
    bool ValidarJugada(ParteFicha<T> fichaMesa, ParteFicha<T> fichaMano);
    bool FinalizoPartida(int jugoJugadorActual, int turnosSinJugar);
    int ProximoJugador(int jugadorActual);
    (int,int) Ganador(int fichasRestantesEquipoA, List<Ficha<T>> equipaA, int fichasRestantesEquipoB, List<Ficha<T>> equipoB);
}
