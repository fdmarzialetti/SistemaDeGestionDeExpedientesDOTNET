namespace SGE.Aplicacion;
public class Tramite
{
    public int Id { get;set; }
    public int ExpedienteId { get; set; }
    public EtiquetaTramite Etiqueta { get; set; } = EtiquetaTramite.EscritoPresentado;
    public string? Contenido { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime FechaUltimaModificacion { get; set; } = DateTime.Now;
    public int UsuarioUltimaModificacionId { get; set; }

    //Constructores
    public Tramite(string? contenido) =>Contenido = contenido;

    public Tramite(){}
    
    //Metodos
    public override string ToString()
    {
        return $"Id: {Id}\n" +
            $"ExpedienteId: {ExpedienteId}\n" +
            $"Contenido: {Contenido}\n" +
            $"Etiqueta: {Etiqueta}\n"+
            $"FechaCreacion: {FechaCreacion}\n" +
            $"FechaUltimaModificacion: {FechaUltimaModificacion}\n" +
            $"UsuarioUltimaModificacionId: {UsuarioUltimaModificacionId}\n" ;          
    }

}