using System.Collections;

namespace Estructuras_Basicas;

public struct Ficha
{
    public int arriba{get;}
    public int abajo{get;}

    public Ficha(int arriba, int abajo)
    {
        this.abajo = abajo;
        this.arriba = arriba;
    }
}
