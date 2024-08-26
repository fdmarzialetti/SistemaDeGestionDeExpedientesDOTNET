namespace SGE.Aplicacion;
public interface IRepositorioExpediente
{
    void ExpedienteAlta(Expediente expediente);
    void ExpedienteBaja(int id);
    void ExpedienteModificacion(Expediente expediente);
    Expediente? ExpedienteConsultaPorId(int id);
    List<Expediente> ExpedienteConsultaTodos();
}