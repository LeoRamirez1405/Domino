namespace Logica.domino.dll;
public class Humano :Jugador,IEstrategias,IEstrategiasSalir 
{
    public Humano(int ID,string nombre,List<Ficha> fichas)
                   :base(ID,nombre,fichas)
    { }
    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    //public Ficha Jugar(IReglas reglas)
    {
        return this.estrategiasSalir[4].Jugar(ref Mano,reglas);     
    }
    //public (Ficha ,int)Jugar(ParteFicha  izquierda ,ParteFicha  derecha,IReglas  reglas)
    public (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual)
    {
        return this.estrategias[4].Jugar(ref Mano,izquierda,derecha,reglas, jugadorActual);     
    }
}