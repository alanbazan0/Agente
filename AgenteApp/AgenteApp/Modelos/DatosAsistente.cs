using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class DatosAsistente
    {
        string version;
        string secuencia;
        string jerId1;
        string jerId2;
        string jerId3;
        string jerId4;
        string jerId5;
        string campoDescripcion;
        string campoCriterio;
        public string VErsion { get { return version; } set { version = value; } }
        public string Secuencia { get { return secuencia; } set { secuencia = value; } }
        public string JerId1 { get { return jerId1; } set { jerId1 = value; } }
        public string JerId2 { get { return jerId2; } set { jerId2 = value; } }
        public string JerId3 { get { return jerId3; } set { jerId3 = value; } }
        public string JerId4 { get { return jerId4; } set { jerId4 = value; } }
        public string JerId5 { get { return jerId5; } set { jerId5 = value; } }
        public string CampoDescripcion { get { return campoDescripcion; } set { campoDescripcion = value; } }
        public string CampoCriterio { get { return campoCriterio; } set { campoCriterio = value; } }
    }
}
