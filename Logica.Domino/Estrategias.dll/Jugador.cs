namespace Estrategias;
using Estructuras_Basicas;
public abstract class Jugador<T>
{
    public List<Ficha<T>> Mano = new List<Ficha<T>>();
<<<<<<< HEAD
    public Jugador(List<Ficha<T>> Mano)
    {
        this.Mano = Mano;
=======
    public int FichasRestantes{get;}
    public Jugador(List<Ficha<T>> Mano)
    {
        this.Mano = Mano;
        this.FichasRestantes = Mano.Count;
>>>>>>> 197984bc55f12d803883120658a5ee1d78efc156
    }
}
