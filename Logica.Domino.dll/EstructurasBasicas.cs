namespace Logica.domino.dll;
public class Ficha 
{
    ParteFicha arriba;
    ParteFicha abajo;
    public int Valor {get ;}
    string name;
    public Ficha(ParteFicha arriba,ParteFicha abajo)
    {
        this.abajo = abajo;
        this.arriba = arriba;
        this.Valor = this.abajo.Valor + this.arriba.Valor;
        name = this.arriba.Valor.ToString() + "_" + this.abajo.ToString();
    }

    public ParteFicha Arriba { get => arriba;}
    public ParteFicha Abajo { get => abajo;}
    public string Name { get => name;}

    public override string ToString()
    {
        string ar = arriba.parte.ToString(); 
        string ab = abajo.parte.ToString(); 
        return $"[{ar}|{ab}]";
    }
}

public class ParteFicha: IEquatable<ParteFicha>
{
    public object parte;
    public int Valor {get;}
    public ParteFicha(object parte,int valor)
    {
        this.parte = parte;
        this.Valor = valor;
    }

    public override string ToString()
    {
        try
        {
            return (string)parte;
        }
        catch
        {
            return "NO";
        }
    }

    public bool Equals(ParteFicha? parte) => this.Valor == parte?.Valor;
    public static bool operator == (ParteFicha a,ParteFicha b) {
   if ( a is null && b is not null) return false;
   if ( a is not null && b is null) return false;
   if ( a is null && b is null) return false;
     return a.Equals(b);
    } 
         
    public static bool operator != (ParteFicha a,ParteFicha b) => !a.Equals(b);
}

public enum ParametrosDefinenGanador
{
    TurnosSinJugar,
    IndexJugadorActual,
    FichasJugadores, // aqui se pasarian las fichas cada jugador en una lista d listas, luego con el index jugadore actual puedo ver las fichas del jugador actual
    SeTrancoElJuego,
}