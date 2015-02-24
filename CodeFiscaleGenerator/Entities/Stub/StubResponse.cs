using System.Xml.Serialization;

namespace CodeFiscaleGenerator.Entities.Stub
{
    [XmlRoot(ElementName = "ConfigurableResponses", Namespace = "")]
    public class StubResponse
    {
        [XmlElement(ElementName = "ConfigurableResponse")]
        public CodeFiscaleData[] Items { get; set; }
    }
}