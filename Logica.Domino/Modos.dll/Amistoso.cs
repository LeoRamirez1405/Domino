using Estrategias;

namespace Modos.dll;
public class Amistoso<T> : IModo<T>
{
    public (List<Jugador<T>>,int) Gana(List<List<Jugador<T>>> Equipos,int[] PuntosEquipos, int equipoGanador, int valorRecogido)
    {
        return (Equipos[equipoGanador],valorRecogido);
    }
}
