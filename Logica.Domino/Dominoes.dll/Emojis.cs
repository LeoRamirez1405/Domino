using Estructuras_Basicas; 
public class Emojis : IDomino<string>
{
    public bool Equals(ParteFicha<string> a, ParteFicha<string> b) => a.Valor == b.Valor;
    public List<Ficha<string>> fichas() => GeneraDomino();
    public int fichasPorJugador() => 10;
    public int maxJugadores() => 4;

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

        for (int i = 0; i < partes.Count; i++)
        {
            for (int j = i; j < partes.Count; j++)
            {
                fichas.Add(new Ficha<string>(partes[i],partes[j]));   
            }
        }
        return fichas;
    }
}