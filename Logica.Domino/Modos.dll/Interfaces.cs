using Estrategias;
public interface IModo<T>
{
    (List<Jugador<T>>,int) Gana(List<List<Jugador<T>>> Equipos, int[] PuntosEquipos,int equipoGanador, int valorRecogido);
}