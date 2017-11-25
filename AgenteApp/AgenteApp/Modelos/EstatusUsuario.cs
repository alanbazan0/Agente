using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
   public class EstatusUsuario
    {
        String estatusId, llamadaId, usuarioID, dscReceso, recesoId;

        public string EstatusId { get { return estatusId; } set { estatusId = value; } }
        public string LlamadaId { get { return llamadaId; } set { llamadaId = value; } }
        public string UsuarioID { get { return usuarioID; } set { usuarioID = value; } }
        public string DscReceso { get { return dscReceso; } set { dscReceso = value; } }
        public string RecesoId { get { return recesoId; } set { recesoId = value; } }
    }
}
