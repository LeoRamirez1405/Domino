namespace Logica.domino.dll;
public class Emojis : IDomino<string>
{
    int cantidad;
    public Emojis(int cant)
    {
         if(cantidad>maxFichas())
            cantidad = maxFichas();
        else
            cantidad = cant;
    }
    public int maxFichas() => 9;
    public List<Ficha<string>> fichas() => GeneraDomino();
    List<Ficha<string>> GeneraDomino()
    {
        ParteFicha<string> cero = new ParteFicha<string>("😙",0);
        ParteFicha<string> uno = new ParteFicha<string>("😎",1);
        ParteFicha<string> dos = new ParteFicha<string>("👍",2);
        ParteFicha<string> tres = new ParteFicha<string>("👌",3);
        ParteFicha<string> cuatro = new ParteFicha<string>("😁",4);
        ParteFicha<string> cinco = new ParteFicha<string>("😒",5);
        ParteFicha<string> seis = new ParteFicha<string>("😂",6);
        ParteFicha<string> siete = new ParteFicha<string>("😊",7);
        ParteFicha<string> ocho = new ParteFicha<string>("😘",8);
        ParteFicha<string> nueve = new ParteFicha<string>("❤️",9);

        List<ParteFicha<string>> partes = new List<ParteFicha<string>>{cero,uno,dos,tres,cuatro,cinco,seis,siete,ocho,nueve};

        List<Ficha<string>> fichas = new List<Ficha<string>>();

        for (int i = 0; i < cantidad; i++)
        {
            for (int j = i; j < cantidad; j++)
            {
                fichas.Add(new Ficha<string>(partes[i],partes[j]));   
            }
        }
        return fichas;
    }
}