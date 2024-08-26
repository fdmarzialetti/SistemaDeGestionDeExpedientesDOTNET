namespace SGE.Aplicacion;
public class CasoDeUsoUsuarioConsultaPorId(IRepositorioUsuario repo, IdUsuarioValidador idUsuarioValidador)
{
    public Usuario? Ejecutar(int id)
    {
        // Valida el id de Usuario
        if (!idUsuarioValidador.Validar(id, out string mensajeErrorId))
        {
            throw new ValidacionException(mensajeErrorId);
            // Si la validación falla, se lanza una excepción de ValidacionException con el mensaje de error correspondiente
        }
        return repo.UsuarioConsultaPorId(id);
        // Retorna el usuario encontrado (o null si no se encontró ninguno)
    }
}