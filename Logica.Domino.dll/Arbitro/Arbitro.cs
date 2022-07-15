namespace Logica.domino.dll;
public class Arbitro
{
    int cantJugadores;//la cant de jugadores debe estar en las reglas
    List<Jugador> jugadores;
    Ficha[,] tablero; // ParteFicha[,] tablero;
    List<int> puntosPorJugadores;
    bool equipo;
    Mesa mesa = new Mesa();

    public void ImprimirMesa()
    {
        mesa.imprimirMesa();
    }
    IReglas reglas;
    int jugadorActual;
    int turnosSinJugar;

    ParteFicha izquierdaParteFicha = null;
    ParteFicha derechaParteFicha = null;

    (int,int)[] posFichasAJugar;
    public Arbitro(int cantJug, bool equipo, IReglas reglas, List<Jugador> jugadores)//IReglas reglas
    {
        this.equipo = equipo;
        this.puntosPorJugadores = new int[cantJug].ToList();
        this.cantJugadores = cantJug;
        this.reglas = reglas;
        this.jugadores = jugadores;
        this.tablero = new Ficha[4,8];
        // this.jugadorActual = reglas.JugadorInicial();
        this.posFichasAJugar = new (int, int)[2];
        this.turnosSinJugar = 0;
    }

    public List<Jugador> GetJugadores() => jugadores;

    public Ficha[,] GetTablero => this.tablero;

    public List<Ficha> GetFichasJugador()
    {
        return jugadores[jugadorActual].fichas;
    }

    public int JugadorActual => this.jugadorActual;
    
    (int,int)[] PonerFichaTablero(Ficha ficha)
    {
        int x = this.tablero.GetLength(0) / 2;
        int y = this.tablero.GetLength(1) / 2;

        tablero[x,y] = ficha;
        return new (int,int)[]{(x, y), (x, y)};
    }

    ((int, int), bool) PonerFichaTablero((int,int) pos, Ficha jugada, int sg)//Ficha ficha, bool dir dir dice si es a la derecha o ala izquierda
    {
        int x = pos.Item1, y = pos.Item2;
        if(PosValida(y + sg, tablero.GetLength(1)) && tablero[x, y + sg] is not null)
        {
            if(PosValida(y - sg, tablero.GetLength(1)))
            {
                tablero[x , y - sg] = jugada;
                return ((x, y - sg), true);
            }
            else if (PosValida(x - 1, tablero.GetLength(0)) && !HayFichaEnFila(x - 1))
            {
                tablero[x -1, y] = jugada;
                return ((x -1, y), true);
            }
            else if (PosValida(x + 1, tablero.GetLength(0)) && !HayFichaEnFila(x + 1))
            {
                tablero[x + 1, y] = jugada;
                return ((x + 1, y), true);
            }
        }
        return ((-1,-1), false);
    }

    bool HayFichaEnFila(int fila)
    {
        if(!PosValida(fila, tablero.GetLength(0))) return true;

        for(int i = 0; i < tablero.GetLength(1); i ++)
        {
            if(tablero[fila,i] is not null)
                return true;
        }
        return false;
    }
    bool PosValida(int x, int length)
    {
        if(x < 0 || x >= tablero.GetLength(0))
            return false;

        return true;
    }

    void AgrandarTablero()
    {
        Ficha[,] newTablero = new Ficha[tablero.GetLength(0) + cantJugadores, tablero.GetLength(1)];//crece en dependencia de la cant d jugadoores
        for(int i = 0, pos = cantJugadores / 2 + 5; i < tablero.GetLength(0); i++)
        {
            for(int j = 0; j < tablero.GetLength(1); j++)
            {
                newTablero[pos, j] = tablero[i,j];
            }
        }
        tablero = newTablero;
    }

    public Ficha Jugar(bool esLaPrimeraJugada)
    {
            (Ficha,int) jugada;
        bool huboJugada = true;
        if(esLaPrimeraJugada)
        {
            jugadorActual = reglas.JugadorInicial();
            jugada.Item1 = ((IEstrategiasSalir)this.jugadores[jugadorActual]).Jugar(ref jugadores[jugadorActual].fichas,this.reglas);
            izquierdaParteFicha = jugada.Item1.Arriba;
            derechaParteFicha = jugada.Item1.Abajo;
            posFichasAJugar = PonerFichaTablero(jugada.Item1);
            mesa.AddDer(jugada.Item1,jugadorActual);
            return jugada.Item1;
        }
        else
        {
            jugadorActual = reglas.ProximoJugador(jugadorActual, cantJugadores,((ClaseComunReglas)reglas).invertido);
            jugada = ((IEstrategias)jugadores[jugadorActual]).Jugar(ref jugadores[jugadorActual].fichas, izquierdaParteFicha, derechaParteFicha, this.reglas, jugadorActual);

            if(jugada.Item2 == -1)
            {
                turnosSinJugar ++;
                huboJugada = false;
                jugada.Item1 = null;
            }
            else
            {
                int posParaJugar = jugada.Item2 == 0 ? 0 : 1;
                if(posParaJugar == 0)
                {
                    if( jugada.Item1.Abajo  != mesa.recorrido[0].Item1.Arriba )
                        jugada.Item1 = new Ficha(jugada.Item1.Abajo,jugada.Item1.Arriba);
                    mesa.AddIzq(jugada.Item1,jugadorActual);
                    izquierdaParteFicha = jugada.Item1.Arriba;
                }
                else if(posParaJugar == 1)
                {
                    if( mesa.recorrido[mesa.recorrido.Count-1].Item1.Abajo != jugada.Item1.Arriba )
                        jugada.Item1 = new Ficha(jugada.Item1.Abajo,jugada.Item1.Arriba);
                    mesa.AddDer(jugada.Item1,jugadorActual);
                    derechaParteFicha = jugada.Item1.Abajo;
                }
                // ((int, int), bool) jugadaPos = PonerFichaTablero(posFichasAJugar[posParaJugar], jugada.Item1, 1);
                // if(jugadaPos.Item2)
                //     posFichasAJugar[posParaJugar] = jugadaPos.Item1;
                // else 
                // {
                //     jugadaPos = PonerFichaTablero(posFichasAJugar[posParaJugar], jugada.Item1, -1);
                //     if(jugadaPos.Item2)
                //         posFichasAJugar[posParaJugar] = jugadaPos.Item1;
                //     else
                //     {
                //         AgrandarTablero();
                //         //Aqui faltan cosas..esto se soluciona si nunk hay q agrandar el tablero
                //     }
                // }
                turnosSinJugar = 0;
            }
        }
        
        reglas.AccionDespuesDeLaJugada(jugadorActual, huboJugada, izquierdaParteFicha, derechaParteFicha, ref puntosPorJugadores, ref jugadores, ref ((ClaseComunReglas)reglas).invertido);
        foreach (var item in jugadores)
        {
            if(item is IJuegaConMesa) ((IJuegaConMesa)item).juegaConMesa(mesa,mesa.fichaActual_jugador,huboJugada);
        }
        return jugada.Item1;
    }

    public bool TerminoPartida()
    {
        int cantFichasJugadorActual = this.jugadores[jugadorActual].fichas.Count;
        return this.reglas.FinalizoPartida(cantFichasJugadorActual, this.turnosSinJugar, this.puntosPorJugadores);
    }

    public (int, List<int>) GetGanador()
    {
        Dictionary<ParametrosDefinenGanador,object> argumentos = new Dictionary<ParametrosDefinenGanador, object>();
                
        argumentos.Add(ParametrosDefinenGanador.TurnosSinJugar,turnosSinJugar);
        argumentos.Add(ParametrosDefinenGanador.IndexJugadorActual, jugadorActual);

        List<List<Ficha>> fichasJugadores = new List<List<Ficha>>();
        foreach(var x in jugadores)
            fichasJugadores.Add(x.fichas);
        argumentos.Add(ParametrosDefinenGanador.FichasJugadores, fichasJugadores);

        if(turnosSinJugar == cantJugadores) 
            argumentos.Add(ParametrosDefinenGanador.SeTrancoElJuego, true);
        else
            argumentos.Add(ParametrosDefinenGanador.SeTrancoElJuego, false);

        return this.reglas.Ganador(argumentos, cantJugadores, reglas.contarPuntos, reglas.calculaPuntos, equipo,puntosPorJugadores,jugadores);
        //return this.reglas.Ganador(argumentos,cantJugadores,reglas.contarPuntos);    
    }
}

