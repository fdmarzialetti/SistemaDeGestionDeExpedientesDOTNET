namespace SGE.Aplicacion;
public class CasoDeUsoTramiteBaja(IRepositorioTramite repoTramite, IRepositorioExpediente repoExpediente, ServicioAutorizacion servicioAutorizacion, ServicioActualizacionEstado servicioActualizacionEstado)
{
    public void Ejecutar(int tramiteId, int usuarioId)
    {
        // Valida el permiso
        if (!servicioAutorizacion.PoseeElPermiso(usuarioId,Permiso.TramiteBaja))
        {
            throw new AutorizacionException("El usuario no posee autorizacion");
            // Si la validación falla, se lanza una excepción de AutorizacionException con el mensaje de error correspondiente
        }
        
        var tramite = repoTramite.TramiteConsultaPorId(tramiteId);
        // Se consulta el tramite por su ID en el repositorio de trámites

        repoTramite.TramiteBaja(tramiteId);
        // Se llama al método TramiteBaja del repositorio de trámites para dar de baja el tramite con el ID proporcionado

        // Al eliminar un tramite, se actualiza la fecha de ultima modificacion y usuario del Expediente asociado.
        if (tramite != null)
        // Verifica si se encontró un tramite con el ID proporcionado
        {
            var expediente = repoExpediente.ExpedienteConsultaPorId(tramite.ExpedienteId);
            // Se consulta el expediente asociado al tramite

            if (expediente != null)
            {
                expediente.FechaUltimaModificacion = DateTime.Now;                
                expediente.UsuarioUltimaModificacionId = usuarioId;
                // Se establece la fecha de la última modificación del expediente como la fecha y hora actuales
                // y el ID de usuario de la última modificación del expediente
                
                repoExpediente.ExpedienteModificacion(expediente);
                // Se llama al método ExpedienteModificacion del repositorio de expedientes para modificar el expediente
                
                expediente.Tramites = repoTramite.TramiteConsultaPorExpediente(expediente);
                // Se actualiza la lista de trámites del expediente consultando los trámites asociados a ese expediente
                
                servicioActualizacionEstado.ActualizarEstado(expediente, repoExpediente);
                // Se actualza el estado (si es necesario) por medio del metodo ActualizarEstado del ServicioActualizacionEstado.
            }
        }
    }
}