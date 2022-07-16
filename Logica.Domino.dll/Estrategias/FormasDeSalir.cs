namespace Logica.domino.dll;

public class ESAleatorio:IEstrategiasSalir
{
    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    {
        Random r = new Random();
        int num = r.Next(0, Mano.Count);
        Ficha ficha = Mano[num];
        Mano.RemoveAt(num);
        return ficha;
    }
}

public class ESBotagorda:IEstrategiasSalir
{
    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    {
        Ficha gorda = Mano[0];
        Mano.RemoveAt(0);
        return (gorda);
    }
}

public class ESMatematico:IEstrategiasSalir
{
    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    {
        Ficha gorda = Mano[0];
        int suelto = 0;
        for (int i = 0; i < Mano.Count; i++)
        {
            if (Mano[i].Valor % 5 == 0)
            {
                gorda = Mano[i];
                suelto = i;
            }
        }
        Mano.RemoveAt(suelto);
        return (gorda);
    }
}

public class ESLeo:IEstrategiasSalir
{
    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    {
        Ficha gorda = Mano[0];
        Mano.RemoveAt(0);
        return (gorda);
    }
}

public class ESHumano:IEstrategiasSalir
{
    public Ficha Jugar(ref List<Ficha> Mano, IReglas reglas)
    {
        foreach (Ficha fic in Mano)
        {
            System.Console.Write(fic.ToString() + " ");
        }
        System.Console.WriteLine();
    
        System.Console.WriteLine("Cual desea jugar? (comienza por el 0)");   
        int respuesta = int.Parse(Console.ReadLine());
            Ficha ficha = Mano[0];
        if(respuesta >= 0 && respuesta < Mano.Count)
            ficha = Mano[respuesta]; 

        Mano.RemoveAt(respuesta);
        Console.Clear();
        return ficha;
    }
}