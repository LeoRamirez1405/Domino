<<<<<<< HEAD
﻿namespace Estrategias;
using System;
using Reglas;
using Estructuras_Basicas;
public class Aleatorio: Jugador, IJugar
=======
﻿using Estructuras_Basicas;
using Reglas;
namespace Estrategias;
public class Aleatorio:Jugador,IJugar
>>>>>>> b52235845526e1c87c9d16730b6925c14ec20983
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
                    if(reglas.ValidarJugada(a,Mano[num].arriba)) return (a,Mano[num],Mano[num].arriba);
                    if(reglas.ValidarJugada(a,Mano[num].abajo)) return (a,Mano[num],Mano[num].abajo);
<<<<<<< HEAD
=======
                    if(reglas.ValidarJugada(b,Mano[num].arriba)) return (b,Mano[num],Mano[num].arriba);
                    if(reglas.ValidarJugada(b,Mano[num].abajo)) return (b,Mano[num],Mano[num].abajo);
>>>>>>> b52235845526e1c87c9d16730b6925c14ec20983
                    intentos++;
                }
                num = r.Next(0,Mano.Count);
            }
        return (-1,Mano[0],Mano[0].arriba);
    }
}
