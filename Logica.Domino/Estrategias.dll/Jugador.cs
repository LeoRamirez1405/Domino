namespace Estrategias;
using Estructuras_Basicas;
public abstract class Jugador<T>
{
    public List<Ficha<T>> Mano = new List<Ficha<T>>();
    public int FichasRestantes{get;}
    public Jugador(List<Ficha<T>> Mano)
    {
        this.Mano = Mano;
        this.FichasRestantes = Mano.Count;
    }
}
