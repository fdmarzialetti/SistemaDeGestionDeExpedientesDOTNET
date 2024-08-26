namespace SGE.Repositorios;

using System.Collections.Generic;
using SGE.Aplicacion;
public class RepositorioUsuarioSQLite : IRepositorioUsuario
{
    private readonly SGEContext db;

    public RepositorioUsuarioSQLite(SGEContext db)=> this.db = db;
    public void UsuarioAlta(Usuario usuario)
    {
        db.Add(usuario); // se agregará realmente con el db.SaveChanges()
        db.SaveChanges(); //actualiza la base de datos.
    }

    public void UsuarioBaja(int id)
    {
        var usuarioBaja = db.Usuarios.Where(u => u.Id == id).SingleOrDefault();
        if (usuarioBaja != null)
        {
            db.Remove(usuarioBaja); //se borra realmente con el db.SaveChanges()
        }else{
            throw new RepositorioException("No Existe el Usuario con id "+id);
        }
        db.SaveChanges(); //actualiza la base de datos.
    }

    public Usuario? UsuarioConsultaPorId(int id)
    {
        var usuario = db.Usuarios.Where(u => u.Id == id).SingleOrDefault();
        if(usuario == null){
            throw new RepositorioException("No Existe el Usuario con id "+id);
        }
        return usuario;
    }

    public List<Usuario> UsuarioConsultaTodos()
    {
        return db.Usuarios.ToList();
    }

    public void UsuarioModificacion(Usuario usuario)
    {
        var usuarioModificar = db.Usuarios.Where( u => u.Id == usuario.Id).SingleOrDefault();
        if (usuarioModificar != null)
        {
            usuarioModificar.Nombre = usuario.Nombre;
            usuarioModificar.Apellido = usuario.Apellido;
            usuarioModificar.CorreoElectronico = usuario.CorreoElectronico;
            usuarioModificar.Permisos = usuario.Permisos;
        }else{
            throw new RepositorioException("No Existe el Usuario con id "+usuario.Id);
        }
    }
}