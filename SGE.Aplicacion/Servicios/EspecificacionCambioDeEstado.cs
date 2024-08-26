namespace SGE.Aplicacion;
public class EspecificacionCambioDeEstado : IEspecificacionCambioDeEstado
{
    public Expediente ActualizarEstado(Expediente expediente)
    {
        Tramite? ultimoTramite = expediente.Tramites.Last();
        if (ultimoTramite != null)
        {
            expediente.Estado = ultimoTramite.Etiqueta switch
            {
                EtiquetaTramite.Resolucion => EstadoExpediente.ConResolucion,
                EtiquetaTramite.PaseAEstudio => EstadoExpediente.ParaResolver,
                EtiquetaTramite.PaseAlArchivo => EstadoExpediente.Finalizado,
                _ => expediente.Estado // Mantener el estado actual si la etiqueta no coincide con ninguna de las opciones anteriores
            };
        }
        expediente.FechaUltimaModificacion = DateTime.Now;
        return expediente;
    }
}

/*

*/