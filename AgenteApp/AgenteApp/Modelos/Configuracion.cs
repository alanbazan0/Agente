using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    class Configuracion
    {
        string versión, descripción, campo, asistente, tipo, tabla, campoid, campos, valores;
        int secuencia;
        public string Versión { get { return versión; } set { versión = value; } }
        public int Secuencia { get { return secuencia; } set { secuencia = value; } }
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
