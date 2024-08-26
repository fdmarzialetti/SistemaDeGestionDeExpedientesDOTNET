namespace SGE.Aplicacion;

public class ValidacionException : Exception {
    public ValidacionException(string mensajeError) : base(mensajeError) { }
}