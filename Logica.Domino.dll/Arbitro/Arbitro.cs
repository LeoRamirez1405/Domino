namespace Logica.domino.dll;
//using System.Diagnostics;
public class Arbitro : GuiaJuego //no es estatico para poder variar las reglas y las estrategias de los jugadores
{
    EstadoJuego estadoJuego;
    IReglas reglas;
    IDomino domino;
    int cantJugadores;//la cant de jugadores debe estar en las reglas
    List<IJugar> jugadores;
    Ficha[,] tablero;

    public Arbitro(IReglas reglas,IDomino domino)//IReglas reglas
    {
        this.estadoJuego = EstadoJuego.Null;
        this.reglas = reglas;
        this.domino = domino;
    }

    public bool CrearJuego(IReglas reglas, List<IJugar> estrategiasJugadores)
    {
        // if(this.estadoJuego != EstadoJuego.Null)
        //     return false;

        this.reglas = reglas;
        this.jugadores = estrategiasJugadores;
        tablero = new Ficha[reglas.DimensionTablero.Item1,this.reglas.DimensionTablero.Item2];//el metodo retorna una tupla (contrarto jj)
        return true;
    }

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
        ParteFicha parteFichaIzquierda = null;
        int turnosSinJugar = 0;

        estadoJuego = EstadoJuego.EnCurso;
        (int,int)[] pos = new (int,int)[2];//por el monento solo se puede jugar x dos lados pues las fichas son las tradicionales

        //Stopwatch clock = new Stopwatch();
        int jugadorActual = 0;
        //Hay que tener algo que sea una abstraccion de tablero que me diga las fichas disponibles por donde se puede jugar

            Ficha fichaIncial = estrategiasJugadores[reglas.JugadorInicial()].Jugar(this.reglas);
            parteFichaDerecha = fichaIncial.Abajo;
            parteFichaIzquierda = fichaIncial.Arriba;

        do
        {
            int proximoJugador = this.reglas.ProximoJugador(jugadorActual);
            //aqui solol deberia pasar las dos fichas por donde se puede jugar y tendria que devolver la ficha que va a jugar y por donde la va ajugar
            //aqui no es necesario pasarle las reglas al los jugadores, pues las estrategias dben poder funcionar (No necesariamente se eficientes) con cualquier juego o regla
            (Ficha, int) jugada = estrategiasJugadores[reglas.JugadorInicial()].Jugar(parteFichaIzquierda,parteFichaDerecha,reglas);
            if(jugada.Item2 == 0) 
            {
                //0 es la parte derecha
                parteFichaDerecha = jugada.Item1.Abajo.Equals(parteFichaDerecha) ? jugada.Item1.Arriba : jugada.Item1.Abajo;
                turnosSinJugar = 0;
            }
            else if(jugada.Item2 == 1)
            {
                //1 es la parte izquierda
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

                List<List<Ficha>> fichasJugadores = new List<List<Fichal>>();
                foreach(var x in jugadores)
                    fichasJugadores.Add(x.Mano);
                argumentos.Add(ParametrosDefinenGanador.FichasJugadores, fichasJugadores);

                result = this.reglas.Ganador(argumentos);
                estadoJuego = EstadoJuego.Null;
            }

            System.Threading.Thread.Sleep(2000);//espera 2 segundos para hacer la proxima jugada
        }
        while(this.estadoJuego == EstadoJuego.EnCurso);
    }

}
