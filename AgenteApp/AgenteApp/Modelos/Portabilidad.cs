using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Modelos
{
    public class Portabilidad
    {
        string idMunicipio;
        string idConsecutivo;
        string numeroPortabilidad;
        string descripcionPortabilidad;
        string ciudadPortabilidad;
        string municipioPortabilidad;
        string estadoPortabilidad;
        string redPortabilidad;
        string tipoLlamadaPortabilidad;
        public string IdMunicipio  { get { return idMunicipio; } set { idMunicipio = value; } }
        public string IdConsecutivo { get { return idConsecutivo; } set { idConsecutivo = value; } }
        public string NumeroPortabilidad { get { return numeroPortabilidad; } set { numeroPortabilidad = value; } }
        public string DescripcionPortabilidad { get { return descripcionPortabilidad; } set { descripcionPortabilidad = value; } }
        public string CiudadPortabilidad { get { return ciudadPortabilidad; } set { ciudadPortabilidad = value; } }
        public string MunicipioPortabilidad { get { return municipioPortabilidad; } set { municipioPortabilidad = value; } }
        public string EstadoPortabilidad { get { return estadoPortabilidad; } set { estadoPortabilidad = value; } }
        public string RedPortabilidad { get { return redPortabilidad; } set { redPortabilidad = value; } }
        public string TipoLlamadaPortabilidad { get { return tipoLlamadaPortabilidad; } set { tipoLlamadaPortabilidad = value; } }
    }
}
