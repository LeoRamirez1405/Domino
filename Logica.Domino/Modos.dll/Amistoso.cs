using Estrategias;

namespace Modos.dll;
public class Amistoso<T> : IModo<T>
{
    public List<Jugador<T>> Gana(List<List<Jugador<T>>> Equipos, int equipoGanador, int valorRecogido)
    {
        return Equipos[equipoGanador];
    }
}
