using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.domino.dll;

    class EAleatorio: IEstrategias
    {
    public (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual)
    {
        bool[] revisados = new bool[Mano.Count];
        Random r = new Random();
        int num = r.Next(0, revisados.Length);
        int intentos = 0;

        while (intentos < revisados.Length)
        {
            if (!revisados[num])
            {
                if (reglas.ValidarJugada(izquierda, Mano[num].Arriba))
                { Ficha f = Mano[num]; Mano.RemoveAt(num); return (f, 0); }
                if (reglas.ValidarJugada(izquierda, Mano[num].Abajo))
                { Ficha f = Mano[num]; Mano.RemoveAt(num); return (f, 0); }
                if (reglas.ValidarJugada(derecha, Mano[num].Arriba))
                { Ficha f = Mano[num]; Mano.RemoveAt(num); return (f, 1); }
                if (reglas.ValidarJugada(derecha, Mano[num].Abajo))
                { Ficha f = Mano[num]; Mano.RemoveAt(num); return (f, 1); }

                intentos++;
                revisados[num] = true;
            }
            num = r.Next(0, revisados.Length);
        }
        return (Mano[0], -1);
    }

}

