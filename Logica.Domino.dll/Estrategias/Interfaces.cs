namespace Logica.domino.dll;
public class Jugador:IEstrategias,IEstrategiasSalir
{
    public string nombre { get; }
    public int ID { get; }
    public List<Ficha> fichas;
    public IEstrategiasSalir estrategiaSalir ;// = new List<IEstrategiasSalir>{ new ESAleatorio(),new ESBotagorda(),new ESLeo(),new ESMatematico(),new ESHumano() };
    public List<IEstrategias> estrategias ;//= new List<IEstrategias>{ new EAleatorio(),new EBotagorda(),new ELeo(),new EMatematico(),new EHumano(), new EPasador() };
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

    public (Ficha, int) Jugar(ref List<Ficha> Mano, ParteFicha izquierda, ParteFicha derecha, IReglas reglas, int jugadorActual)
    {
        Random r = new Random();
        int random = r.Next(0,estrategias.Count);
        return estrategias[random].Jugar(ref Mano,izquierda,derecha,reglas, jugadorActual);     
    }

    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    {
        return estrategiaSalir.Jugar(ref Mano,reglas);
    }

    public Jugador(int ID, string nombre,List<Ficha> fichas,List<IEstrategias> estrategias,IEstrategiasSalir estrategiaSalir)
    {
        this.ID = ID;
        this.nombre = nombre;
        Ordena(ref fichas);
        this.fichas = fichas;
        this.estrategias = estrategias;
        this.estrategiaSalir = estrategiaSalir;
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


