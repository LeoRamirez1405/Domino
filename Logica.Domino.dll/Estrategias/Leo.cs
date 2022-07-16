namespace Logica.domino.dll;


public class Leo:Jugador,IEstrategias,IEstrategiasSalir
{
    public Leo(int ID,string nombre,List<Ficha> fichas)
                   :base(ID,nombre,fichas)
    { }
    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    {
        return this.estrategiasSalir[2].Jugar(ref Mano,reglas);     
    }
    public (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual)
    {
        return this.estrategias[2].Jugar(ref Mano,izquierda,derecha,reglas, jugadorActual);     
    }
}
