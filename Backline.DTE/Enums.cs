using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Backline.DTE
{
    public class Enums
    {
        public enum TipoDocumento
        {
            FacturaElectronica = 33,
            BoletaElectronica = 39,
            BoletaElectronicaExenta = 41,
            NotaCredito=61
        }

        public enum UnidadMedida
        {
            [Description("TON")]
            Tonelada = 1,
            [Description("KG")]
            Kilogramo = 2,
            [Description("UNID")]
            Unidades = 2,
            [Description("QTAL")]
            Quintal = 2,
            [Description("M3")]
            MetroCubico = 2,
            [Description("MR")]
            MetroRuma = 2,
            [Description("TBDM")]
            ToneladaBDMT = 2,
            [Description("TBDU")]
            ToneladaBDU = 2,
            [Description("PP")]
            PulgadaPinera = 2,
            [Description("PM")]
            PulgadaMaderera = 2,

        }
    }
}
