namespace SGE.Aplicacion;
public class CasoDeUsoUsuarioAlta(IRepositorioUsuario repo)
{
    public void Ejecutar(Usuario usuario)
    {   
        repo.UsuarioAlta(usuario);
        // Se llama al método UsuarioAlta del repositorio para dar de alta el usuario.
    }
}