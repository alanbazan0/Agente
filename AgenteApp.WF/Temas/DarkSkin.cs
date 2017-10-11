using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgenteApp.WF.Temas
{
    class DarkSkin : RibbonProfesionalRendererColorTable
    {
        public DarkSkin()
        {
            OrbDropDownDarkBorder = Color.Yellow;
            OrbDropDownLightBorder = Color.FromKnownColor(KnownColor.WindowFrame);
            OrbDropDownBack = Color.FromName("Red");
            OrbDropDownNorthA = FromHex("#C2FF3D");
            OrbDropDownNorthB = Color.FromArgb(201, 100, 150);
        }

    }
}
