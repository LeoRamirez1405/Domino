namespace Logica.domino.dll;

public interface IValidarJugada
{
    bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano);

}

public class ValidarJugada_Clasica : IValidarJugada
{
    public bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano)
    {
        return fichaMano == fichaMesa;
    }
}

public class ValidarJugada_Menor: IValidarJugada
{
    //Cuando la ficha de la mesa es mayor que la de la mano
    public bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano)
    {
        return fichaMano.Valor <= fichaMesa.Valor;
    }
}

public class ValidarJugada_Mayor : IValidarJugada
{
    //Cuando la ficha de la mesa es menor que la de la mano
    public bool ValidarJugada(ParteFicha fichaMesa, ParteFicha fichaMano)
    {
        return fichaMano.Valor >= fichaMesa.Valor;
    }
}

