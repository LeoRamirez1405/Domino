using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.domino.dll;

public class EPasador : IEstrategias ,IJuegaConMesa
{
    public Dictionary<int, List<int>> pases = new Dictionary<int, List<int>>();
    public (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual)
    {
            int aux = 0;
            if(!reglas.invertido && jugadorActual < reglas.CantidadJugadores-1){aux = jugadorActual+1;}
            else if(reglas.invertido && jugadorActual == 0){aux = reglas.CantidadJugadores-1;}
            else if(reglas.invertido && jugadorActual < reglas.CantidadJugadores-1){aux = jugadorActual-1;}
                
                Ficha f = Mano[0]; 
                int index = -1;  
                int pos = -1;             
                    for (int num = 0; num < Mano.Count; num++)
                    {
                        if (reglas.ValidarJugada(izquierda, Mano[num].Arriba))
                        {  
                            f = Mano[num]; pos = 0; index = num;
                            if(pases.ContainsKey(aux))
                            {
                                foreach (var item in pases[aux])
                                {
                                    if(item == Mano[num].Abajo.Valor) 
                                    {
                                         Mano.RemoveAt(num); return (f, 0); 
                                    }
                                }
                            }
                        }
                        if (reglas.ValidarJugada(izquierda, Mano[num].Abajo))
                        {   
                            f = Mano[num]; pos = 0; index = num;
                            if(pases.ContainsKey(aux))
                            {
                                foreach (var item in pases[aux] )
                                {
                                    if(item == Mano[num].Arriba.Valor) 
                                    {
                                        Mano.RemoveAt(num); return (f, 0); 
                                    }
                                }
                            }

                        }
                        if (reglas.ValidarJugada(derecha, Mano[num].Arriba))
                        { 
                            f = Mano[num]; pos = 1; index = num;
                            if(pases.ContainsKey(aux))
                            {
                                foreach (var item in pases[aux] )
                                {
                                    if(item == Mano[num].Abajo.Valor) 
                                    {
                                        Mano.RemoveAt(num); return (f, 1); 
                                    }
                                }
                            }
                        }
                        if (reglas.ValidarJugada(derecha, Mano[num].Abajo))
                        {
                            f = Mano[num]; pos = 1; index = num;
                            if(pases.ContainsKey(aux))
                            {
                                foreach (var item in pases[aux] )
                                {
                                    if(item == Mano[num].Arriba.Valor) 
                                    {
                                        Mano.RemoveAt(num); return (f, 1); 
                                    }
                                }
                            }
                        }
                    }
                    if(index == -1) return (f,pos);
                Mano.RemoveAt(index);
                return (f,pos);      
    }
    
    public void juegaConMesa(Mesa mesa, (Ficha, int) ultimaJugada,bool huboJugada)
    {
        if (huboJugada) return;
        if(pases.ContainsKey(ultimaJugada.Item2))
        {
            if (!pases[ultimaJugada.Item2].Contains(mesa.recorrido[0].Item1.Arriba.Valor))
                pases[ultimaJugada.Item2].Add(mesa.recorrido[0].Item1.Arriba.Valor);
            if (!pases[ultimaJugada.Item2].Contains(mesa.recorrido[mesa.recorrido.Count - 1].Item1.Abajo.Valor))
                pases[ultimaJugada.Item2].Add(mesa.recorrido[mesa.recorrido.Count - 1].Item1.Abajo.Valor);
        }
        else
        {
            pases.Add(ultimaJugada.Item2, new List<int>{ mesa.recorrido[0].Item1.Arriba.Valor, mesa.recorrido[mesa.recorrido.Count - 1].Item1.Abajo.Valor});
        }
    }

}
   


