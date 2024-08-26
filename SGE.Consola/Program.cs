using SGE.Aplicacion;
using SGE.Repositorios;

//Configuracion de dependencias

//DataBaseContext
SGEContext db = new SGEContext();
//Repositorios
IRepositorioExpediente repositorioExpediente = new RepositorioExpedienteSQLite(db);
IRepositorioTramite repositorioTramite = new RepositorioTramiteSQLite(db);
IRepositorioUsuario repositorioUsuario = new RepositorioUsuarioSQLite(db);
//Servicios
IEspecificacionCambioDeEstado especificacionCambioDeEstado = new EspecificacionCambioDeEstado();
ServicioActualizacionEstado servicioActualizacionEstado = new ServicioActualizacionEstado(especificacionCambioDeEstado);
ServicioAutorizacion servicioAutorizacion = new ServicioAutorizacion(repositorioUsuario);
//Validadores
IdUsuarioValidador idUsuarioValidador = new IdUsuarioValidador();
ExpedienteValidador expedienteValidador = new ExpedienteValidador();
TramiteValidador tramiteValidador = new TramiteValidador();
//CasoDeUsoExpediente
var expedienteAlta = new CasoDeUsoExpedienteAlta(repositorioExpediente, servicioAutorizacion, expedienteValidador);
var expedienteConsultaTodos = new CasoDeUsoExpedienteConsultaTodos(repositorioExpediente,repositorioTramite);
var expedienteBaja = new CasoDeUsoExpedienteBaja(repositorioExpediente, repositorioTramite, servicioAutorizacion);
var expedienteConsultaPorId = new CasoDeUsoExpedienteConsultaPorId(repositorioExpediente, repositorioTramite);
var expedienteModificacion = new CasoDeUsoExpedienteModificacion(repositorioExpediente, servicioAutorizacion, expedienteValidador);
//CasoDeUsoTramite
var tramiteAlta = new CasoDeUsoTramiteAlta(repositorioTramite, repositorioExpediente, servicioAutorizacion, tramiteValidador, servicioActualizacionEstado);
var tramiteBaja = new CasoDeUsoTramiteBaja(repositorioTramite, repositorioExpediente, servicioAutorizacion, servicioActualizacionEstado);
var tramiteConsultaPorId = new CasoDeUsoTramiteConsultaPorId(repositorioTramite);
var tramiteConsultaPorEtiqueta = new CasoDeUsoTramiteConsultaPorEtiqueta(repositorioTramite);
var tramiteModificacion = new CasoDeUsoTramiteModificacion(repositorioTramite, repositorioExpediente, servicioAutorizacion, tramiteValidador, servicioActualizacionEstado);
//CasoDeUsoUsuario
var usuarioAlta = new CasoDeUsoUsuarioAlta(repositorioUsuario);
var usuarioBaja = new CasoDeUsoUsuarioBaja(repositorioUsuario,idUsuarioValidador);
var usuarioConsultaPorId = new CasoDeUsoUsuarioConsultaPorId(repositorioUsuario,idUsuarioValidador);
var usuarioConsultaTodos = new CasoDeUsoUsuarioConsultaTodos(repositorioUsuario,idUsuarioValidador);
var usuarioModificacion = new CasoDeUsoUsuarioModificacion(repositorioUsuario,idUsuarioValidador);

SGESqlite.Inicializar();
Usuario? usuario = db.Usuarios.Find(1);
if (usuario != null)
{
    while (true)
    {
        // Mostrar menú
        Console.WriteLine("\n-- Menú --");
        Console.WriteLine("1. Alta de Expediente");
        Console.WriteLine("2. Consulta de Todos los Expedientes");
        Console.WriteLine("3. Baja de Expediente");
        Console.WriteLine("4. Consulta de Expediente por ID");
        Console.WriteLine("5. Modificación de Expediente");
        Console.WriteLine("6. Alta de Trámite");
        Console.WriteLine("7. Baja de Trámite");
        Console.WriteLine("8. Consulta de Trámite por Etiqueta");
        Console.WriteLine("9. Modificación de Trámite");
        Console.WriteLine("0. Salir");

        // Leer opción del usuario
        Console.Write("Ingrese el número de la opción que desea ejecutar: ");
        string? opcion = Console.ReadLine();

        // Ejecutar opción
        switch (opcion)
        {
            case "1":
                Console.Write("Ingrese la caratula del nuevo expediente: ");
                expedienteAlta.Ejecutar(new Expediente(Console.ReadLine()), usuario.Id);
                break;

            case "2":
                Console.WriteLine("\n");
                foreach (Expediente expediente in expedienteConsultaTodos.Ejecutar()) Console.WriteLine("\n" + expediente.ToString());
                break;

            case "3":
                Console.Write("Ingrese el id del expediente que quiere dar de baja: ");
                expedienteBaja.Ejecutar(int.Parse(Console.ReadLine() ?? "0"), usuario.Id);
                break;

            case "4":
                Console.Write("Ingrese el id del expediente que quiere consultar: ");
                Console.WriteLine(expedienteConsultaPorId.Ejecutar(int.Parse(Console.ReadLine() ?? "0")));
                break;
            case "5":
                Console.Write("Ingrese el id del expediente que quiere modificar: ");
                var expedienteAModificar = expedienteConsultaPorId.Ejecutar(int.Parse(Console.ReadLine() ?? "0"));
                if (expedienteAModificar != null)
                {
                    Console.Write("Ingrese una nueva caratula para el expediente: ");
                    expedienteAModificar.Caratula = Console.ReadLine();
                    expedienteModificacion.Ejecutar(expedienteAModificar, usuario.Id);
                }
                break;

            case "6":
                Console.Write("Ingrese el contenido del nuevo tramite: ");
                var contenido = Console.ReadLine();
                Console.Write("Ingrese el id del expediente al que se asignara el nuevo tramite: ");
                var expedienteAAsignar = expedienteConsultaPorId.Ejecutar(int.Parse(Console.ReadLine() ?? "0"));
                if (expedienteAAsignar != null)
                {
                    tramiteAlta.Ejecutar(new Tramite(contenido), expedienteAAsignar, usuario.Id);
                }
                break;
            case "7":
                Console.Write("Ingrese el id del tramite que quiere dar de baja: ");
                tramiteBaja.Ejecutar(int.Parse(Console.ReadLine() ?? "0"), 1);
                break;

            case "8":
                foreach (Tramite tramite in tramiteConsultaPorEtiqueta.Ejecutar(SeleccionDeEtiqueta()))
                {
                    Console.WriteLine(tramite.ToString());
                }
                break;
            case "9":
                Console.Write("Ingrese el id del tramite que quiere modificar: ");
                var tramitePorId = tramiteConsultaPorId.Ejecutar(int.Parse(Console.ReadLine() ?? "0"));
                Console.WriteLine("Seleecione una nueva etiqueta para el tramite: ");
                if (tramitePorId != null)
                {
                    tramitePorId.Etiqueta = SeleccionDeEtiqueta();
                    tramiteModificacion.Ejecutar(tramitePorId, usuario.Id);
                }
                break;
            case "0":
                Console.WriteLine("Saliendo...");
                return;
            default:
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                break;

        }
    }
}

EtiquetaTramite SeleccionDeEtiqueta()
{
    EtiquetaTramite etiqueta = EtiquetaTramite.EscritoPresentado;
    Console.WriteLine("\n-- Menú de Selección de Etiquetas --");
    Console.WriteLine("1. Escrito Presentado");
    Console.WriteLine("2. Pase a Estudio");
    Console.WriteLine("3. Despacho");
    Console.WriteLine("4. Resolución");
    Console.WriteLine("5. Notificación");
    Console.WriteLine("6. Pase al Archivo");
    Console.Write("Ingrese el número de la etiqueta que desea seleccionar: ");
    string opcion = Console.ReadLine() ?? "0";
    // Ejecutar opción
    switch (opcion)
    {
        case "1":
            etiqueta = EtiquetaTramite.EscritoPresentado;
            break;
        case "2":
            etiqueta = EtiquetaTramite.PaseAEstudio;
            break;
        case "3":
            etiqueta = EtiquetaTramite.Despacho;
            break;
        case "4":
            etiqueta = EtiquetaTramite.Resolucion;
            break;
        case "5":
            etiqueta = EtiquetaTramite.Notificacion;
            break;
        case "6":
            etiqueta = EtiquetaTramite.PaseAlArchivo;
            break;
        default:
            Console.WriteLine("Opción no válida. Intente de nuevo.");
            break;
    }
    return etiqueta;
}
