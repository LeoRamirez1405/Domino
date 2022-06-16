namespace Logica.domino.dll;
public abstract class Jugador<T>
{
    public List<Ficha<T>> Mano = new List<Ficha<T>>();//En la clase que hagas no pongas esto publico...
    //El arbitro necesita poder leer las fichas ...Hazme Una propiedad que se llame Mano 
    public List<Ficha<T>> ObtenerFichas() => Mano;
    public int ValorMano()
    {
        int total = 0;
        foreach (var item in Mano)
        {
            total += item.Valor;           
        }
        return total;
    }

    public void Ordena(ref List<Ficha<T>> mano)
    {
        for (int i = 0; i < mano.Count - 1; i++)
        {
            for (int j = i + 1; j < mano.Count; j++)
            {
                if (mano[i].Valor < mano[j].Valor)
                {
                    var aux = mano[i];
                    mano[i] = mano[j];
                    mano[j] = aux;
                }
            }
        }
    }
    public int FichasRestantes{get;}
    public Jugador(List<Ficha<T>> Mano)
    {
        this.Mano = Mano;
        this.FichasRestantes = Mano.Count;
    }
}
