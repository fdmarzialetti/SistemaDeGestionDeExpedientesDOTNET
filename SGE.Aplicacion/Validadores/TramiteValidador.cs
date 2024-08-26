namespace SGE.Aplicacion;
public class TramiteValidador
{
    public bool Validar(Tramite tramite, out string mensajeError)
    {
        mensajeError = "";
        if (string.IsNullOrWhiteSpace(tramite.Contenido))
        {
            mensajeError = "El contenido del tramite no puede estar vacio.\n";
        }
        return (mensajeError == "");
    }
}