namespace SGE.Aplicacion;

public class AutorizacionException : Exception {
    public AutorizacionException(string mensajeError) : base(mensajeError) { }
}