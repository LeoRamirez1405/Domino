namespace Logica.domino.dll;
public class Aleatorio :IJugar 
{
    List<Ficha> Mano;   
    public Aleatorio(List<Ficha > mano)
    {
        this.Mano = mano;
    }

    public Ficha Jugar(IReglas  reglas)
    {
        Random r = new Random();
        int num = r.Next(0,Mano.Count);
        Ficha ficha = Mano[num];
        Mano.RemoveAt(num);
        return ficha;
    }
    public (Ficha ,int)Jugar(ParteFicha  izquierda ,ParteFicha  derecha,IReglas  reglas)
    {
        bool[] revisados = new bool[Mano.Count];
        Random r = new Random();
        int num = r.Next(0,Mano.Count);
        int intentos = 0;
        
            while(intentos<Mano.Count)
            {
                if(!revisados[num])
                {
                    if(reglas.ValidarJugada(izquierda,Mano[num].Arriba)) 
                    {Ficha  f = Mano[num]; Mano.RemoveAt(num); return (f,0);}
                    if(reglas.ValidarJugada(izquierda,Mano[num].Abajo)) 
                    {Ficha  f = Mano[num]; Mano.RemoveAt(num); return (f,0);}
                    if(reglas.ValidarJugada(derecha,Mano[num].Arriba))
                    {Ficha  f = Mano[num]; Mano.RemoveAt(num); return (f,1);}
                    if(reglas.ValidarJugada(derecha,Mano[num].Abajo)) 
                    {Ficha  f = Mano[num]; Mano.RemoveAt(num); return (f,1);}

                    intentos++;
                }
                num = r.Next(0,Mano.Count);
            }
        return (Mano[0],-1);
    }

    public List<Ficha> ObtenerFichas()
    {
       return Mano;
    }
}
