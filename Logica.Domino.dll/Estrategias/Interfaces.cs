namespace Logica.domino.dll;
public class Jugador
{
    public string nombre { get; }
    public int ID { get; }
    public List<Ficha> fichas;
    public List<IEstrategiasSalir> estrategiasSalir = new List<IEstrategiasSalir>{ new ESAleatorio(),new ESBotagorda(),new ESLeo(),new ESMatematico(),new ESHumano() };
    public List<IEstrategias> estrategias = new List<IEstrategias>{ new EAleatorio(),new EBotagorda(),new ELeo(),new EMatematico(),new EHumano(), new EPasador() };
    public static void Ordena(ref List<Ficha> mano)
    {
        for (int i = 0; i < mano.Count - 1; i++)
        {
            for (int j = i + 1; j < mano.Count; j++)
            {
                if (mano[i].Valor < mano[j].Valor)
                {
                    var aux = mano[i];
                    mano[i] = mano[j];
                    mano[j] = aux;
                }
            }
        }
    }
    public Jugador(int ID, string nombre,List<Ficha> fichas)
    {
        this.ID = ID;
        this.nombre = nombre;
        Ordena(ref fichas);
        this.fichas = fichas;
    }
}

public interface IEstrategiasSalir
{
    Ficha Jugar(ref List<Ficha> Mano, IReglas reglas);
}

public interface IEstrategias
{
    (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual);
}
public interface IJuegaConMesa
{
    public void juegaConMesa(Mesa mesa, (Ficha, int) ultimaJugada,bool huboJugada);
}


