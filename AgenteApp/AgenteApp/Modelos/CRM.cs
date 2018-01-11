using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class CRM
    {
        String fecha, dia, hora, canal, temaPrincipal, tipificación,canalId,cantidad;

        public string Fecha { get { return fecha; } set { fecha = value; } }
        public string Dia { get { return dia; } set { dia = value; } }
        public string Hora { get { return hora; } set { hora = value; } }
        public string Canal { get { return canal; } set { canal = value; } }
        public string TemaPrincipal { get { return temaPrincipal; } set { temaPrincipal = value; } }
        public string Tipificación { get { return tipificación; } set { tipificación = value; } }
        public string CanalId { get { return canalId; } set { canalId = value; } }
        public string Cantidad { get { return cantidad; } set { cantidad = value; } }
    }
}
