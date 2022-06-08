using Estrategias;

namespace Modos.dll;
public class Match<T> : IModo<T>
{
    int cantidad;
    public Match(int cantidad)
    {
        this.cantidad = cantidad;
    }
    public (List<Jugador<T>>,int) Gana(List<List<Jugador<T>>> Equipos,int[] PuntosEquipos, int equipoGanador, int valorRecogido)
    {
        //equipoGanador tienen que empezarlo en 0
        PuntosEquipos[equipoGanador] += 1;
        if(PuntosEquipos[equipoGanador]>= cantidad)
            return (Equipos[equipoGanador],PuntosEquipos[equipoGanador]);
        return (Equipos[0],-1);
    }
}