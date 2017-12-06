using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    class Configuración
    {
        string versión, secuencia, descripción, campo, asistente, tipo;
        string tabla, campoid, campos, valores;
        public string Versión { get { return versión; } set { versión = value; } }
        public string Secuencia { get { return secuencia; } set { secuencia = value; } }
        public string Descripcion { get { return descripción; } set { descripción = value; } }
        public string Campo { get { return campo; } set { campo = value; } }
        public string Asistente { get { return asistente; } set { asistente = value; } }
        public string Tipo { get { return tipo; } set { tipo = value; } }
        public string Tabla { get { return tabla; } set { tabla = value; } }
        public string Campoid { get { return campoid; } set { campoid = value; } }
        public string Campos { get { return campos; } set { campos = value; } }
        public string Valores { get { return valores; } set { valores = value; } }
    }
}
