namespace Logica.domino.dll;
public class Aleatorio : Jugador, IEstrategiasSalir, IEstrategias
{
    public Aleatorio(int ID,string nombre,List<Ficha> fichas)
                   :base(ID,nombre,fichas)
    { }
    //public Ficha Jugar(IReglas reglas)
    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    {
        return this.estrategiasSalir[0].Jugar(ref Mano,reglas);     
    }
    public (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual)
    //public (Ficha ,int)Jugar(ParteFicha  izquierda ,ParteFicha  derecha,IReglas  reglas)
    {
        return this.estrategias[0].Jugar(ref Mano,izquierda,derecha,reglas, jugadorActual);     
    }

}

