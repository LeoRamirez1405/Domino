namespace Logica.domino.dll;

public interface IDomino<T>
{
    int maxFichas();
    List<Ficha<T>> fichas();
}
