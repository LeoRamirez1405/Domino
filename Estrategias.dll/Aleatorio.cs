using Estructuras_Basicas;
using Reglas;
namespace Estrategias;
public class Aleatorio:Jugador,IJugar
{
    public Aleatorio(List<Ficha> mano):base(mano){ }

    public (int,Ficha,int)Jugar(int a, int b,IReglas reglas)
    {
        bool[] revisados = new bool[Mano.Count];
        Random r = new Random();
        int num = r.Next(0,Mano.Count);
        int intentos = 0;
        
            while(intentos<Mano.Count)
            {
                if(!revisados[num])
                {
                    if(reglas.ValidarJugada(a,Mano[num].arriba)) return (a,Mano[num],0);
                    if(reglas.ValidarJugada(a,Mano[num].abajo)) return (a,Mano[num],1);
                    if(reglas.ValidarJugada(b,Mano[num].arriba)) return (b,Mano[num],0);
                    if(reglas.ValidarJugada(b,Mano[num].abajo)) return (b,Mano[num],1);
                    intentos++;
                }
                num = r.Next(0,Mano.Count);
            }
            
        return (-1,Mano[0],0);
    }
}
