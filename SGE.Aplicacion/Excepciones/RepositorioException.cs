namespace SGE.Aplicacion;

public class RepositorioException : Exception {
    public RepositorioException(string mensajeError) : base(mensajeError) { }
}