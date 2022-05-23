namespace Estrategias;
using Estructuras_Basicas;
using Reglas;
public interface IJugar
{
    (int,Ficha,int) Jugar(int a, int b,IReglas reglas);
}


