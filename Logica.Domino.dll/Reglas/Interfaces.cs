namespace Logica.domino.dll;
public interface IReglas:IAccionDespuesDeLaJugada,IFinalizarJugada,IGanador,IProximoJugador,IRepartir,IValidarJugada
{
    bool equipo { get; }
    bool invertido{get;}
    int CantFichasTotalJuego();
    int CantFichasPorJugador();//es protected para poder heredar de clasico y hacer un doble 6 (este es el doble 9)
    int JugadorInicial();
    int CantidadJugadores{ get; }
    (int,int) DimensionTablero { get; }
    IContarPuntos contarPuntos { get; }
    ICalculaPuntos calculaPuntos { get; }
    // int JugadorActual ();
    /* List<Ficha[]> Repartir(List<Ficha> todasFichas);
bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano);
bool FinalizoPartida(int jugoJugadorActual, int turnosSinJugar);
int ProximoJugador(int jugadorActual);
(int, int) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador);//int fichasRestantesEquipoA, List<Ficha> equipaA, int fichasRestantesEquipoB, List<Ficha> equipoB
void AccionDespuesDeLaJugada(int jugadoractual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha);*/
}
