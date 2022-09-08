using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.domino.dll;


class Leo: IEstrategias
{
    public (Ficha, int) Jugar(ref List<Ficha> Mano, ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual)
    {
        List<Ficha> posiblesIzquierdas = new List<Ficha>();
        List<int> posManoIzq = new List<int>();
        List<Ficha> posiblesDerechas = new List<Ficha>();
        List<int> posManoDer = new List<int>();

        for (int i = 0; i < Mano.Count; i++)
        {
            if (reglas.ValidarJugada(izquierda, Mano[i].Arriba))
            { posiblesIzquierdas.Add(Mano[i]); posManoIzq.Add(i); }
            if (reglas.ValidarJugada(izquierda, Mano[i].Abajo))
            { posiblesIzquierdas.Add(Mano[i]); posManoIzq.Add(i); }
            if (reglas.ValidarJugada(derecha, Mano[i].Arriba))
            { posiblesDerechas.Add(Mano[i]); posManoDer.Add(i); }
            if (reglas.ValidarJugada(derecha, Mano[i].Abajo))
            { posiblesDerechas.Add(Mano[i]); posManoDer.Add(i); }

        }
        if (posiblesIzquierdas.Count == 0 && posiblesDerechas.Count == 0)
            return (Mano[0], -1);
        else
        {
            if (posiblesIzquierdas.Count > posiblesDerechas.Count)
            {
                Mano.RemoveAt(posManoIzq[0]);
                return (posiblesIzquierdas[0], 0);
            }
            Mano.RemoveAt(posManoDer[0]);
            return (posiblesDerechas[0], 1);
        }
    }
}

