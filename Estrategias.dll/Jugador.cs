namespace Estrategias;
using Estructuras_Basicas;
public abstract class Jugador
{
    public List<Ficha> Mano = new List<Ficha>();
    public Jugador(List<Ficha> Mano)
    {
        this.Mano = Mano;
    }
}
