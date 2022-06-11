namespace Estrategias;
using Estructuras_Basicas;
using Reglas;
public interface IJugar<T>
{
   (Ficha<T>,int) Jugar(ParteFicha<T> a, ParteFicha<T> b, IReglas<T> reglas);//devuelve una ficha y 0 si juega pos la primera ficha q recibe y 1 si juega x la 2da ficha q recibe
   Ficha<T> Jugar(IReglas<T> reglas);//devuelve una ficha y 0 si juega pos la primera ficha q recibe y 1 si juega x la 2da ficha q recibe
}


