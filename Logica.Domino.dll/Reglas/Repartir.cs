namespace Logica.domino.dll;

public interface IRepartir
{
    public List<Ficha[]> Repartir(List<Ficha> todasFichas, int CantidadJugadores, int cantFichasPorJugador);//la lista de fichas que se recibe como paarametro son todas las fichas del juego
}

public class Repartir_Clasico: IRepartir
{
    public List<Ficha[]> Repartir(List<Ficha> todasFichas,int CantidadJugadores,int cantFichasPorJugador)//la lista de fichas que se recibe como paarametro son todas las fichas del juego
    {
        //aqui se modifica la coleccion de fichas general para que ell arbitro solo se quede con las fichas sobrantes luego de repartir
        List<Ficha[]> result = new List<Ficha[]>();
        System.Random r = new Random();
        for (int i = 0; i < CantidadJugadores; i++)//cantJugadores
        {
            Ficha[] resultemporal = new Ficha[cantFichasPorJugador];//cantFichas
            for (int j = 0; j < resultemporal.Length; j++)//cantFichas
            {
                int pos = r.Next(0, todasFichas.Count);
                resultemporal[j] = todasFichas[pos];
                todasFichas.RemoveAt(pos);
            }
            result.Add(resultemporal);
        }
        return result;
    }
}