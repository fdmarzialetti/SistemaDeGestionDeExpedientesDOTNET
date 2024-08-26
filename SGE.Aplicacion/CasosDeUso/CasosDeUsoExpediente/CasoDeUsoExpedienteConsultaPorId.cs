namespace SGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaPorId(IRepositorioExpediente repoExpediente, IRepositorioTramite repoTramite)
{
    public Expediente? Ejecutar(int id)
    {
        var expediente = repoExpediente.ExpedienteConsultaPorId(id);
        // Se llama al método ExpedienteConsultaPorId del repositorio de expedientes para obtener el expediente con el ID proporcionado

        if (expediente != null)
        // Verifica si se encontró un expediente con el ID proporcionado
        {
            expediente.Tramites = repoTramite.TramiteConsultaPorExpediente(expediente);
            // Si se encontró un expediente, se llama al método TramiteConsultaPorExpediente del repositorio de trámites para obtener los trámites asociados a ese expediente y se asigna al expediente encontrado
        }

        return expediente;
        // Retorna el expediente encontrado (o null si no se encontró ninguno)
    }
}