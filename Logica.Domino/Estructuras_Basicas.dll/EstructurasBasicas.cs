using System.Collections;
namespace Estructuras_Basicas;

public class Ficha
{
//     int arriba;
//     int abajo;

    ParteFicha arriba;
    int valorArriba;
    ParteFicha abajo;
    int valorAbajo;
    

    public Ficha(int arriba, int abajo)
    {
        this.abajo = new ParteFicha(abajo);
        this.arriba = new ParteFicha(arriba);
        this.valorAbajo = abajo;
        this.valorArriba = arriba;

    }

    public ParteFicha Arriba { get => arriba;}
    public ParteFicha Abajo { get => abajo;}
    public int ValorArriba { get => valorArriba; set => valorArriba = value; }
    public int ValorAbajo { get => valorAbajo; set => valorAbajo = value; }
}

public class ParteFicha
{
    int parte;
    public ParteFicha(int parte)
    {
        this.parte = parte;
    }
}

public enum EstadoJuego
{
    Null,
    ListoParaComenzar,
    EnCurso,
    EnPausa

}
