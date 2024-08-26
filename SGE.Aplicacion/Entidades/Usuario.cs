using System.Text;

namespace SGE.Aplicacion;
public class Usuario
{
    //Propiedades
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string CorreoElectronico { get; set; }
    public string Contrasena { get; set; }
    public List<Permiso> Permisos { get; set; } = new List<Permiso>();

    //Constructores
    public Usuario(List<Permiso> permisos) => Permisos = permisos;
    public Usuario() { }

    //Metodos
    public override string ToString()
    {
        StringBuilder stringPermisos = new StringBuilder();
        foreach (var permiso in Permisos)
        {
            stringPermisos.Append("\n" + permiso.ToString());
        }
        return $"Id: {Id}\n" +
        $"Nombre: {Nombre}\n" +
        $"Apellido: {Apellido}\n" +
        $"Correo Electrónico: {CorreoElectronico}\n" +
        $"Permisos: {stringPermisos}\n";
    }

}