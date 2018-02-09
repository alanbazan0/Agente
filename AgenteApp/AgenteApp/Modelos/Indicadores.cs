using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class Indicadores
    {

        String orden, titulo, canalId, presentacion,cantidad, color;

        public string Orden { get { return orden; } set { orden = value; } }
        public string Titulo { get { return titulo; } set { titulo = value; } }
        public string CanalId { get { return canalId; } set { canalId = value; } }
        public string Presentacion { get { return presentacion; } set { presentacion = value; } }
        public string Cantidad { get { return cantidad; } set { cantidad = value; } }
        public string Color { get { return color; } set { color = value; } }
    }
}
