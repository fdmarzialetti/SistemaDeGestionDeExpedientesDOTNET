namespace SGE.Aplicacion;
public class CasoDeUsoTramiteConsultaPorId(IRepositorioTramite repoTramite)
{
    public Tramite? Ejecutar(int id)
    { 
        return repoTramite.TramiteConsultaPorId(id);
        // Retorna el tramite obtenido al llamar al método TramiteConsultaPorId del repositorio de trámites, pasando el ID del tramite como parámetro
    }
}