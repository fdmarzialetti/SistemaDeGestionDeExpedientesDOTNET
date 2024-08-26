namespace SGE.Repositorios;
using System.Linq;
using SGE.Aplicacion;
public class RepositorioTramiteTXT : IRepositorioTramite
{
    readonly string _nombreArchivoTramite = "Tramites.txt";
    readonly string _nombreArchivoUltimoIdTramite = "UltimoIdTramite.txt";

    private void GuardarTramite(Tramite tramite, StreamWriter swArchivoTramite)
    // Se escribe cada propiedad del tramite en el archivo
    {
        swArchivoTramite.WriteLine(tramite.Id);
        swArchivoTramite.WriteLine(tramite.ExpedienteId);
        swArchivoTramite.WriteLine(tramite.Etiqueta);
        swArchivoTramite.WriteLine(tramite.Contenido);
        swArchivoTramite.WriteLine(tramite.FechaCreacion);
        swArchivoTramite.WriteLine(tramite.FechaUltimaModificacion);
        swArchivoTramite.WriteLine(tramite.UsuarioUltimaModificacionId);
    }
    public void TramiteAlta(Tramite tramite)
    // Se obtiene el último ID asignado a un tramite, se le asigna el siguiente al nuevo tramite
    // Se sobreescribe el último ID con el siguiente
    // Se almacena el tramite en el archivo de texto
    {   
        using var srArchivoUltimoIdTramite = new StreamReader(_nombreArchivoUltimoIdTramite,true);
        int ultimoId = int.Parse(srArchivoUltimoIdTramite.ReadLine()??"0");
        ultimoId++;
        tramite.Id = ultimoId;
        srArchivoUltimoIdTramite.Close();
        using var swArchivoUltimoIdTramite= new StreamWriter(_nombreArchivoUltimoIdTramite,false);
        swArchivoUltimoIdTramite.WriteLine(ultimoId);
        swArchivoUltimoIdTramite.Close();
        using var swArchivoTramite = new StreamWriter(_nombreArchivoTramite, true);
        GuardarTramite(tramite,swArchivoTramite);
    }
    public List<Tramite> TramiteConsultaTodos()
    // Se lee el archivo de tramites y se crea una lista con los tramites encontrados
    // Se retorna la lista de tramites
    {
        var listaTramites = new List<Tramite>();
        using var sr = new StreamReader(_nombreArchivoTramite);
        while (!sr.EndOfStream)
        {
            var tramite = new Tramite();
            tramite.Id = int.Parse(sr.ReadLine() ?? "");
            tramite.ExpedienteId = int.Parse(sr.ReadLine() ?? "");
            tramite.Etiqueta = (EtiquetaTramite)Enum.Parse(typeof(EtiquetaTramite), sr.ReadLine()??"");
            tramite.Contenido = sr.ReadLine() ?? "";
            tramite.FechaCreacion = DateTime.Parse(sr.ReadLine() ?? "");
            tramite.FechaUltimaModificacion = DateTime.Parse(sr.ReadLine()??"");
            tramite.UsuarioUltimaModificacionId=int.Parse(sr.ReadLine()??"");
            listaTramites.Add(tramite);
        }
        return listaTramites;
    }

    public void TramiteBaja(int idEliminar)
    // Se verifica si el tramite a eliminar existe, de lo contrario se lanza una excepción
    // Se obtiene la lista de todos los tramites
    // Se remueve el tramite a eliminar de la lista
    // Se actualiza el archivo de tramites con la lista modificada
    {   
        if(TramiteConsultaPorId(idEliminar)==null)
        {
            throw new RepositorioException("No Existe el Tramite con id "+idEliminar);
        }
        var listaTramite = TramiteConsultaTodos();
        listaTramite.RemoveAll(t => t.Id == idEliminar);
        using var swArchivoTramite = new StreamWriter(_nombreArchivoTramite, false);
        foreach(Tramite tramite in listaTramite){
            GuardarTramite(tramite,swArchivoTramite);
        }
    }
    public Tramite? TramiteConsultaPorId(int idBuscado)
    // Se busca el tramite por su ID, si no se encuentra se lanza una excepción
    // Se retorna el tramite encontrado
    {
        var tramite = TramiteConsultaTodos().FirstOrDefault(exp => exp.Id == idBuscado);
        if(tramite==null)
        {
            throw new RepositorioException("No Existe el Tramite con id "+idBuscado);
        }
        return tramite;
    } 

    public void TramiteModificacion(Tramite tramiteModificado)
    // Se verifica si el tramite a modificar existe, de lo contrario se lanza una excepción
    // Se obtiene la lista de todos los tramites
    // Se encuentra el índice del tramite a modificar en la lista
    // Se reemplaza el tramite antiguo con el modificado en la lista
    // Se actualiza el archivo de tramites con la lista modificada
    {
        if(TramiteConsultaPorId(tramiteModificado.Id)==null)
        {
            throw new RepositorioException("No Existe el Tramite con id "+tramiteModificado.Id);
        }
        var listaTramites = TramiteConsultaTodos();
        int indiceTramite = listaTramites.FindIndex(tramite => tramite.Id == tramiteModificado.Id);
        listaTramites[indiceTramite]=tramiteModificado;
        using var swArchivoTramite = new StreamWriter(_nombreArchivoTramite, false);
        foreach(Tramite tramite in listaTramites){
            GuardarTramite(tramite,swArchivoTramite);
        }
    }

    public void tramiteBajaTodos(int idExpediente)
    // Se obtiene la lista de todos los tramites
    // Se remueven todos los tramites asociados al expediente de la lista
    // Se actualiza el archivo de tramites con la lista modificada
    {
        var listaTramite = TramiteConsultaTodos();
        listaTramite.RemoveAll(t => t.ExpedienteId == idExpediente);
        using var swArchivoTramite = new StreamWriter(_nombreArchivoTramite, false);
        foreach(Tramite tramite in listaTramite){
            GuardarTramite(tramite,swArchivoTramite);
        }
    }
    public List<Tramite> TramiteConsultaPorExpediente(Expediente expediente)=>TramiteConsultaTodos().FindAll(t=>t.ExpedienteId==expediente.Id).OrderByDescending(t => t.FechaUltimaModificacion).ToList();
    // Se filtran los tramites por el ID del expediente y se ordenan por fecha de última modificación
    // Se retorna la lista de tramites filtrada y ordenada
    public List<Tramite> TramiteConsultaPorEtiqueta(EtiquetaTramite etiqueta)=>TramiteConsultaTodos().FindAll(t=>t.Etiqueta==etiqueta);
    // Se filtran los tramites por la etiqueta
    // Se retorna la lista de tramites filtrada
}