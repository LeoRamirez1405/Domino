using Estructuras_Basicas; 
interface IDomino<T>
{
    int maxJugadores();
    int fichasPorJugador();
    List<Ficha<T>> fichas();
}

public class Doble9 : IDomino<int>
{
    public List<Ficha<int>> fichas() => GeneraDomino();
    public int fichasPorJugador() => 10;
    public int maxJugadores() => 4;

    List<Ficha<int>> GeneraDomino()
    {
        ParteFicha<int> cero = new ParteFicha<int>(0,0);
        ParteFicha<int> uno = new ParteFicha<int>(1,1);
        ParteFicha<int> dos = new ParteFicha<int>(2,2);
        ParteFicha<int> tres = new ParteFicha<int>(3,3);
        ParteFicha<int> cuatro = new ParteFicha<int>(4,4);
        ParteFicha<int> cinco = new ParteFicha<int>(5,5);
        ParteFicha<int> seis = new ParteFicha<int>(6,6);
        ParteFicha<int> siete = new ParteFicha<int>(7,7);
        ParteFicha<int> ocho = new ParteFicha<int>(8,8);
        ParteFicha<int> nueve = new ParteFicha<int>(9,9);

        List<ParteFicha<int>> partes = new List<ParteFicha<int>>{cero,uno,dos,tres,cuatro,cinco,seis,siete,ocho,nueve};

        List<Ficha<int>> fichas = new List<Ficha<int>>();
        for (int i = 0; i < partes.Count; i++)
        {
            for (int j = i; j < partes.Count; j++)
            {
                fichas.Add(new Ficha<int>(partes[i],partes[j]));   
            }
        }
        return fichas;
    }
}