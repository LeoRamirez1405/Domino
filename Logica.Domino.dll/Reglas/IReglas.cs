namespace Logica.domino.dll;
public interface IReglas : IAccionDespuesDeLaJugada, IFinalizarJugada, IGanador, IProximoJugador, IRepartir, IValidarJugada
{
    bool equipo { get; }
    bool invertido{get;}
    int CantFichasTotalJuego();
    int CantFichasPorJugador();//es protected para poder heredar de clasico y hacer un doble 6 (este es el doble 9)
    int JugadorInicial();
    int CantidadJugadores{ get; }
    IContarPuntos contarPuntos { get; }
    ICalculaPuntos calculaPuntos { get; }
    }
