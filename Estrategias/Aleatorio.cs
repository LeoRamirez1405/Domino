namespace Estrategias;
using System;

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
                    if(reglas.Validar(a,Mano[num],0)) return (a,Mano[num],0);
                    if(reglas.Validar(a,Mano[num],1)) return (a,Mano[num],1);
                    intentos++;
                }
                num = r.Next(0,Mano.Count);
            }
        return (-1,Mano[0],0);
    }
}
