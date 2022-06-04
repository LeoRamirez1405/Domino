using Estrategias;
public interface IModo<T>
{
    List<Jugador<T>> Gana(List<List<Jugador<T>>> Equipos,int equipoGanador, int valorRecogido);
}