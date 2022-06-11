using System.Collections;
namespace Estructuras_Basicas;

public class Ficha<T> 
{
    ParteFicha<T> arriba;
    ParteFicha<T> abajo;
    public int Valor;
    public Ficha(ParteFicha<T> arriba,ParteFicha<T> abajo)
    {
        this.abajo = abajo;
        this.arriba = arriba;
        this.Valor = this.abajo.Valor + this.arriba.Valor;
    }

    public ParteFicha<T> Arriba { get => arriba;}
    public ParteFicha<T> Abajo { get => abajo;}
}

<<<<<<< HEAD
public class ParteFicha<T>
=======
public class ParteFicha<T>: IEquatable<ParteFicha<T>>
>>>>>>> 197984bc55f12d803883120658a5ee1d78efc156
{
    T parte;
    public int Valor {get;}
    public ParteFicha(T parte,int valor)
    {
        this.parte = parte;
        this.Valor = valor;
    }
<<<<<<< HEAD
=======
    public bool Equals(ParteFicha<T>? parte) => this.Valor == parte?.Valor;
    public static bool operator == (ParteFicha<T> a,ParteFicha<T> b) => a.Equals(b);
    public static bool operator != (ParteFicha<T> a,ParteFicha<T> b) => !a.Equals(b);
>>>>>>> 197984bc55f12d803883120658a5ee1d78efc156
}

public enum EstadoJuego
{
    Null,
    ListoParaComenzar,
    EnCurso,
    EnPausa

}
