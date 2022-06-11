using Estructuras_Basicas;
using Reglas;
namespace Estrategias;
public class Botagorda<T>:Jugador<T>,IJugar<T>
{
    public Botagorda(List<Ficha<T>> mano):base(mano)
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
        for (int i = 0; i < Mano.Count; i++)
        {
            if(reglas.ValidarJugada(izquierda,Mano[i].Arriba)) 
            {Ficha<T> f = Mano[i]; Mano.RemoveAt(i); return (f,0);}
            if(reglas.ValidarJugada(izquierda,Mano[i].Abajo)) 
            {Ficha<T> f = Mano[i]; Mano.RemoveAt(i); return (f,0);}
            if(reglas.ValidarJugada(derecha,Mano[i].Arriba))
            {Ficha<T> f = Mano[i]; Mano.RemoveAt(i); return (f,1);}
            if(reglas.ValidarJugada(derecha,Mano[i].Abajo)) 
            {Ficha<T> f = Mano[i]; Mano.RemoveAt(i); return (f,1);} 
        }
        return (Mano[0],-1);
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
