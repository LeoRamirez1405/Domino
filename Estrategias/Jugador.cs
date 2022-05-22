namespace Estrategias;
public abstract class Jugador
{
    public List<Ficha> Mano = new List<Ficha>();
    public Jugador(List<Ficha> Mano)
    {
        this.Mano = Mano;
    }
}
