namespace SGE.Aplicacion;
public class CasoDeUsoUsuarioConsultaTodos(IRepositorioUsuario repo,IdUsuarioValidador idUsuarioValidador)
{
    public List<Usuario> Ejecutar(int id)
    {
        // Valida el id de Usuario
        if (!idUsuarioValidador.Validar(id, out string mensajeErrorId))
        {
            throw new ValidacionException(mensajeErrorId);
            // Si la validación falla, se lanza una excepción de ValidacionException con el mensaje de error correspondiente
        }
        return repo.UsuarioConsultaTodos();
        // Retorna la lista de expedientes obtenida al llamar al método ExpedienteConsultaTodos del repositorio de expedientes
    }
}