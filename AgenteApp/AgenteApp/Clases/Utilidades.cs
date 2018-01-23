using System;
using System.Collections.Generic;
using System.Text;

namespace AgenteApp.Clases
{
    class Utilidades
    {
        public static string UTF8_to_ISO(string value)
        {

            Encoding isoEncoding = Encoding.GetEncoding("ISO-8859-1");
            Encoding utfEncoding = Encoding.UTF8;

            // Converte os bytes 
            byte[] bytesIso = utfEncoding.GetBytes(value);

            //  Obtém os bytes da string UTF 
            byte[] bytesUtf = Encoding.Convert(utfEncoding, isoEncoding, bytesIso);

            // Obtém a string ISO a partir do array de bytes convertido
            string textoISO = utfEncoding.GetString(bytesUtf);

            return textoISO;

        }
    }
}
