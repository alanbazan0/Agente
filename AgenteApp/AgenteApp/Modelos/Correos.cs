using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class Correos
    {
        string id, fecha, nombre, correo, asunto, contenido, origen, origenDsc;
        string acorreo , titulo, total, dia , semana , mes, NomCLinete;

        public string OrigenDsc { get { return origenDsc; } set { origenDsc = value; } }
        public string Origen { get { return origen; } set { origen = value; } }
        public string NUmCLinete { get { return NomCLinete; } set { NomCLinete = value; } }
        public string Id { get { return id; } set { id = value; } }
        public string Fecha { get { return fecha; } set { fecha = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Correo { get { return correo; } set { correo = value; } }
        public string Asunto { get { return asunto; } set { asunto = value; } }
        public string Contenido { get { return contenido; } set { contenido = value; } }
        public string Acorreo { get { return acorreo; } set { acorreo = value; } }
        public string Titulo { get { return titulo; } set { titulo = value; } }

        public string Total { get { return total; } set { total = value; } }
        public string DIa { get { return dia; } set { dia = value; } }
        public string Semana { get { return semana; } set { semana = value; } }
        public string Mes { get { return mes; } set { mes = value; } }
    }
}
