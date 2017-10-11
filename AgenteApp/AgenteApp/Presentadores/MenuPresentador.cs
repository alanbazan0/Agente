using AgenteApp.Modelos;
using AgenteApp.ServiciosDatos;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Presentadores
{
    public class MenuPresentador
    {
        IMenuVista vista;
        public MenuPresentador(IMenuVista vista)
        {
            this.vista = vista;
        }

        public async void ConsultarOpcionesMenu()
        {
            MenuServicioDatos menuServicioDatos = new MenuServicioDatos();
            List<OpcionMenu> opcionesMenu = await menuServicioDatos.Consultar();
            vista.CrearMenu(opcionesMenu);
        }
    }
}
