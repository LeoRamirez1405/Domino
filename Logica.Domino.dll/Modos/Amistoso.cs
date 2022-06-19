﻿namespace Logica.domino.dll;
public class Amistoso : IModo
{
    // List<IJugar> jugadores;
    Arbitro arbitro;
    int[] PuntosJugadores;

    public Amistoso(int noJug)
    {
        this.arbitro = new Arbitro(noJug);
        // this.jugadores = this.arbitro.GetJugadores();
        this.PuntosJugadores = new int[arbitro.GetJugadores().Count];
    }
    public (int,int) Gana()
    {
        return arbitro.Jugando();
    }
}
