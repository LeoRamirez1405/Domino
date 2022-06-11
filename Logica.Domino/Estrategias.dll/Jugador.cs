namespace Estrategias;
using Estructuras_Basicas;
public abstract class Jugador<T>
{
    public List<Ficha<T>> Mano = new List<Ficha<T>>();//En la clase que hagas no pongas esto publico...
    //El arbitro necesita poder leer las fichas ...Hazme Una propiedad que se llame Mano 

    public int FichasRestantes{get;}
    public Jugador(List<Ficha<T>> Mano)
    {
        this.Mano = Mano;
        this.FichasRestantes = Mano.Count;
    }
}
