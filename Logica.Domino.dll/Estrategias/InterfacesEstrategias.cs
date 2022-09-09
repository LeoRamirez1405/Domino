namespace Logica.domino.dll;

public interface IEstrategiasSalir
{
    Ficha Jugar(ref List<Ficha> Mano, IReglas reglas);
}

public interface IEstrategias
{
    (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual);
}

public interface IJuegaConMesa
{
    void juegaConMesa(Mesa mesa, (Ficha, int) ultimaJugada,bool huboJugada);
}


