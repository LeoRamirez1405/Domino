namespace Logica.domino.dll;
public interface IJugar
{
   (Ficha,int) Jugar(ParteFicha a, ParteFicha b, IReglas reglas);//devuelve una ficha y 0 si juega pos la primera ficha q recibe y 1 si juega x la 2da ficha q recibe
   Ficha Jugar(IReglas reglas);//devuelve una ficha y 0 si juega pos la primera ficha q recibe y 1 si juega x la 2da ficha q recibe
}


