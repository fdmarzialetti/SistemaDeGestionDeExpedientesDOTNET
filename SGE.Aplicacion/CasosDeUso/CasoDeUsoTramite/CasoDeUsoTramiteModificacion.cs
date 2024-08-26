namespace SGE.Aplicacion;
public class CasoDeUsoTramiteModificacion(IRepositorioTramite repoTramite, IRepositorioExpediente repoExpediente, ServicioAutorizacion servicioAutorizacion, TramiteValidador tramiteValidador, ServicioActualizacionEstado servicioActualizacionEstado)
{
    public void Ejecutar(Tramite tramite, int usuarioId)
    {
        // Valida el permiso
        if (!servicioAutorizacion.PoseeElPermiso(usuarioId,Permiso.TramiteModificacion))
        {
            throw new AutorizacionException("El usuario no posee autorizacion");
            // Si la validación falla, se lanza una excepción de AutorizacionException con el mensaje de error correspondiente
        }
        // Valida el Tramite
        if (!tramiteValidador.Validar(tramite, out string mensajeError))
        {
            throw new ValidacionException(mensajeError);
            // Si la validación falla, se lanza una excepción de ValidacionException con el mensaje de error correspondiente
        }
        
        tramite.FechaUltimaModificacion = DateTime.Now;
        
        tramite.UsuarioUltimaModificacionId = usuarioId;
        // Se establece la fecha de la última modificación en el tramite como la fecha y hora actuales
        // y el ID de usuario de la última modificación en el tramite
        
        repoTramite.TramiteModificacion(tramite);
        // Se llama al método TramiteModificacion del repositorio para modificar el tramite
        
        var expediente = repoExpediente.ExpedienteConsultaPorId(tramite.ExpedienteId);
        // Se consulta el expediente asociado al tramite
        
        if (expediente != null)
        {
            expediente.UsuarioUltimaModificacionId = usuarioId;
            // Se establece el ID de usuario de la última modificación en el expediente
            
            repoExpediente.ExpedienteModificacion(expediente);
            // Se llama al método ExpedienteModificacion del repositorio de expedientes para modificar el expediente
            
            expediente.Tramites = repoTramite.TramiteConsultaPorExpediente(expediente);
            // Se actualiza la lista de trámites del expediente consultando los trámites asociados a ese expediente
            
            servicioActualizacionEstado.ActualizarEstado(expediente, repoExpediente);
            // Se actualza el estado (si es necesario) por medio del metodo ActualizarEstado del ServicioActualizacionEstado.
        }
    }
}