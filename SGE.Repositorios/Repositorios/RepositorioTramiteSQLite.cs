namespace SGE.Repositorios;
using System.Collections.Generic;
using SGE.Aplicacion;
public class RepositorioTramiteSQLite : IRepositorioTramite
{
    private readonly SGEContext db;
    public RepositorioTramiteSQLite(SGEContext db)=> this.db = db;
    public void TramiteAlta(Tramite tramite)
    {
        db.Add(tramite); // se agregará realmente con el db.SaveChanges()
        db.SaveChanges(); //actualiza la base de datos.
    }

    public void TramiteBaja(int id)
    {
        var tramiteBaja = db.Tramites.Where(t => t.Id == id).SingleOrDefault();
        if (tramiteBaja != null)
        {
            db.Remove(tramiteBaja); //se borra realmente con el db.SaveChanges()
        }else{
            throw new RepositorioException("No Existe el Tramite con id "+id);
        }
        db.SaveChanges(); //actualiza la base de datos.
    }

    public void tramiteBajaTodos(int idExpediente)
    {
        var tramitesBaja = db.Tramites.Where(t => t.ExpedienteId == idExpediente);
        foreach (Tramite tramite in tramitesBaja)
        {
            db.Remove(tramite);
        }
        db.SaveChanges(); //actualiza la base de datos.
    }

    public List<Tramite> TramiteConsultaPorEtiqueta(EtiquetaTramite etiqueta)
    {
        return db.Tramites.Where(t => t.Etiqueta == etiqueta).ToList();
    }

    public List<Tramite> TramiteConsultaPorExpediente(Expediente expediente)
    {
        return db.Tramites.Where(t => t.ExpedienteId == expediente.Id).ToList();
    }

    public Tramite? TramiteConsultaPorId(int id)
    {
        var tramite = db.Tramites.Where(t => t.Id == id).SingleOrDefault();
        if(tramite == null){
            throw new RepositorioException("No Existe el Tramite con id "+id);
        }
        return tramite;
    }

    public List<Tramite> TramiteConsultaTodos()
    {
        return db.Tramites.ToList();
    }

    public void TramiteModificacion(Tramite tramite)
    {
        var tramiteModificar = db.Tramites.Where( t => t.Id == tramite.Id).SingleOrDefault();
        if (tramiteModificar != null)
        {
            tramiteModificar.Etiqueta = tramite.Etiqueta;
            tramiteModificar.Contenido = tramite.Contenido;
            tramiteModificar.FechaUltimaModificacion = tramite.FechaUltimaModificacion;
            tramiteModificar.UsuarioUltimaModificacionId = tramite.UsuarioUltimaModificacionId;
        }else{
            throw new RepositorioException("No Existe el Tramite con id "+tramite.Id);
        }
        db.SaveChanges(); //actualiza la base de datos.
    }
}