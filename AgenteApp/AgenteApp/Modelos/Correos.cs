using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class Correos
    {
        string id, fecha, nombre, correo, asunto, contenido;
        string acorreo , titulo;
        public string Id { get { return id; } set { id = value; } }
        public string Fecha { get { return fecha; } set { fecha = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Correo { get { return correo; } set { correo = value; } }
        public string Asunto { get { return asunto; } set { asunto = value; } }
        public string Contenido { get { return contenido; } set { contenido = value; } }
        public string Acorreo { get { return acorreo; } set { acorreo = value; } }
        public string Titulo { get { return titulo; } set { titulo = value; } }
    }
}
