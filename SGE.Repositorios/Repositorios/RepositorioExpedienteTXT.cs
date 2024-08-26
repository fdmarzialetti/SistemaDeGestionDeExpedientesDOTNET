namespace SGE.Repositorios;
using System.Linq;
using SGE.Aplicacion;
public class RepositorioExpedienteTXT : IRepositorioExpediente
{
    readonly string _nombreArchivoExpediente = "Expedientes.txt";
    readonly string _nombreArchivoUltimoIdExpediente = "UltimoIdExpediente.txt";

    private void GuardarExpediente(Expediente expediente, StreamWriter swArchivoExpediente){
    // Se escribe cada propiedad del expediente en el archivo
        swArchivoExpediente.WriteLine(expediente.Id);
        swArchivoExpediente.WriteLine(expediente.Caratula);
        swArchivoExpediente.WriteLine(expediente.FechaCreacion);
        swArchivoExpediente.WriteLine(expediente.FechaUltimaModificacion);
        swArchivoExpediente.WriteLine(expediente.UsuarioUltimaModificacionId);
        swArchivoExpediente.WriteLine(expediente.Estado);
    }
    public void ExpedienteAlta(Expediente expediente)
    // Se obtiene el último ID asignado a un expediente, se le asigna el siguiente al nuevo expediente
    // Se sobreescribe el último ID con el siguiente
    // Se almacena el expediente en el archivo de texto
        { 
        using var srArchivoUltimoIdExpediente = new StreamReader(_nombreArchivoUltimoIdExpediente,true);
        int ultimoId = int.Parse(srArchivoUltimoIdExpediente.ReadLine()??"0");
        ultimoId++;
        expediente.Id = ultimoId;
        srArchivoUltimoIdExpediente.Close();
        using var swArchivoUltimoIdExpediente = new StreamWriter(_nombreArchivoUltimoIdExpediente,false);
        swArchivoUltimoIdExpediente.WriteLine(ultimoId);
        swArchivoUltimoIdExpediente.Close();
        using var swArchivoExpediente = new StreamWriter(_nombreArchivoExpediente, true);
        GuardarExpediente(expediente,swArchivoExpediente);
    }
    public List<Expediente> ExpedienteConsultaTodos()
    // Se lee el archivo de expedientes y se crea una lista con los expedientes encontrados
    // Se retorna la lista de expedientes
    {
        var listaExpedientes = new List<Expediente>();
        using var sr = new StreamReader(_nombreArchivoExpediente,false);
        while (!sr.EndOfStream)
        {
            var expediente = new Expediente();
            expediente.Id = int.Parse(sr.ReadLine() ?? "");
            expediente.Caratula = sr.ReadLine() ?? "";
            expediente.FechaCreacion = DateTime.Parse(sr.ReadLine() ?? "");
            expediente.FechaUltimaModificacion = DateTime.Parse(sr.ReadLine()??"");
            expediente.UsuarioUltimaModificacionId=int.Parse(sr.ReadLine()??"");
            expediente.Estado = (EstadoExpediente)Enum.Parse(typeof(EstadoExpediente), sr.ReadLine()??"");
            listaExpedientes.Add(expediente);
        }
        return listaExpedientes;
    }

    public void ExpedienteBaja(int idEliminar)
    // Se verifica si el expediente a eliminar existe, de lo contrario se lanza una excepción
    // Se obtiene la lista de todos los expedientes
    // Se remueve el expediente a eliminar de la lista
    // Se actualiza el archivo de expedientes con la lista modificada
    {   
        if(ExpedienteConsultaPorId(idEliminar)==null)
        {
            throw new RepositorioException("No Existe el Expediente con id "+idEliminar);
        }
        var listaExpedientes = ExpedienteConsultaTodos();
        listaExpedientes.RemoveAll(e => e.Id == idEliminar);
        Console.WriteLine("Elementos en la lista de expedientes: "+listaExpedientes.Count);
        
        using var swArchivoExpediente = new StreamWriter(_nombreArchivoExpediente, false);
        foreach(Expediente expediente in listaExpedientes){
            GuardarExpediente(expediente,swArchivoExpediente);
        }
        
    }
    public Expediente? ExpedienteConsultaPorId(int idBuscado)
    // Se busca el expediente por su ID, si no se encuentra se lanza una excepción
    // Se retorna el expediente encontrado
    {
        var expediente = ExpedienteConsultaTodos().FirstOrDefault(exp => exp.Id == idBuscado);
        if(expediente==null)
        {
            throw new RepositorioException("No Existe el Expediente con id "+idBuscado);
        }
        return expediente;
    } 

    public void ExpedienteModificacion(Expediente expedienteModificado)
    // Se verifica si el expediente a modificar existe, de lo contrario se lanza una excepción
    // Se obtiene la lista de todos los expedientes
    // Se encuentra el índice del expediente a modificar en la lista
    // Se reemplaza el expediente antiguo con el modificado en la lista
    // Se actualiza el archivo de expedientes con la lista modificada
    {
        if(ExpedienteConsultaPorId(expedienteModificado.Id)==null)
        {
            throw new RepositorioException("No Existe el Expediente con id "+expedienteModificado.Id);
        }
        var listaExpedientes = ExpedienteConsultaTodos();
        int indiceExpediente = listaExpedientes.FindIndex(expediente => expediente.Id == expedienteModificado.Id);
        listaExpedientes[indiceExpediente]=expedienteModificado;
        using var swArchivoExpediente = new StreamWriter(_nombreArchivoExpediente, false);
        foreach(Expediente expediente in listaExpedientes){
            GuardarExpediente(expediente,swArchivoExpediente);
        }
    }

}