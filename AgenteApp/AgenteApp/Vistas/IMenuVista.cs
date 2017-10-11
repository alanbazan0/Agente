using AgenteApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Vistas
{
    public interface IMenuVista
    {
        void CrearMenu(List<OpcionMenu> opcionesMenu);
        void AbrirOpcionMenu(OpcionMenu opcionMenu);
    }
}
