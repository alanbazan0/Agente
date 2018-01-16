using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class CRM
    {
        String idFolio,fecha, dia, hora, canal, temaPrincipal, tipificación, canalId, cantidad, idLlamada, extension, telefonoCliente, idCliente,idAgente,nombreAgente;

        public string IdFolio { get { return idFolio; } set { idFolio = value; } }
        public string Fecha { get { return fecha; } set { fecha = value; } }
        public string Dia { get { return dia; } set { dia = value; } }
        public string Hora { get { return hora; } set { hora = value; } }
        public string Canal { get { return canal; } set { canal = value; } }
        public string TemaPrincipal { get { return temaPrincipal; } set { temaPrincipal = value; } }
        public string Tipificación { get { return tipificación; } set { tipificación = value; } }
        public string CanalId { get { return canalId; } set { canalId = value; } }
        public string Cantidad { get { return cantidad; } set { cantidad = value; } }
        public string IdLlamada { get { return idLlamada; } set { idLlamada = value; } }
        public string Extension { get { return extension; } set { extension = value; } }
        public string TelefonoCliente { get { return telefonoCliente; } set { telefonoCliente = value; } }
        public string IdCliente { get { return idCliente; } set { idCliente = value; } }
        public string IdAgente { get { return idAgente; } set { idAgente = value; } }
        public string NombreAgente { get { return nombreAgente; } set { nombreAgente = value; } }
    }
}
