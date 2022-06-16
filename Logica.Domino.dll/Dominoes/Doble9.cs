namespace Logica.domino.dll;
public class Doble9 : IDomino
{
    int cantidad = 9;
    
    // public List<Ficha> fichas() => GeneraDomino(int cant);
    public int maxFichas() => cantidad;

    public List<Ficha> fichas(int cant)
    {
        if(cant > cantidad) cant = cantidad;
        ParteFicha cero = new ParteFicha(0,0);
        ParteFicha uno = new ParteFicha(1,1);
        ParteFicha dos = new ParteFicha(2,2);
        ParteFicha tres = new ParteFicha(3,3);
        ParteFicha cuatro = new ParteFicha(4,4);
        ParteFicha cinco = new ParteFicha(5,5);
        ParteFicha seis = new ParteFicha(6,6);
        ParteFicha siete = new ParteFicha(7,7);
        ParteFicha ocho = new ParteFicha(8,8);
        ParteFicha nueve = new ParteFicha(9,9);

        List<ParteFicha> partes = new List<ParteFicha>{cero,uno,dos,tres,cuatro,cinco,seis,siete,ocho,nueve};

        List<Ficha> fichas = new List<Ficha>();
        for (int i = 0; i < cant; i++)
        {
            for (int j = i; j < cant; j++)
            {
                fichas.Add(new Ficha(partes[i],partes[j]));   
            }
        }
        return fichas;
    }
}