namespace SGE.Aplicacion;
public class CasoDeUsoUsuarioBaja(IRepositorioUsuario repo, IdUsuarioValidador idUsuarioValidador)
{
    public void Ejecutar(int id)
    {
        // Valida el id de Usuario
        if (!idUsuarioValidador.Validar(id, out string mensajeErrorId))
        {
            throw new ValidacionException(mensajeErrorId);
            // Si la validación falla, se lanza una excepción de ValidacionException con el mensaje de error correspondiente
        }
        repo.UsuarioBaja(id);
        // Se llama al método UsuarioBaja del repositorio de Usuarios para dar de baja el usuario con el ID proporcionado
    }
}