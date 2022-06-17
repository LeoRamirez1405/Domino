namespace Logica.domino.dll;
public class Botagorda:IJugar
{
    List<Ficha> Mano;   
    public Botagorda(List<Ficha> mano)
    {
        this.Mano = mano;
        IJugar.Ordena(ref mano);
    }

    public List<Ficha> ObtenerFichas()
    {
       return Mano;
    }
    public Ficha Jugar(IReglas reglas)
    {
        Ficha gorda = Mano[0];
        Mano.RemoveAt(0);
        return (gorda);
    }
    public (Ficha,int)Jugar(ParteFicha izquierda ,ParteFicha derecha,IReglas reglas)
    {   
        for (int i = 0; i < Mano.Count; i++)
        {
            if(reglas.ValidarJugada(izquierda,Mano[i].Arriba)) 
            {Ficha f = Mano[i]; Mano.RemoveAt(i); return (f,0);}
            if(reglas.ValidarJugada(izquierda,Mano[i].Abajo)) 
            {Ficha f = Mano[i]; Mano.RemoveAt(i); return (f,0);}
            if(reglas.ValidarJugada(derecha,Mano[i].Arriba))
            {Ficha f = Mano[i]; Mano.RemoveAt(i); return (f,1);}
            if(reglas.ValidarJugada(derecha,Mano[i].Abajo)) 
            {Ficha f = Mano[i]; Mano.RemoveAt(i); return (f,1);} 
        }
        return (Mano[0],-1);
    }

}
