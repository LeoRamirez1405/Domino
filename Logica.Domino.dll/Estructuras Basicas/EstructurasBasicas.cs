using System.Collections;
namespace Estructuras_Basicas;

public class Ficha<T> 
{
    ParteFicha<T> arriba;
    ParteFicha<T> abajo;
    public int Valor {get ;}
    public Ficha(ParteFicha<T> arriba,ParteFicha<T> abajo)
    {
        this.abajo = abajo;
        this.arriba = arriba;
        this.Valor = this.abajo.Valor + this.arriba.Valor;
    }

    public ParteFicha<T> Arriba { get => arriba;}
    public ParteFicha<T> Abajo { get => abajo;}
}

public class ParteFicha<T>: IEquatable<ParteFicha<T>>
{
    T parte;
    public int Valor {get;}
    public ParteFicha(T parte,int valor)
    {
        this.parte = parte;
        this.Valor = valor;
    }

    public bool Equals(ParteFicha<T>? parte) => this.Valor == parte?.Valor;
    public static bool operator == (ParteFicha<T> a,ParteFicha<T> b) => a.Equals(b);
    public static bool operator != (ParteFicha<T> a,ParteFicha<T> b) => !a.Equals(b);
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
