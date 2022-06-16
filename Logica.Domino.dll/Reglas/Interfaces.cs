namespace Logica.domino.dll;
public interface IReglas
{
    int JugadorInicial();
    int CantidadJugadores{ get; }
    (int,int) DimensionTablero { get; }
    List<Ficha[]> Repartir(List<Ficha> todasFichas);
    bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano);
    bool FinalizoPartida(int jugoJugadorActual, int turnosSinJugar);
    int ProximoJugador(int jugadorActual);
    (int,int) Ganador(Dictionary<ParametrosDefinenGanador,object> definenGanador);//int fichasRestantesEquipoA, List<Ficha> equipaA, int fichasRestantesEquipoB, List<Ficha> equipoB
}
