using Estructuras_Basicas; 
public class Doble6 : IDomino<int>
{
    public List<Ficha<int>> fichas() => GeneraDomino();
    public int fichasPorJugador() => 7;
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

        List<ParteFicha<int>> partes = new List<ParteFicha<int>>{cero,uno,dos,tres,cuatro,cinco,seis};

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