namespace Logica.domino.dll;

public class Matematico<T> : Jugador<T>, IJugar<T>
{
    public Matematico(List<Ficha<T>> mano) : base(mano)
    {
        Ordena(ref mano);
    }

    public Ficha<T> Jugar(IReglas<T> reglas)
    {
        Ficha<T> gorda = Mano[0];
        int suelto = 0;
        for (int i = 0; i < Mano.Count; i++)
        {
            if (Mano[i].Valor % 5 == 0)
            {
                gorda = Mano[i];
                suelto = i;
            }
        }
        Mano.RemoveAt(suelto);
        return (gorda);
    }
    public (Ficha<T>, int) Jugar(ParteFicha<T> izquierda, ParteFicha<T> derecha, IReglas<T> reglas)
    {
        Ficha<T> MejorFicha = Mano[0];
        int totalPuntos = 0;
        int posFicha = -1;
        int posFichaMano = 0;

        for (int i = 0; i < Mano.Count; i++)
        {
            if (reglas.ValidarJugada(izquierda, Mano[i].Arriba))
            {
                int actual = Calcula(izquierda, Mano[i].Arriba);
                if (Calcula(izquierda, Mano[i].Arriba) >= totalPuntos)
                {
                    MejorFicha = Mano[i];
                    totalPuntos = actual;
                    posFicha = 0;
                    posFichaMano = i;
                }
            }
            if (reglas.ValidarJugada(izquierda, Mano[i].Abajo))
            {
                int actual = Calcula(izquierda, Mano[i].Arriba);
                if (Calcula(izquierda, Mano[i].Arriba) >= totalPuntos)
                {
                    MejorFicha = Mano[i];
                    totalPuntos = actual;
                    posFicha = 0;
                    posFichaMano = i;
                }
            }
            if (reglas.ValidarJugada(derecha, Mano[i].Arriba))
            {
                int actual = Calcula(izquierda, Mano[i].Arriba);
                if (Calcula(izquierda, Mano[i].Arriba) >= totalPuntos)
                {
                    MejorFicha = Mano[i];
                    totalPuntos = actual;
                    posFicha = 1;
                    posFichaMano = i;
                }
            }
            if (reglas.ValidarJugada(derecha, Mano[i].Abajo))
            {
                int actual = Calcula(izquierda, Mano[i].Arriba);
                if (Calcula(izquierda, Mano[i].Arriba) >= totalPuntos)
                {
                    MejorFicha = Mano[i];
                    totalPuntos = actual;
                    posFicha = 1;
                    posFichaMano = i;
                }
            }

        }
        if (posFicha != -1)
        {
            Mano.RemoveAt(posFichaMano);
            return (MejorFicha, posFicha);
        }
        return (Mano[0],-1);
    }

    
    int Calcula(ParteFicha<T> fichaMano, ParteFicha<T> fichaTablero)
    {
        if (fichaMano.Valor + fichaTablero.Valor % 5 != 0)
            return fichaMano.Valor + fichaTablero.Valor;
        return 0;
    }
}
