namespace SGE.Aplicacion;

public class CasoDeUsoTramiteConsultaPorEtiqueta(IRepositorioTramite repo)
{
    public List<Tramite> Ejecutar(EtiquetaTramite etiquetaTramite)
    {
        return repo.TramiteConsultaPorEtiqueta(etiquetaTramite);
        // Retorna la lista de trámites obtenida al llamar al método TramiteConsultaPorEtiqueta del repositorio de trámites, pasando la etiqueta de trámite como parámetro
    }
}