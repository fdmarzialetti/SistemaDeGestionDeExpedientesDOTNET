namespace SGE.Aplicacion;
public class IdUsuarioValidador
{
    public bool Validar(int IdUsuario, out string mensajeError)
    {
        mensajeError = "";
        if (IdUsuario<=0)
        {
            mensajeError = "El id de usuario debe ser mayor que 0.\n";
        }
        return (mensajeError == "");
    }
}