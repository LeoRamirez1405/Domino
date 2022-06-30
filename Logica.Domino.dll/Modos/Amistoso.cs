namespace Logica.domino.dll;
public class Amistoso : IModo
{
    // List<IJugar> jugadores;
    Arbitro arbitro;
    int[] PuntosJugadores;
    bool EnEquipo;
    public Amistoso(int noJug,bool EnEquipo)
    {
        this.EnEquipo = EnEquipo;
        this.arbitro = new Arbitro(noJug);
        this.PuntosJugadores = new int[arbitro.GetJugadores().Count];
    }
    public (int,int) Gana(bool EnEquipo)
    {
        (int,List<int>) ganadorEs =  arbitro.Jugando();

        if(EnEquipo)
        {
            if (ganadorEs.Item1 % 2 == 0)
                return (0, ganadorEs.Item2[ganadorEs.Item1]);
            return (1, ganadorEs.Item2[ganadorEs.Item1]);
        }
        return (ganadorEs.Item1, ganadorEs.Item2[ganadorEs.Item1]);

    }
}
