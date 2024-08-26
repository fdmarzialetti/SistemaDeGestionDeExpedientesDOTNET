namespace SGE.Aplicacion;

public class ServicioAutorizacion : IServicioAutorizacion
{
    private IRepositorioUsuario repo;
    public ServicioAutorizacion(IRepositorioUsuario repo) => this.repo = repo;
    public bool PoseeElPermiso(int IdUsuario, Permiso permiso)
    {
        var usuario = repo.UsuarioConsultaPorId(IdUsuario);
        if (usuario != null)
        {
            return usuario.Permisos.Contains(permiso);
        }
        return false;
    }
}