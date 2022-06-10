using Estructuras_Basicas;
public interface IDomino<T>
{
    int maxJugadores();
    int fichasPorJugador();
    List<Ficha<T>> fichas();
}
