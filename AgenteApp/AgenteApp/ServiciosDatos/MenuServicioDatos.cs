using AgenteApp.Clases;
using AgenteApp.DataAccess;
using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgenteApp.ServiciosDatos
{
    public class MenuServicioDatos
    {
        public async Task<List<OpcionMenu>> Consultar()
        {
            ServicioDatos<List<OpcionMenu>> servicioDatos = new ServicioDatos<List<OpcionMenu>>(Constantes.DIRECCION_BASE, "/api/menu/consultar.php");
            List<OpcionMenu> opcionesMenu = await servicioDatos.Ejecutar();
            return opcionesMenu;
        }
    }
}
