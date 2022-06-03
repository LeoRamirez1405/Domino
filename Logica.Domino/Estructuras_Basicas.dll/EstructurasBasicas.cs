using System.Collections;
namespace Estructuras_Basicas;

public class Ficha
{
//     int arriba;
//     int abajo;

    ParteFicha arriba;
    ParteFicha abajo;
    

    public Ficha(int arriba, int abajo)
    {
        this.abajo = new ParteFicha(abajo);
        this.arriba = new ParteFicha(arriba);
    }

    public ParteFicha Arriba { get => arriba; set => arriba = value; }
    public ParteFicha Abajo { get => abajo; set => abajo = value; }
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
