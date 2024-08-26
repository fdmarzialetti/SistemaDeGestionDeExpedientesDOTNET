namespace SGE.Aplicacion;
public class ExpedienteValidador
{
    public bool Validar(Expediente expediente, out string mensajeError)
    {
        mensajeError = "";
        if (string.IsNullOrWhiteSpace(expediente.Caratula))
        {
            mensajeError = "La caratula del expediente no puede estar vacia.\n";
        }
        return (mensajeError == "");
    }
}