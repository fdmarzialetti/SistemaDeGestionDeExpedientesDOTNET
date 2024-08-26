namespace SGE.Aplicacion;

public class ServicioAutorizacionProvisoria : IServicioAutorizacion
{
    public bool PoseeElPermiso(int IdUsuario, Permiso permiso)
    {
        return IdUsuario == 1;
    }
}