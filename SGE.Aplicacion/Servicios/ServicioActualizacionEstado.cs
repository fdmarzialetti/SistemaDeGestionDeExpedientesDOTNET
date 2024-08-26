namespace SGE.Aplicacion;

public class ServicioActualizacionEstado(IEspecificacionCambioDeEstado especificacionCombioDeEstado)
{
    public void ActualizarEstado(Expediente expediente, IRepositorioExpediente repo)
    {
        expediente=especificacionCombioDeEstado.ActualizarEstado(expediente); 
        repo.ExpedienteModificacion(expediente);
    }
}

