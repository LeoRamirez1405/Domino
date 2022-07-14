namespace Logica.domino.dll;
public class EHumano: IEstrategias
{
    //public (Ficha, int) Jugar(ParteFicha izquierda, ParteFicha derecha, IReglas reglas)
    public (Ficha, int) Jugar(ref List<Ficha> Mano,ParteFicha izquierda, ParteFicha derecha, IReglas reglas,int jugadorActual)
    {
        Arbitro.imprimirMano(Mano);
        System.Console.WriteLine("Cual desea jugar? (comienza por el 0) ({0 - izquierda} {1 - derecha}) (-1 si no lleva)");
        string respuesta = Console.ReadLine();
        (Ficha, int) result = (Mano[0], -1);

        if (respuesta == "-1")
        {
            Console.Clear();
            return result;
        }
        else
        {
            try
            {
                string[] trabajo = respuesta.Split(" ");
                (int, int) sol = (int.Parse(trabajo[0]), int.Parse(trabajo[1]));
                if (sol.Item2 == 0)
                {
                    if (reglas.ValidarJugada(izquierda, Mano[sol.Item1].Arriba) || reglas.ValidarJugada(izquierda, Mano[sol.Item1].Abajo))
                    {
                        Ficha f = Mano[sol.Item1];
                        Mano.RemoveAt(sol.Item1);
                        Console.Clear();
                        return (f, sol.Item2);
                    }
                }
                else if (sol.Item2 == 1)
                {
                    if (reglas.ValidarJugada(derecha, Mano[sol.Item1].Arriba) || reglas.ValidarJugada(derecha, Mano[sol.Item1].Abajo))
                    {
                        Ficha f = Mano[sol.Item1];
                        Mano.RemoveAt(sol.Item1);
                        Console.Clear();
                        return (f, sol.Item2);
                    }
                }
                else
                {
                    Console.Clear();
                    return result;
                }
            }
            catch
            {
                Console.Clear();
                return result;
            }
        }
        Console.Clear();
        return result;
    }
}