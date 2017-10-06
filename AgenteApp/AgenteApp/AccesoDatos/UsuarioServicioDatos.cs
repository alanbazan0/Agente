using AgenteApp.Clases;
using AgenteApp.DataAccess;
using AgenteApp.Modelos;
using System.Threading.Tasks;

namespace AgenteApp.Presenters
{
    public class UsuarioServicioDatos
    {
        public UsuarioServicioDatos()
        {
        }

        public async Task<Usuario> Buscar(string nombreUsuario, string contrasena)
        {
            ServicioDatos<Usuario> servicioDatos = new ServicioDatos<Usuario>(Constantes.DIRECCION_BASE, "/api/usuarios/buscar.php");
            servicioDatos.AgregarParametro("nombreUsuario", nombreUsuario);
            servicioDatos.AgregarParametro("contrasena", contrasena);
            servicioDatos.Metodo = MetodoHTTP.POST;
            Usuario usuario = await servicioDatos.Ejecutar();
            return usuario;

        }
    }
}