using System.Text;

namespace SGE.Aplicacion;
public class Expediente
{
    //Propiedades
    public int Id { get;set; }
    public string? Caratula { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime FechaUltimaModificacion { get; set; } = DateTime.Now;
    public int UsuarioUltimaModificacionId { get; set; }
    public EstadoExpediente Estado { get; set; } = EstadoExpediente.RecienIniciado;
    public List<Tramite>Tramites {get;set;} = new List<Tramite>();
    
    //Constructores
    public Expediente(string? caratula) => Caratula=caratula;
    public Expediente(){}

    //Metodos
    public override string ToString()
    {
        StringBuilder stringTramites = new StringBuilder();
        foreach(Tramite tramite in Tramites){
            stringTramites.Append("\n"+tramite.ToString());
        }
        return $"Id: {Id}\n" +
            $"Caratula: {Caratula}\n" +
            $"FechaCreacion: {FechaCreacion}\n" +
            $"FechaUltimaModificacion: {FechaUltimaModificacion}\n" +
            $"UsuarioUltimaModificacionId: {UsuarioUltimaModificacionId}\n" +
            $"Estado: {Estado}\n"+
            $"Tramites: {stringTramites}\n";
    }

}