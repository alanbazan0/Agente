using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class Supervisores
    {
        private string idSupervisor;
        private string nombreSupervisor;
        private string puestoSupervisor;
        private string extensionSupervisor;
        public string IdSupervisor { get { return idSupervisor; } set { idSupervisor = value; } }
        public string NombreSupervisor { get { return nombreSupervisor; } set { nombreSupervisor = value; } }
        public string PuestoSupervisor { get { return puestoSupervisor; } set { puestoSupervisor = value; } }
        public string ExtensionSupervisor { get { return extensionSupervisor; } set { extensionSupervisor = value; } }
    }
}
