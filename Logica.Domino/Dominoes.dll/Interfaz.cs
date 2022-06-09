using Estructuras_Basicas;
interface IDomino<T>
{
    int maxJugadores();
    int fichasPorJugador();
    List<Ficha<T>> fichas();
}
