namespace Logica.domino.dll;

public interface IGanador
{
    (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos, ICalculaPuntos calcularPuntosGanoJugador, bool equipo, List<int> puntosPorJugador,List<Jugador> jugadores);
}

public class Ganador_Clasico : IGanador
{
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos, ICalculaPuntos calcularPuntosGanoJugador, bool equipo,List<int> puntosPorJugador,List<Jugador> jugadores)
    {
         int jugadorGanador = -1;
        List<int> valorFichasPorJugador = new List<int>();
        if (definenGanador.ContainsKey(ParametrosDefinenGanador.FichasJugadores) && definenGanador.ContainsKey(ParametrosDefinenGanador.IndexJugadorActual))
        {
            List<List<Ficha>> fichasJugadores = (List<List<Ficha>>)definenGanador[ParametrosDefinenGanador.FichasJugadores];
            int jugadorActual = (int)definenGanador[ParametrosDefinenGanador.IndexJugadorActual];

            valorFichasPorJugador = contarPuntos.ContarPuntos(fichasJugadores);
            for (int i = 0; i < cantidadJugadores; i++)//this.cantJugadores
            {
                valorFichasPorJugador[i] += puntosPorJugador[i];
            }
            //estas condiciones no son necesarias 
            if (fichasJugadores[jugadorActual].Count == 0)//esto seria equivalente a ver si al jugador actual le quedan 0 fichas
            {
                jugadorGanador = jugadores[jugadorActual].ID;
                valorFichasPorJugador[jugadorGanador] = calcularPuntosGanoJugador.CalcularPuntosGanoJugador(jugadorGanador, valorFichasPorJugador, equipo);
            }
            else if (definenGanador.ContainsKey(ParametrosDefinenGanador.TurnosSinJugar))//else
            {
                int min = int.MaxValue;
                for (int i = 0; i < cantidadJugadores; i++)//this.cantJugadores
                {
                    if (valorFichasPorJugador[i] < min)
                    {
                        min = valorFichasPorJugador[i];
                        jugadorGanador = jugadores[i].ID;
                    }
                }
                bool empate = false;
                for (int i = 0; i < cantidadJugadores; i++)
                {
                    if (valorFichasPorJugador[i] == min)
                    {
                        if (empate == true)
                            return (-1, new int[cantidadJugadores].ToList());
                        else
                            empate = true;
                    }
                }
                valorFichasPorJugador[jugadorGanador] = calcularPuntosGanoJugador.CalcularPuntosGanoJugador(jugadorGanador, valorFichasPorJugador, equipo);
            }


            return (jugadorGanador, valorFichasPorJugador);
        }

        return (jugadorGanador, valorFichasPorJugador);//Ojo buscar los tipos de excepciones

    //     int jugadorGanador = -1;
    //     List<int> valorFichasPorJugador = new List<int>();
    //     if (definenGanador.ContainsKey(ParametrosDefinenGanador.FichasJugadores) && definenGanador.ContainsKey(ParametrosDefinenGanador.IndexJugadorActual))
    //     {
    //         List<List<Ficha>> fichasJugadores = (List<List<Ficha>>)definenGanador[ParametrosDefinenGanador.FichasJugadores];
    //         int jugadorActual = (int)definenGanador[ParametrosDefinenGanador.IndexJugadorActual];

    //         valorFichasPorJugador = contarPuntos.ContarPuntos(fichasJugadores);
    //         for (int i = 0; i < cantidadJugadores; i++)//this.cantJugadores
    //         {
    //             valorFichasPorJugador[i] += puntosPorJugador[i];
    //         }

    //         //estas condiciones no son necesarias 


    //         if (fichasJugadores[jugadorActual].Count == 0)//esto seria equivalente a ver si al jugador actual le quedan 0 fichas
    //         {
    //             jugadorGanador = jugadores[jugadorActual].ID;
    //             // IGanador.ADevolver(jugadorGanador, ref valorFichasPorJugador);
    //             valorFichasPorJugador[jugadorGanador] = calcularPuntosGanoJugador.CalcularPuntosGanoJugador(jugadorGanador, valorFichasPorJugador, equipo);
    //         }
    //         else if (definenGanador.ContainsKey(ParametrosDefinenGanador.TurnosSinJugar))//else
    //         {
    //             int min = int.MaxValue;
    //             for (int i = 0; i < cantidadJugadores; i++)//this.cantJugadores
    //             {
    //                 if (valorFichasPorJugador[i] < min)
    //                 {
    //                     min = valorFichasPorJugador[i];
    //                     jugadorGanador = jugadores[i].ID;
    //                 }
    //             }
    //             bool empate = false;
    //             for (int i = 0; i < cantidadJugadores; i++)
    //             {
    //                 if (valorFichasPorJugador[i] == min)
    //                 {
    //                     if (empate == true)
    //                         return (-1, new int[cantidadJugadores].ToList());
    //                     else
    //                         empate = true;
    //                 }
    //             }

    //             // IGanador.ADevolver(jugadorGanador, ref valorFichasPorJugador);
    //             valorFichasPorJugador[jugadorGanador] = calcularPuntosGanoJugador.CalcularPuntosGanoJugador(jugadorGanador, valorFichasPorJugador, equipo);

    //         }


    //         return (jugadorGanador, valorFichasPorJugador);
    //     }

    //     return (jugadorGanador, valorFichasPorJugador);//Ojo buscar los tipos de excepciones
    }
}
public class Ganador_Quincena : IGanador
{
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos, ICalculaPuntos calcularPuntosGanoJugador, bool equipo, List<int> puntosPorJugador,List<Jugador> jugadores)
    {
        (int, List<int>) resultAux = new Ganador_Clasico().Ganador(definenGanador, cantidadJugadores, contarPuntos, calcularPuntosGanoJugador, equipo, puntosPorJugador,jugadores);
        List<int> aux = resultAux.Item2;
         
        if (resultAux.Item1 == -1) return (-1, new int[cantidadJugadores].ToList());
        for (int i = 0; i < resultAux.Item2.Count; i++)
        {
            if(i == resultAux.Item1) continue;
            else aux[i] = puntosPorJugador[i];
        }
        resultAux.Item2 = aux;
        if (definenGanador.ContainsKey(ParametrosDefinenGanador.FichasJugadores) && definenGanador.ContainsKey(ParametrosDefinenGanador.IndexJugadorActual))
        {
            resultAux.Item2[resultAux.Item1] += 5;
            return (resultAux.Item1, resultAux.Item2);
        }

        resultAux.Item2[resultAux.Item1] += 10;
        return (resultAux.Item1, resultAux.Item2);

    }
}
//Gana el que más puntos tiene
public class Ganador_Inverso : IGanador
{
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos, ICalculaPuntos calcularPuntosGanoJugador, bool equipo, List<int> puntosPorJugador,List<Jugador> jugadores)
    {
        int jugadorGanador = -1;
        List<int> valorFichasPorJugador = new List<int>();
        if (definenGanador.ContainsKey(ParametrosDefinenGanador.FichasJugadores) && definenGanador.ContainsKey(ParametrosDefinenGanador.IndexJugadorActual))
        {
            List<List<Ficha>> fichasJugadores = (List<List<Ficha>>)definenGanador[ParametrosDefinenGanador.FichasJugadores];
            int jugadorActual = (int)definenGanador[ParametrosDefinenGanador.IndexJugadorActual];

            valorFichasPorJugador = contarPuntos.ContarPuntos(fichasJugadores);
            for (int i = 0; i < cantidadJugadores; i++)//this.cantJugadores
            {
                valorFichasPorJugador[i] += puntosPorJugador[i];
            }
            //estas condiciones no son necesarias 

            if (fichasJugadores[jugadorActual].Count == 0)//esto seria equivalente a ver si al jugador actual le quedan 0 fichas
            {
                jugadorGanador = jugadores[jugadorActual].ID;

                valorFichasPorJugador[jugadorGanador] = calcularPuntosGanoJugador.CalcularPuntosGanoJugador(jugadorGanador, valorFichasPorJugador, equipo);
                // IGanador.ADevolver(jugadorGanador, ref valorFichasPorJugador);
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
                        jugadorGanador = jugadores[i].ID;

                    }
                }

                bool empate = false;
                for (int i = 0; i < cantidadJugadores; i++)
                {
                    if (valorFichasPorJugador[i] == max)
                    {
                        if (empate == true)
                            return (-1, new int[cantidadJugadores].ToList());
                        else
                            empate = true;
                    }
                }
            }
        }
        valorFichasPorJugador[jugadorGanador] = calcularPuntosGanoJugador.CalcularPuntosGanoJugador(jugadorGanador, valorFichasPorJugador, equipo);
        // IGanador.ADevolver(jugadorGanador, ref valorFichasPorJugador);
        return (jugadorGanador, valorFichasPorJugador);//Ojo buscar los tipos de excepciones
    }

}
public class Ganador_SiEmpataAcumulaSusPuntos : IGanador
{
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos, ICalculaPuntos calcularPuntosGanoJugador, bool equipo, List<int> puntosPorJugador,List<Jugador> jugadores)
    {
        (int, List<int>) resultAux = new Ganador_Clasico().Ganador(definenGanador, cantidadJugadores, contarPuntos, calcularPuntosGanoJugador, equipo, puntosPorJugador,jugadores);

        if (resultAux.Item1 == -1) return (-1, resultAux.Item2);
        return (resultAux.Item1, resultAux.Item2);
    }
}