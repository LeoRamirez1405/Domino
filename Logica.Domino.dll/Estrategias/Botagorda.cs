using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.domino.dll;

    class Botagorda: IEstrategias
    {
        public (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual)
        {
            for (int i = 0; i < Mano.Count; i++)
            {
                if (reglas.ValidarJugada(izquierda, Mano[i].Arriba))
                { Ficha f = Mano[i]; Mano.RemoveAt(i); return (f, 0); }
                if (reglas.ValidarJugada(izquierda, Mano[i].Abajo))
                { Ficha f = Mano[i]; Mano.RemoveAt(i); return (f, 0); }
                if (reglas.ValidarJugada(derecha, Mano[i].Arriba))
                { Ficha f = Mano[i]; Mano.RemoveAt(i); return (f, 1); }
                if (reglas.ValidarJugada(derecha, Mano[i].Abajo))
                { Ficha f = Mano[i]; Mano.RemoveAt(i); return (f, 1); }
            }
            return (Mano[0], -1);
        }
    }

