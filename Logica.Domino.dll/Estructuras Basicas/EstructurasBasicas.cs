namespace Logica.domino.dll;
public class Ficha 
{
    ParteFicha arriba;
    ParteFicha abajo;
    public int Valor {get ;}
    public Ficha(ParteFicha arriba,ParteFicha abajo)
    {
        this.abajo = abajo;
        this.arriba = arriba;
        this.Valor = this.abajo.Valor + this.arriba.Valor;
    }

    public ParteFicha Arriba { get => arriba;}
    public ParteFicha Abajo { get => abajo;}
}

public class ParteFicha: IEquatable<ParteFicha>
{
    object parte;
    public int Valor {get;}
    public ParteFicha(object parte,int valor)
    {
        this.parte = parte;
        this.Valor = valor;
    }

    public bool Equals(ParteFicha? parte) => this.Valor == parte?.Valor;
    public static bool operator == (ParteFicha a,ParteFicha b) => a.Equals(b);
    public static bool operator != (ParteFicha a,ParteFicha b) => !a.Equals(b);
}

public enum EstadoJuego
{
    Null,
    ListoParaComenzar,
    EnCurso,
    EnPausa

}

public enum ParametrosDefinenGanador
{
    TurnosSinJugar,
    IndexJugadorActual,
    // FichasJugadorActual,
    FichasJugadores, // aqui se pasarian las fichas cada h=jugador en una lista d listas, luego con el index jugadore actual puedo ver las fichas del jugador actual
}
