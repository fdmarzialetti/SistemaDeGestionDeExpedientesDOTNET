namespace SGE.Aplicacion;
public class CasoDeUsoTramiteAlta(IRepositorioTramite repoTramite, IRepositorioExpediente repoExpediente, ServicioAutorizacion servicioAutorizacion, TramiteValidador tramiteValidador, ServicioActualizacionEstado servicioActualizacionEstado )
{
    public void Ejecutar(Tramite tramite, Expediente expediente, int usuarioId)
    {   
        // Valida el permiso
        if (!servicioAutorizacion.PoseeElPermiso(usuarioId,Permiso.TramiteAlta))
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
        
        tramite.ExpedienteId = expediente.Id;
        tramite.UsuarioUltimaModificacionId = usuarioId;
        // Se establece el ID del expediente en el tramite
        // y el ID de usuario de la última modificación en el tramite
        
        repoTramite.TramiteAlta(tramite);
        // Se llama al método TramiteAlta del repositorio para dar de alta el tramite
        
        expediente.Tramites = repoTramite.TramiteConsultaPorExpediente(expediente);
        // Se actualiza la lista de trámites del expediente consultando los trámites asociados a ese expediente
        
        servicioActualizacionEstado.ActualizarEstado(expediente, repoExpediente);
        // Se instancia y llama al método ActualizarEstado del ServicioActualizacionEstado con la especificación de cambio de estado y se pasa el expediente y el repositorio de expedientes como parámetros
    }
}