namespace SGE.Aplicacion;
public class CasoDeUsoUsuarioModificacion(IRepositorioUsuario repo, IdUsuarioValidador idUsuarioValidador)
{
    public void Ejecutar(Usuario usuario, int usuarioId)
    {
        // Valida el id de Usuario
        if (!idUsuarioValidador.Validar(usuarioId, out string mensajeErrorId))
        {
            throw new ValidacionException(mensajeErrorId);
            // Si la validación falla, se lanza una excepción de ValidacionException con el mensaje de error correspondiente
        }
        repo.UsuarioModificacion(usuario);
        // Se llama al método UsuarioModificacion del repositorio para modificar el usuario.
    }
}