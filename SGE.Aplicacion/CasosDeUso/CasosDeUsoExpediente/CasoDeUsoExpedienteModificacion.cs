namespace SGE.Aplicacion;
public class CasoDeUsoExpedienteModificacion(IRepositorioExpediente repo, ServicioAutorizacion servicioAutorizacion, ExpedienteValidador expedienteValidador)
{
    public void Ejecutar(Expediente expediente, int usuarioId)
    {
        // Valida el permiso
        if (!servicioAutorizacion.PoseeElPermiso(usuarioId,Permiso.ExpedienteModificacion))
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
        expediente.FechaUltimaModificacion = DateTime.Now;
        expediente.UsuarioUltimaModificacionId = usuarioId;
        // Se establece la fecha de la última modificación en el expediente como la fecha y hora actuales
        // y el ID de usuario de la última modificación en el expediente

        repo.ExpedienteModificacion(expediente);
        // Se llama al método ExpedienteModificacion del repositorio para modificar el expediente
    }
}