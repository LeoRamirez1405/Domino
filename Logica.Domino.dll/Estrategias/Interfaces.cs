namespace Logica.domino.dll;
public interface IJugar
{
    public List<Ficha> ObtenerFichas();
    public int ValorMano(List<Ficha> Mano)
    {
        int total = 0;
        foreach (var item in Mano)
        {
            total += item.Valor;           
        }
        return total;
    }

    public static void Ordena(ref List<Ficha> mano)
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
   (Ficha,int) Jugar(ParteFicha a, ParteFicha b, IReglas reglas);//devuelve una ficha y 0 si juega pos la primera ficha q recibe y 1 si juega x la 2da ficha q recibe
   Ficha Jugar(IReglas reglas);//devuelve una ficha y 0 si juega pos la primera ficha q recibe y 1 si juega x la 2da ficha q recibe
}


