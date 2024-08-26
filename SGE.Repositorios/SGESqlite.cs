using SGE.Aplicacion;
namespace SGE.Repositorios;

public class SGESqlite
{
    public static void Inicializar()
    {
        using var context = new SGEContext();
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Se creó base de datos");
            // Agregar Usuarios al contexto.
            var usuario = new Usuario()
            {
                Nombre = "Fernando",
                Apellido = "Marzialetti",
                CorreoElectronico = "fdmarzialetti@gmail.com",
                Contrasena = "123456",
                Permisos = new List<Permiso>()
                    {
                        Permiso.ExpedienteAlta,
                        Permiso.ExpedienteBaja,
                        Permiso.ExpedienteModificacion,
                        Permiso.TramiteAlta,
                        Permiso.TramiteBaja,
                        Permiso.TramiteModificacion
                    }
            };
            context.Add(usuario);
            // Agregar Expedientes al contexto.
            context.Add(new Expediente()
            {
                Estado = EstadoExpediente.RecienIniciado,
                Caratula = "Expediente de Caso Judicial",
                FechaCreacion = DateTime.Now,
                FechaUltimaModificacion = DateTime.Now,
                UsuarioUltimaModificacionId = 1
            });
            // Agregar Tramites al contexto.
            context.Add(new Tramite()
            {
                ExpedienteId = 1,
                Etiqueta = EtiquetaTramite.EscritoPresentado,
                Contenido = "Presentación de Demanda",
                FechaCreacion = DateTime.Now,
                FechaUltimaModificacion = DateTime.Now,
                UsuarioUltimaModificacionId = 1
            });
            context.Add(new Tramite()
            {
                ExpedienteId = 1,
                Etiqueta = EtiquetaTramite.EscritoPresentado,
                Contenido = "Audiencia Preliminar",
                FechaCreacion = DateTime.Now,
                FechaUltimaModificacion = DateTime.Now,
                UsuarioUltimaModificacionId = 1
            });
            context.SaveChanges();
            Console.WriteLine("Se creó base de datos");
        }
    }
}