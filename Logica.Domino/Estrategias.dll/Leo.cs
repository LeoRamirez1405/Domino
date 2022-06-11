using Estructuras_Basicas;
using Reglas;
namespace Estrategias;
public class Leo<T>:Jugador<T>,IJugar<T>
{
    public Leo(List<Ficha<T>> mano):base(mano)
    {
        Ordena(ref mano);
    }

    public Ficha<T>Jugar(IReglas<T> reglas)
    {
        Ficha<T> gorda = Mano[0];
        Mano.RemoveAt(0);
        return (gorda);
    }
    public (Ficha<T>,int)Jugar(ParteFicha<T> izquierda ,ParteFicha<T> derecha,IReglas<T> reglas)
    {   
        List<Ficha<T>> posiblesIzquierdas = new List<Ficha<T>>();
        List<int> posManoIzq = new List<int>();
        List<Ficha<T>> posiblesDerechas = new List<Ficha<T>>();
        List<int> posManoDer = new List<int>();

        for (int i = 0; i < Mano.Count; i++)
        {
            if(reglas.ValidarJugada(izquierda,Mano[i].Arriba)) 
                {posiblesIzquierdas.Add(Mano[i]); posManoIzq.Add(i);}
            if(reglas.ValidarJugada(izquierda,Mano[i].Abajo)) 
                {posiblesIzquierdas.Add(Mano[i]); posManoIzq.Add(i);}
            if(reglas.ValidarJugada(derecha,Mano[i].Arriba))
                {posiblesDerechas.Add(Mano[i]); posManoDer.Add(i);}
            if(reglas.ValidarJugada(derecha,Mano[i].Abajo)) 
                {posiblesDerechas.Add(Mano[i]); posManoDer.Add(i);}

        }
        if (posiblesIzquierdas.Count == 0 && posiblesDerechas.Count == 0)
            return (Mano[0],-1);
        else
        {
            if(posiblesIzquierdas.Count>posiblesDerechas.Count)
            {
                Mano.RemoveAt(posManoIzq[0]);
                return (posiblesIzquierdas[0],0);
            }
            Mano.RemoveAt(posManoDer[0]);
            return (posiblesDerechas[0],1);
        }
    }

    public void Ordena(ref List<Ficha<T>> mano)
    {
        for (int i = 0; i < mano.Count-1; i++)
        {
            for (int j = i+1; j < mano.Count; j++)
            {
                if(mano[i].Valor<mano[j].Valor)
                {
                    var aux =mano[i];
                    mano[i] = mano[j];
                    mano[j] = aux;
                }   
            }
        }   
    }
}
