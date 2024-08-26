namespace SGE.Aplicacion;
public class CasoDeUsoExpedienteAlta(IRepositorioExpediente repo, ServicioAutorizacion servicioAutorizacion, ExpedienteValidador expedienteValidador)
{
    public void Ejecutar(Expediente expediente, int usuarioId)
    {   
        // Valida el permiso
        if (!servicioAutorizacion.PoseeElPermiso(usuarioId,Permiso.ExpedienteAlta))
        {
            throw new AutorizacionException("El usuario no posee autorizacion");
            // Si la validación falla, se lanza una excepción de AutorizacionException con el mensaje de error correspondiente
        }

        // Valida el Expediente
        if (!expedienteValidador.Validar(expediente, out string mensajeErrorExpediente))
        {
            throw new ValidacionException(mensajeErrorExpediente);
            // Si la validación falla, se lanza una excepción de ValidacionException con el mensaje de error correspondiente
        }

        expediente.UsuarioUltimaModificacionId = usuarioId;
        // Se establece el ID de usuario de la última modificación en el expediente

        repo.ExpedienteAlta(expediente);
        // Se llama al método ExpedienteAlta del repositorio para dar de alta el expediente
    }
}