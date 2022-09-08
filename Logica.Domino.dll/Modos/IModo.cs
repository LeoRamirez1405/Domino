namespace Logica.domino.dll;
public interface IModo
{
   int CantidadJugadores{get;}
   bool TerminoModo(int ganador, List<int> puntosAcumulados);
   void TerminoUnaPratida(int ganador, List<int> puntosAcumulados);
   (int, int) GetGanador(bool EnEquipo);
}