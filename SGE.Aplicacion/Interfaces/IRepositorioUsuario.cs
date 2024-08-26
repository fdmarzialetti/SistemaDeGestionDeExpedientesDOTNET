namespace SGE.Aplicacion;
public interface IRepositorioUsuario
{
    void UsuarioAlta(Usuario usuario);
    void UsuarioBaja(int id);
    void UsuarioModificacion(Usuario usuario);
    Usuario? UsuarioConsultaPorId(int id);
    List<Usuario> UsuarioConsultaTodos();
}