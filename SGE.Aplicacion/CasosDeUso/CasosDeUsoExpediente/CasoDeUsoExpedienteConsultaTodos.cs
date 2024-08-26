namespace SGE.Aplicacion;
public class CasoDeUsoExpedienteConsultaTodos(IRepositorioExpediente repo,IRepositorioTramite repoTramite)
{
    public List<Expediente> Ejecutar()
    {
        var listaExpedientes = repo.ExpedienteConsultaTodos();
        foreach(Expediente expediente in listaExpedientes){
            expediente.Tramites = repoTramite.TramiteConsultaPorExpediente(expediente);
        }
        return listaExpedientes;
        // Retorna la lista de expedientes obtenida al llamar al método ExpedienteConsultaTodos del repositorio de expedientes
    }
}