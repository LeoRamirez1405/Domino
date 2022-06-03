namespace Estrategias;
using Estructuras_Basicas;

public interface IJugar
{
    (Ficha,int) Jugar(Ficha a, Ficha b);//devuelve una ficha y 0 si juega pos la primera ficha q recibe y 1 si juega x la 2da ficha q recibe
}


