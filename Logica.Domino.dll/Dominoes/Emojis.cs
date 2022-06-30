namespace Logica.domino.dll;
public class Emojis : IDomino
{
    int cantidad = 9;
    public int maxFichas() => cantidad+1;
    public List<Ficha> fichas(int cant)
    {
        if(cant > cantidad) cant = cantidad;
        ParteFicha cinco = new ParteFicha("😒",0);
        ParteFicha dos = new ParteFicha("👍",1);
        ParteFicha tres = new ParteFicha("👌",2);
        ParteFicha uno = new ParteFicha("😎",3);
        ParteFicha cuatro = new ParteFicha("😁",4);
        ParteFicha cero = new ParteFicha("😙",5);
        ParteFicha seis = new ParteFicha("😂",6);
        ParteFicha siete = new ParteFicha("😊",7);
        ParteFicha ocho = new ParteFicha("😘",8);
        ParteFicha nueve = new ParteFicha("❤️",9);

        List<ParteFicha> partes = new List<ParteFicha>{cero,uno,dos,tres,cuatro,cinco,seis,siete,ocho,nueve};

        List<Ficha> fichas = new List<Ficha>();

        for (int i = 0; i <= cant; i++)
        {
            for (int j = i; j <= cant; j++)
            {
                fichas.Add(new Ficha(partes[i],partes[j]));   
            }
        }
        return fichas;
    }
}