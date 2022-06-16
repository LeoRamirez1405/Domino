namespace Logica.domino.dll;
public class Doble9 : IDomino<int>
{
    int cantidad;
    public Doble9(int cant)
    {
        if(cantidad>maxFichas())
            cantidad = maxFichas();
        else
            cantidad = cant;
    }
    public List<Ficha<int>> fichas() => GeneraDomino();
    public int maxFichas() => 9;

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
        for (int i = 0; i < cantidad; i++)
        {
            for (int j = i; j < cantidad; j++)
            {
                fichas.Add(new Ficha<int>(partes[i],partes[j]));   
            }
        }
        return fichas;
    }
}