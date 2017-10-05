using AgenteApp.DataAccess;
using AgenteApp.Models;
using System.Threading.Tasks;

namespace AgenteApp.Presenters
{
    public class UsuarioServicioDatos
    {
        public UsuarioServicioDatos()
        {
        }

        public async Task<Usuario> Encontrar(string nombreUsuario, string contraseña)
        {
            ServicioDatos<Usuario> ServicioDatos = new ServicioDatos<Usuario>(Constantes.DIRECCION_BASE, "/api/users/find.php");

            ServicioDatos.agregarParametros("username", nombreUsuario);
            ServicioDatos.agregarParametros("password", contraseña);
            Usuario usuario = await ServicioDatos.EjecutarPOST();
            return usuario;

        }
    }
}