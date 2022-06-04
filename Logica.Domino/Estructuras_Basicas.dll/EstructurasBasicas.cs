using System.Collections;
namespace Estructuras_Basicas;

public class Ficha<T>
{
    ParteFicha<T> arriba;
    ParteFicha<T> abajo;
    public int Valor;
    public Ficha(T arriba, T abajo)
    {
        this.abajo = new ParteFicha<T>(abajo);
        this.arriba = new ParteFicha<T>(arriba);
        this.Valor = abajo.Valor + arriba.Valor;
    }

    public ParteFicha<T> Arriba { get => arriba;}
    public ParteFicha<T> Abajo { get => abajo;}
}

public class ParteFicha<T>
{
    T parte;
    int Valor;
    public ParteFicha(T parte)
    {
        this.parte = parte;
        this.Valor =
    }
    private int Valorar(T Parte)
    {
        if(Parte is int) return Parte.;
        return 0;
    }
}

public enum EstadoJuego
{
    Null,
    ListoParaComenzar,
    EnCurso,
    EnPausa

}
