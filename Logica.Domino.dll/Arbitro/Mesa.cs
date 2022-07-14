using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.domino.dll;

    public class Mesa
    {
        public (Ficha, int) fichaActual_jugador;
        public List<(Ficha, int)> recorrido = new List<(Ficha, int)>();

        public void AddDer(Ficha ficha, int jugador)
        {
            recorrido.Add((ficha,jugador));
            fichaActual_jugador = (ficha,jugador);
        }
        public void AddIzq(Ficha ficha, int jugador)
        {
            List<(Ficha, int)> recorrido2 = new List<(Ficha, int)>{(ficha,jugador)};
            recorrido2.AddRange(recorrido);
            recorrido = recorrido2;
            fichaActual_jugador = (ficha,jugador);
        }
        public void imprimirMesa()
        {
            foreach (var item in recorrido)
            {
                System.Console.Write(item.Item1.ToString()); 
            }
            System.Console.WriteLine();
        }
    }

