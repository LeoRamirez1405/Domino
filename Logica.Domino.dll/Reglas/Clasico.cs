namespace Logica.domino.dll;
public class ClasicoIndividual : ClaseComunReglas, IReglas
{
    
    IRepartir repartir = new Repartir_Clasico();
    IContarPuntos ContarPuntos => new ContarPuntos_Clasico();
    IFinalizarJugada finalizarPartida = new FinalizarJugada_Clasico();
    IGanador ganador = new Ganador_Clasico(); 
    IProximoJugador proximoJugador = new ProximoJugador_Clasico();
    IValidarJugada validarPartida = new ValidarJugada_Clasica();
    public bool equipo => base.equipo;
    // bool IReglas.invertido { get => base.invertido; set => base.invertido; }
    public bool invertido => base.invertido;
    public ICalculaPuntos calculaPuntos => new CalcularPuntosGanoJugador_Clasico();
    public ClasicoIndividual(int cantJugadores, int cantFichas, int valorMaxFichas,bool equipo) : base(cantFichas,cantJugadores,valorMaxFichas,equipo){}//aqui se deberia incializar de con valores
    public int CantidadJugadores => base.cantJugadoresEnJuego;
    public (int, int) DimensionTablero => (5,8);//5 filas 8 columnas
    public void AccionDespuesDeLaJugada(int jugadorActual, bool huboJugada, ParteFicha izquierda, ParteFicha derecha, ref List<int> puntosPorJugador, ref List<Jugador> jugadores, ref bool invertido)
    {
        return;
    }
    public int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {
        return calculaPuntos.CalcularPuntosGanoJugador(jugadorGanador,puntosPorJugador,equipo);   
    }
    public int CantFichasPorJugador()
    {
        return cantFichasPorJugador;
    }
    public int CantFichasTotalJuego()
    {
        return CantFichasPorJugador() * CantidadJugadores;
    }
    public bool FinalizoPartida(int cantFichasJugadorActual, int turnosSinJugar, List<int> puntosPorJugador)
    {
        return finalizarPartida.FinalizoPartida(cantFichasJugadorActual , turnosSinJugar , puntosPorJugador);
    }
    public (int, List<int>) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador, int cantidadJugadores, IContarPuntos contarPuntos, ICalculaPuntos calculaPuntos,bool equipo, List<int> puntosPorJugador,List<Jugador> jugadores)
    {
        return ganador.Ganador(definenGanador, cantidadJugadores, contarPuntos,calculaPuntos,equipo,puntosPorJugador,jugadores);
    }
    public int ProximoJugador(int jugadorActual, int totalJugadores,bool invertido)
    {
        return proximoJugador.ProximoJugador(jugadorActual, totalJugadores, invertido);    
    }
    public List<Ficha[]> Repartir(List<Ficha> todasFichas, int CantidadJugadores, int cantFichasPorJugador)
    {
        return repartir.Repartir(todasFichas,CantidadJugadores,base.cantFichasPorJugador);
    }
    public bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano)
    {
        return validarPartida.ValidarJugada(fichaMesa, fichaMano);
    }

    public IContarPuntos contarPuntos => ContarPuntos;



    /*public List<Ficha[]> Repartir(List<Ficha> todasFichas)//la lista de fichas que se recibe como paarametro son todas las fichas del juego
    {
        //aqui se modifica la coleccion de fichas general para que ell arbitro solo se quede con las fichas sobrantes luego de repartir
        List<Ficha[]> result = new List<Ficha[]>();
        System.Random r = new Random();
        for(int i = 0; i < CantidadJugadores; i++)//cantJugadores
        {
            Ficha[] resultemporal = new Ficha[base.cantFichasPorJugador];//cantFichas
            for(int j = 0; j < resultemporal.Length; j++)//cantFichas
            {
                int pos = r.Next(0,todasFichas.Count);
                resultemporal[j] = todasFichas[pos];
                todasFichas.RemoveAt(pos);
            }
            result.Add(resultemporal);
        }
        return result;
    }

    public virtual bool FinalizoPartida(int cantFichasJugadorActual, int turnosSinJugar)
    {
        if(cantFichasJugadorActual == 0) return true;
        if(turnosSinJugar == CantidadJugadores) return true;
        return false;
    }

    virtual public (int, int) Ganador(Dictionary<ParametrosDefinenGanador, object> definenGanador)
    {
        int jugadorGanador = -1;
        List<int> valorFichasPorJugador = new List<int>();
        if(definenGanador.ContainsKey(ParametrosDefinenGanador.FichasJugadores) && definenGanador.ContainsKey(ParametrosDefinenGanador.IndexJugadorActual))
        {
            List<List<Ficha>> fichasJugadores = (List<List<Ficha>>)definenGanador[ParametrosDefinenGanador.FichasJugadores];
            int jugadorActual = (int)definenGanador[ParametrosDefinenGanador.IndexJugadorActual];

            foreach(var x in fichasJugadores)
            {
                int puntos = 0;
                foreach(var y in x)
                    puntos += y.Valor;
                valorFichasPorJugador.Add(puntos);
            }

            //estas condiciones no son necesarias 

            if(fichasJugadores[jugadorActual].Count == 0)//esto seria equivalente a ver si al jugador actual le quedan 0 fichas
            {
                jugadorGanador = jugadorActual;
            }
            else if(definenGanador.ContainsKey(ParametrosDefinenGanador.TurnosSinJugar))//else
            {
                int min = int.MaxValue;
                for(int i = 0; i < CantidadJugadores; i++)//this.cantJugadores
                {
                    if(valorFichasPorJugador[i] < min)
                    {
                        min = valorFichasPorJugador[i];
                        jugadorGanador = i;
                    }
                }
            }
        }

        return (jugadorGanador, CalcularPuntosGanoJugador(jugadorGanador, valorFichasPorJugador));//Ojo buscar los tipos de excepciones
    }

    int CalcularPuntosGanoJugador(int jugadorGanador, List<int> puntosPorJugador)
    {
        int totalPuntos = 0;
        for(int i = 0; i < puntosPorJugador.Count; i++)
        {
            if(i == jugadorGanador) continue;
            totalPuntos += puntosPorJugador[i];
        }
        return totalPuntos;
    }

    public int ProximoJugador(int jugadorActual)
    {
        if(jugadorActual == CantidadJugadores - 1) return 0;//cantJugadores
        return jugadorActual + 1;
    }

    //Este metodo solo funciona se se juega en elmodo clasico con las fichas clasicas, si trato de jugar con otro tipo de ficha da error ent.. esto se soluciona redefinido el equals de PrteFicha
    //luego, la jugada de parte ficha sera valida si parteFicha fichaMano.Equals(fichaMes) == true
    public bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano)
    {
        return fichaMano == fichaMesa;//se redefinio el ==
    }

    public int CantFichasPorJugador()
    {
        return base.cantFichasPorJugador;//
    }

    public int CantFichasTotalJuego()
    {
        return CantFichasPorJugador() * CantidadJugadores;//
    }

    public virtual void AccionDespuesDeLaJugada(int jugadorActual, bool hubojugada, ParteFicha izquierda, ParteFicha Derecha)
    {
        return;
    }*/


}