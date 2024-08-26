namespace SGE.Repositorios;
using System.Collections.Generic;
using SGE.Aplicacion;
public class RepositorioExpedienteSQLite : IRepositorioExpediente
{
    private readonly SGEContext db;

    public RepositorioExpedienteSQLite(SGEContext db)=> this.db = db;
        
    public void ExpedienteAlta(Expediente expediente)
    {
        db.Add(expediente); // se agregará realmente con el db.SaveChanges()
        db.SaveChanges(); //actualiza la base de datos.
    }

    public void ExpedienteBaja(int id)
    {   
        var expedienteBorrar = db.Expedientes.Where(e => e.Id == id).SingleOrDefault();
        if (expedienteBorrar != null)
        {
            db.Remove(expedienteBorrar); //se borra realmente con el db.SaveChanges()
        }else{
            throw new RepositorioException("No Existe el Expediente con id "+id);
        }
        db.SaveChanges(); //actualiza la base de datos.
    }

    public Expediente? ExpedienteConsultaPorId(int id)
    {
        var expediente = db.Expedientes.Where(e => e.Id == id).SingleOrDefault();
        if(expediente==null){
            throw new RepositorioException("No Existe el Expediente con id "+id);
        }
        return expediente;
    }

    public List<Expediente> ExpedienteConsultaTodos()
    {
        return db.Expedientes.ToList();
    }

    public void ExpedienteModificacion(Expediente expediente)
    {
        var expedienteModificar = db.Expedientes.Where( t => t.Id == expediente.Id).SingleOrDefault();
        if (expedienteModificar != null)
        {
            expedienteModificar.Caratula = expediente.Caratula;
            expedienteModificar.FechaUltimaModificacion = expediente.FechaUltimaModificacion;
            expedienteModificar.UsuarioUltimaModificacionId = expediente.UsuarioUltimaModificacionId;
            expedienteModificar.Estado = expediente.Estado;
        }else{
            throw new RepositorioException("No Existe el Expediente con id "+expediente.Id);
        }
        db.SaveChanges(); //actualiza la base de datos.
    }
}