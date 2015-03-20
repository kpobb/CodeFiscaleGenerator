using System;
using System.Linq;
using System.Xml.Serialization;

namespace CodeFiscaleGenerator.Entities.Stub
{
    [XmlRoot(ElementName = "ConfigurableResponses", Namespace = "")]
    public class StubResponse
    {
        [XmlElement(ElementName = "ConfigurableResponse")]
        public CodeFiscaleData[] Items { get; set; }

        public bool HasCodeFiscale(string codeFiscale)
        {
            return Items != null && Items.Any(s => s.FiscalCode.Equals(codeFiscale, StringComparison.InvariantCultureIgnoreCase));
        }

        public CodeFiscaleData FindCodeFiscaleData(string codeFiscale)
        {
            return Items.FirstOrDefault(s => s.FiscalCode.Equals(codeFiscale, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}