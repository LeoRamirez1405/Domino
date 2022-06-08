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

public class ParteFicha<T>
{
    T parte;
    public int Valor {get;}
    public ParteFicha(T parte,int valor)
    {
        this.parte = parte;
        this.Valor = valor;
    }
}

public enum EstadoJuego
{
    Null,
    ListoParaComenzar,
    EnCurso,
    EnPausa

}
