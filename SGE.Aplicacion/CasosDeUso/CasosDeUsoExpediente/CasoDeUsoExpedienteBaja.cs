namespace SGE.Aplicacion;
public class CasoDeUsoExpedienteBaja(IRepositorioExpediente repoExpediente, IRepositorioTramite repoTramite, ServicioAutorizacion servicioAutorizacion)
{
    public void Ejecutar(int expedienteId, int usuarioId)
    {
        // Valida el permiso
        if (!servicioAutorizacion.PoseeElPermiso(usuarioId,Permiso.ExpedienteBaja))
        {
            throw new AutorizacionException("El usuario no posee autorizacion");
            // Si la validación falla, se lanza una excepción de AutorizacionException con el mensaje de error correspondiente
        }
        
        repoExpediente.ExpedienteBaja(expedienteId);
        // Se llama al método ExpedienteBaja del repositorio de expedientes para dar de baja el expediente con el ID proporcionado
        
        repoTramite.tramiteBajaTodos(expedienteId);
        // Se llama al método tramiteBajaTodos del repositorio de trámites para dar de baja todos los trámites asociados al expediente con el ID proporcionado
    }
}