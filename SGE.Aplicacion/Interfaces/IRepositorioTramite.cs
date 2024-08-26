namespace SGE.Aplicacion;
public interface IRepositorioTramite
{
    void TramiteAlta(Tramite tramite);
    void TramiteBaja(int id);
    void TramiteModificacion(Tramite tramite);
    Tramite? TramiteConsultaPorId(int id);
    List<Tramite> TramiteConsultaTodos();
    void tramiteBajaTodos(int idExpediente);

    List<Tramite> TramiteConsultaPorExpediente(Expediente expediente);
    public List<Tramite> TramiteConsultaPorEtiqueta(EtiquetaTramite etiqueta);
}