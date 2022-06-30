namespace Logica.domino.dll;

public interface IContarPuntos
{
    public List<int> ContarPuntos(List<List<Ficha>> fichasJugadores);
}

public class ContarPuntos_Clasico : IContarPuntos
{
    public List<int> ContarPuntos(List<List<Ficha>> fichasJugadores)
    {
        List<int> valorFichasPorJugador = new List<int>();
        foreach (var x in fichasJugadores)
        {
            int puntos = 0;
            foreach (var y in x)
                puntos += y.Valor;
            valorFichasPorJugador.Add(puntos);
        }
        return valorFichasPorJugador;
    }
}

public class ContarPuntos_DobleDoble: IContarPuntos
{
    public List<int> ContarPuntos(List<List<Ficha>> fichasJugadores)
    {
        List<int> valorFichasPorJugador = new List<int>();
        foreach (var x in fichasJugadores)
        {
            int puntos = 0;
            foreach (var y in x)
            {
                puntos += y.Valor;
                if (y.Abajo.Valor == y.Arriba.Valor)
                    puntos += y.Valor;
            }
            valorFichasPorJugador.Add(puntos);
        }
        return valorFichasPorJugador;
    }
}

public class ContarPuntos_ManoDura: IContarPuntos
{
    public List<int> ContarPuntos(List<List<Ficha>> fichasJugadores)
    {
        List<int> valorFichasPorJugador = new List<int>();
        foreach (var x in fichasJugadores)
        {
            int puntos = 0;
            foreach (var y in x)
                puntos += y.Valor;
            valorFichasPorJugador.Add(puntos*x.Count);
        }
        return valorFichasPorJugador;
    }
}
