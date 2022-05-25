namespace Estrategias;
using Reglas;
public interface IJugar
{
    (int,Ficha,int) Jugar(int a, int b,IReglas reglas);
}

public struct Ficha
{
    int arriba;
    int abajo;
}
