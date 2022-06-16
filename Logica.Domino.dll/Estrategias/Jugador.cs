namespace Logica.domino.dll;
public abstract class Jugador
{
    public List<Ficha> Mano = new List<Ficha>();//En la clase que hagas no pongas esto publico...
    //El arbitro necesita poder leer las fichas ...Hazme Una propiedad que se llame Mano 
    public List<Ficha> ObtenerFichas() => Mano;
    public int ValorMano()
    {
        int total = 0;
        foreach (var item in Mano)
        {
            total += item.Valor;           
        }
        return total;
    }

    public void Ordena(ref List<Ficha> mano)
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
    public Jugador(List<Ficha> Mano)
    {
        this.Mano = Mano;
        this.FichasRestantes = Mano.Count;
    }
}
