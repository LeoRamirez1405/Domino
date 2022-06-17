﻿namespace Logica.domino.dll;
//using System.Diagnostics;
public class Arbitro : GuiaJuego //no es estatico para poder variar las reglas y las estrategias de los jugadores
{
    EstadoJuego estadoJuego;
    IReglas reglas;
    IDomino domino;
    int cantJugadores;//la cant de jugadores debe estar en las reglas
    List<IJugar> jugadores;
    ParteFicha[,] tablero;

    public Arbitro(IReglas reglas,IDomino domino)//IReglas reglas
    {
        this.estadoJuego = EstadoJuego.Null;
        this.reglas = reglas;
        this.domino = domino;
        IniciaJugadores();
    }

    public bool CrearJuego(IReglas reglas, List<IJugar> estrategiasJugadores)
    {
        // if(this.estadoJuego != EstadoJuego.Null)
        //     return false;

        this.reglas = reglas;
        this.jugadores = estrategiasJugadores;
        tablero = new ParteFicha[reglas.DimensionTablero.Item1,this.reglas.DimensionTablero.Item2];//el metodo retorna una tupla (contrarto jj)
        return true;
    }
// public int MyProperty { get; private set; }
    public bool ComenzarJuego()
    {
        if(this.estadoJuego != EstadoJuego.ListoParaComenzar)
            return false;

        //este estado es en el que no se han puesto fichas
        Jugando();
        return true;
    }

    public (int,int) Jugando()
    {
        (int,int) result = (-1,-1);

        ParteFicha parteFichaDerecha = null;
        // (int,int) posParteFichaderecha = (-1,-1);

        ParteFicha parteFichaIzquierda = null;
        // (int,int) posParteFichaIzquierda = (-1,-1);

        int turnosSinJugar = 0;

        estadoJuego = EstadoJuego.EnCurso;
        (int,int)[] pos = new (int,int)[2];//por el monento solo se puede jugar x dos lados pues las fichas son las tradicionales

        //Stopwatch clock = new Stopwatch();
        int jugadorActual = reglas.JugadorInicial();
        //Hay que tener algo que sea una abstraccion de tablero que me diga las fichas disponibles por donde se puede jugar

        Ficha fichaIncial = jugadores[reglas.JugadorInicial()].Jugar(this.reglas);
        parteFichaDerecha = fichaIncial.Abajo;
        parteFichaIzquierda = fichaIncial.Arriba;
        pos = PonerFichaTablero(fichaIncial);
            

        do
        {
            int proximoJugador = this.reglas.ProximoJugador(jugadorActual);
            //aqui solol deberia pasar las dos fichas por donde se puede jugar y tendria que devolver la ficha que va a jugar y por donde la va ajugar
            //aqui no es necesario pasarle las reglas al los jugadores, pues las estrategias dben poder funcionar (No necesariamente se eficientes) con cualquier juego o regla
            (Ficha, int) jugada = jugadores[reglas.JugadorInicial()].Jugar(parteFichaIzquierda,parteFichaDerecha,reglas);
            if(jugada.Item2 == 1) 
            {
                //1 es la parte derecha
                parteFichaDerecha = jugada.Item1.Abajo.Equals(parteFichaDerecha) ? jugada.Item1.Arriba : jugada.Item1.Abajo;
                turnosSinJugar = 0;
                pos[1] = PonerFichaTablero(pos[1], jugada.Item1);
            }
            else if(jugada.Item2 == 0)
            {
                //0 es la parte izquierda
                parteFichaIzquierda = jugada.Item1.Abajo.Equals(parteFichaDerecha) ? jugada.Item1.Arriba : jugada.Item1.Abajo;
                turnosSinJugar = 0;
            }
            else if(jugada.Item2 == -1)
            {
                turnosSinJugar += 1;
            }

            if(this.reglas.FinalizoPartida(jugadorActual,turnosSinJugar))
            {
                Dictionary<ParametrosDefinenGanador,object> argumentos = new Dictionary<ParametrosDefinenGanador, object>();
                argumentos.Add(ParametrosDefinenGanador.TurnosSinJugar,turnosSinJugar);

                argumentos.Add(ParametrosDefinenGanador.IndexJugadorActual, jugadorActual);

                // List<List<Ficha<T>>> fichasJugadores = new List<List<Fichal<T>>>();
                List<List<Ficha>> fichasJugadores = new List<List<Ficha>>();
                foreach(var x in jugadores)
                    fichasJugadores.Add(x.ObtenerFichas());
                argumentos.Add(ParametrosDefinenGanador.FichasJugadores, fichasJugadores);

                result = this.reglas.Ganador(argumentos);
                estadoJuego = EstadoJuego.Null;
            }

            System.Threading.Thread.Sleep(2000);//espera 2 segundos para hacer la proxima jugada
        }
        while(this.estadoJuego == EstadoJuego.EnCurso);

        return result;
    }

    
    (int,int)[] PonerFichaTablero(Ficha ficha)
    {
        int x = this.tablero.GetLength(0) / 2;
        int y = this.tablero.GetLength(1) / 2;

        tablero[x, y] = ficha.Arriba;
        if(y + 1 < tablero.GetLength(1))
            tablero[x, y + 1] = ficha.Abajo;
        return new (int,int)[]{(x, y), (x, y + 1)};
    }

    (int,int) PonerFichaTablero((int,int) pos, Ficha ficha)
    {
        return PonerFichaTablero(pos, ficha, 0, (-1,-1));
    }

    (int,int) PonerFichaTablero((int,int) pos, Ficha ficha, int index, (int, int) newPos)
    {
        if(index >= 2)
            return newPos;

        int x = pos.Item1, y = pos.Item2;

        if(PosValida(y - 1, tablero.GetLength(1)) && tablero[x, y - 1] == null)
        {
            PonerParteFicha(x,y-1,ficha);
            newPos = (x, y -1);
            return PonerFichaTablero((x, y - 1), ficha, index + 1, pos);
        }
        if(PosValida(y + 1, tablero.GetLength(1)) && tablero[x, y + 1] == null)
        {
            newPos = (x, y + 1);
            PonerParteFicha(x, y + 1, ficha);
            return PonerFichaTablero((x, y + 1), ficha, index + 1, newPos);
        }
        if(PosValida(x - 1, tablero.GetLength(0)) && !HayFichaEnFila(x - 1) && tablero[x - 1, y] == null)
        {
            newPos = (x - 1, y);
            PonerParteFicha(x - 1, y, ficha);
            return PonerFichaTablero((x - 1, y), ficha, index + 1, newPos);
        }
        if(PosValida(x + 1, tablero.GetLength(0)) && !HayFichaEnFila(x + 1) && tablero[x + 1, y] == null)
        {
            newPos = (x + 1, y);
            PonerParteFicha(x + 1, y, ficha);
            return PonerFichaTablero((x + 1, y), ficha, index + 1, newPos);
        }
        
        throw new System.Exception();
    }

    void PonerParteFicha(int x, int y, Ficha ficha)
    {
        if(tablero[x, y] == ficha.Abajo) 
                tablero[x, y] = ficha.Abajo;
            else
                tablero[x, y] = ficha.Arriba;
    }

    bool HayFichaEnFila(int fila)
    {
        if(!PosValida(fila, tablero.GetLength(0))) return true;

        for(int i = 0; i < tablero.GetLength(1); i ++)
        {
            if(tablero[fila,i] != null)
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
 public void IniciaJugadores()
    {
        List<IJugar> ListaJugadores = new List<IJugar>();
        int cantJugadores = reglas.CantidadJugadores;
    
        System.Console.WriteLine("1. Aleatorio");
        System.Console.WriteLine("2. Botagorda");
        System.Console.WriteLine("3. Leo");
        System.Console.WriteLine("4. Matemático");
       //Aqui se empieza
       List<Ficha[]> fichasJugadores = reglas.Repartir(domino.fichas(reglas.CantFichas()));
        for (int i = 0; i < cantJugadores; i++)
        {
            System.Console.WriteLine($"Escoja la estrategia del jugador {i}");
            int jug = int.Parse(Console.ReadLine());
            while(true)
            {
                if(jug == 1) {ListaJugadores.Add(new Aleatorio(fichasJugadores[i].ToList())); break;}
                else if(jug == 2) {ListaJugadores.Add(new Botagorda(fichasJugadores[i].ToList())); break;}
                else if(jug == 3) {ListaJugadores.Add(new Leo(fichasJugadores[i].ToList())); break;}
                else if(jug == 4) {ListaJugadores.Add(new Matematico(fichasJugadores[i].ToList())); break;}
                else
                {
                    System.Console.WriteLine("Dato incorrecto. Vuelva a intentarlo: ");
                    jug = int.Parse(Console.ReadLine());
                }
            }
        }

        jugadores = ListaJugadores;
        System.Console.Clear();
    }
    public List<IJugar> GetJugadores() => jugadores;
    
}

