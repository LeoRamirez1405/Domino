namespace Logica.domino.dll;
public class Pasador : Jugador, IEstrategiasSalir, IEstrategias
{
    public Dictionary<int, List<int>> pases = new Dictionary<int, List<int>>();

    public Pasador(int ID,string nombre,List<Ficha> fichas)
                   :base(ID,nombre,fichas)
    { }
    public (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual)
    {
        return this.estrategias[5].Jugar(ref Mano,izquierda,derecha,reglas, jugadorActual);     
    }

    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    {
        return base.estrategiasSalir[0].Jugar(ref Mano,reglas);
    }
}
   