using Estructuras_Basicas;
using Reglas;
namespace Estrategias;
public class Aleatorio<T>:Jugador<T>,IJugar<T>
{
    public Aleatorio(List<Ficha<T>> mano):base(mano){ }

    public Ficha<T>Jugar(IReglas<T> reglas)
    {
        Random r = new Random();
        int num = r.Next(0,Mano.Count);
        return (Mano[num]);
    }
    public (Ficha<T>,int)Jugar(ParteFicha<T> izquierda ,ParteFicha<T> derecha,IReglas<T> reglas)
    {
        bool[] revisados = new bool[Mano.Count];
        Random r = new Random();
        int num = r.Next(0,Mano.Count);
        int intentos = 0;
        
            while(intentos<Mano.Count)
            {
                if(!revisados[num])
                {
                    if(reglas.ValidarJugada(izquierda,Mano[num].Arriba)) 
                    {Ficha<T> f = Mano[num]; Mano.RemoveAt(num); return (f,0);}
                    if(reglas.ValidarJugada(izquierda,Mano[num].Abajo)) 
                    {Ficha<T> f = Mano[num]; Mano.RemoveAt(num); return (f,0);}
                    if(reglas.ValidarJugada(derecha,Mano[num].Arriba))
                    {Ficha<T> f = Mano[num]; Mano.RemoveAt(num); return (f,1);}
                    if(reglas.ValidarJugada(derecha,Mano[num].Abajo)) 
                    {Ficha<T> f = Mano[num]; Mano.RemoveAt(num); return (f,1);}

                    intentos++;
                }
                num = r.Next(0,Mano.Count);
            }
        return (Mano[0],-1);
    }
}
