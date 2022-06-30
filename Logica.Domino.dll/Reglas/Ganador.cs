namespace Logica.domino.dll;

public interface IGanador
{
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos);
    public static int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {
        int totalPuntos = 0;
        for (int i = 0; i < puntosPorJugador.Count; i++)
        {
            if (i == jugadorGanador) continue;
            totalPuntos += puntosPorJugador[i];
        }
        return totalPuntos;
    }
    public static void ADevolver(int jugadorGanador, ref List<int> puntosporJugador)
    {
        for (int i = 0; i < puntosporJugador.Count; i++)
        {
            if (jugadorGanador == i)
            {
                puntosporJugador[i] = IGanador.CalcularPuntosGanoJugador(jugadorGanador, puntosporJugador);
            }
            else
                puntosporJugador[i] = 0;
        }
    }
}

public class Ganador_Clasico : IGanador
{
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos)
    {
        int jugadorGanador = -1;
        List<int> valorFichasPorJugador = new List<int>();
        if (definenGanador.ContainsKey(ParametrosDefinenGanador.FichasJugadores) && definenGanador.ContainsKey(ParametrosDefinenGanador.IndexJugadorActual))
        {
            List<List<Ficha>> fichasJugadores = (List<List<Ficha>>)definenGanador[ParametrosDefinenGanador.FichasJugadores];
            int jugadorActual = (int)definenGanador[ParametrosDefinenGanador.IndexJugadorActual];

            valorFichasPorJugador = contarPuntos.ContarPuntos(fichasJugadores);

            //estas condiciones no son necesarias 


            if (fichasJugadores[jugadorActual].Count == 0)//esto seria equivalente a ver si al jugador actual le quedan 0 fichas
            {
                jugadorGanador = jugadorActual;
                IGanador.ADevolver(jugadorGanador, ref valorFichasPorJugador);
                return (jugadorGanador, valorFichasPorJugador);
            }
            else if (definenGanador.ContainsKey(ParametrosDefinenGanador.TurnosSinJugar))//else
            {
                int min = int.MaxValue;
                for (int i = 0; i < cantidadJugadores; i++)//this.cantJugadores
                {
                    if (valorFichasPorJugador[i] < min)
                    {
                        min = valorFichasPorJugador[i];
                        jugadorGanador = i;
                    }
                }
                bool empate = false;
                for (int i = 0; i < cantidadJugadores; i++)
                {
                    if (valorFichasPorJugador[i] == min)
                    {
                        if (empate == true)
                            return (-1, valorFichasPorJugador);
                        else
                            empate = true;
                    }
                }

                IGanador.ADevolver(jugadorGanador, ref valorFichasPorJugador);
                return (jugadorGanador, valorFichasPorJugador);
            }
        }

        return (jugadorGanador, valorFichasPorJugador);//Ojo buscar los tipos de excepciones
    }
}

public class Ganador_Quincena : IGanador
{
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos)
    {
        (int, List<int>) resultAux = new Ganador_Clasico().Ganador(definenGanador, cantidadJugadores, contarPuntos);
       
        List<int> list = new List<int>();
        foreach (var item in resultAux.Item2)
        {
            list.Add(0);
        }
        if (definenGanador.ContainsKey(ParametrosDefinenGanador.SeTrancoElJuego) && (bool)definenGanador[ParametrosDefinenGanador.SeTrancoElJuego])
        {
            list[resultAux.Item1] += 5;
            return (resultAux.Item1, list);
        }
        list[resultAux.Item1] += 10;
        return (resultAux.Item1, list);
    }
}

//Gana el que más puntos tiene
public class Ganador_Inverso : IGanador
{
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos)
    {
        int jugadorGanador = -1;
        List<int> valorFichasPorJugador = new List<int>();
        if (definenGanador.ContainsKey(ParametrosDefinenGanador.FichasJugadores) && definenGanador.ContainsKey(ParametrosDefinenGanador.IndexJugadorActual))
        {
            List<List<Ficha>> fichasJugadores = (List<List<Ficha>>)definenGanador[ParametrosDefinenGanador.FichasJugadores];
            int jugadorActual = (int)definenGanador[ParametrosDefinenGanador.IndexJugadorActual];

            valorFichasPorJugador = contarPuntos.ContarPuntos(fichasJugadores);

            //estas condiciones no son necesarias 

            if (fichasJugadores[jugadorActual].Count == 0)//esto seria equivalente a ver si al jugador actual le quedan 0 fichas
            {
                jugadorGanador = jugadorActual;
                IGanador.ADevolver(jugadorGanador, ref valorFichasPorJugador);
                return (jugadorGanador, valorFichasPorJugador);
            }
            else if (definenGanador.ContainsKey(ParametrosDefinenGanador.TurnosSinJugar))//else
            {
                int max = int.MinValue;
                for (int i = 0; i < cantidadJugadores; i++)//this.cantJugadores
                {
                    if (valorFichasPorJugador[i] > max)
                    {
                        max = valorFichasPorJugador[i];
                        jugadorGanador = i;
                    }
                }

                bool empate = false;
                for (int i = 0; i < cantidadJugadores; i++)
                {
                    if (valorFichasPorJugador[i] == max)
                    {
                        if (empate == true)
                            return (-1, valorFichasPorJugador);
                        else
                            empate = true;
                    }
                }
            }
        }
        IGanador.ADevolver(jugadorGanador, ref valorFichasPorJugador);
        return (jugadorGanador, valorFichasPorJugador);//Ojo buscar los tipos de excepciones
    }

}

public class Ganador_SiTrancaAcumulaSusPuntos : IGanador
{
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos)
    {
        int jugadorGanador = -1;
        List<int> valorFichasPorJugador = new List<int>();
        if (definenGanador.ContainsKey(ParametrosDefinenGanador.FichasJugadores) && definenGanador.ContainsKey(ParametrosDefinenGanador.IndexJugadorActual))
        {
            List<List<Ficha>> fichasJugadores = (List<List<Ficha>>)definenGanador[ParametrosDefinenGanador.FichasJugadores];
            int jugadorActual = (int)definenGanador[ParametrosDefinenGanador.IndexJugadorActual];

            valorFichasPorJugador = contarPuntos.ContarPuntos(fichasJugadores);

            //estas condiciones no son necesarias 

            if (fichasJugadores[jugadorActual].Count == 0)//esto seria equivalente a ver si al jugador actual le quedan 0 fichas
            {
                jugadorGanador = jugadorActual;
                IGanador.ADevolver(jugadorGanador, ref valorFichasPorJugador);
                return (jugadorGanador, valorFichasPorJugador);
            }
            else if (definenGanador.ContainsKey(ParametrosDefinenGanador.TurnosSinJugar))//else
            {
                int max = int.MinValue;
                for (int i = 0; i < cantidadJugadores; i++)//this.cantJugadores
                {
                    if (valorFichasPorJugador[i] > max)
                    {
                        max = valorFichasPorJugador[i];
                        jugadorGanador = i;
                    }
                }

                bool empate = false;
                for (int i = 0; i < cantidadJugadores; i++)
                {
                    if (valorFichasPorJugador[i] == max)
                    {
                        if (empate == true)
                            return (-1, valorFichasPorJugador);
                        else
                            empate = true;
                    }
                }
            }
        }
        return (jugadorGanador, valorFichasPorJugador);//Ojo buscar los tipos de excepciones
    }
}