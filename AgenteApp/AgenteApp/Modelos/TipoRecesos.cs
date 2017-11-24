using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class TipoRecesos
    {
        string id, rDescripcion, rCorto, rLargo, rTiempo, rRecesos;
        string usuario, consulto;
        public string Id { get { return id; } set { id = value; } }
        public string RDescripcion { get { return rDescripcion; } set { rDescripcion = value; } }
        public string RCorto { get { return rCorto; } set { rCorto = value; } }
        public string RLargo { get { return rLargo; } set { rLargo = value; } }
        public string RTiempo { get { return rTiempo; } set { rTiempo = value; } }
        public string RRecesos { get { return rRecesos; } set { rRecesos = value; } }
        public string Consulto { get { return consulto; } set { consulto = value; } }
        public string Usuario { get { return usuario; } set { usuario = value; } }
    }
}
