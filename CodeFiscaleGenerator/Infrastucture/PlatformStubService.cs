using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CodeFiscaleGenerator.Entities;
using CodeFiscaleGenerator.Entities.Stub;

namespace CodeFiscaleGenerator.Infrastucture
{
    internal class PlatformStubService
    {
        private readonly HttpRequestHandler _requestHandler;

        public PlatformStubService(HttpRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
        }

        public StubResponse GetCodeFiscaleList(int labelId)
        {
            var serializer = new XmlSerializer(typeof(StubResponse));

            var url = string.Format("https://213.92.84.21:8843/pgad-accounting-protocol-stub/service/rest/configure/getConfiguredResponses/{0}", labelId);

            var response = _requestHandler.ExecuteHttpRequest(url);

            var textReader = new StringReader(response);

            var xmlNamespace = new XmlSerializerNamespaces();
            xmlNamespace.Add(string.Empty, string.Empty);

            return serializer.Deserialize(textReader) as StubResponse;
        }

        public void RegisterNewCodeFiscale(string codeFiscale, int registrationId, int subRegistrationId, int labelId)
        {
            var configurableResponses = new StubResponse
            {
                CodeFiscaleArray = new[]
                {
                    new CodeFiscaleData
                    {
                        SubregistrationResponseCode = subRegistrationId,
                        IndividualAccountOpeningResponseCode = registrationId,
                        Subregistration2ResponseCode = subRegistrationId,
                        FiscalCode = codeFiscale
                    }
                }
            };

            var serializer = new XmlSerializer(typeof(StubResponse));
            var builder = new StringBuilder();
            var settings = new XmlWriterSettings { OmitXmlDeclaration = true };

            using (var stringWriter = XmlWriter.Create(builder, settings))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);

                serializer.Serialize(stringWriter, configurableResponses, ns);
            }

            var url = string.Format("https://213.92.84.21:8843/pgad-accounting-protocol-stub/service/rest/configure/addResponses/{0}", labelId);

            _requestHandler.ExecutePost(url, builder.ToString());
        }

        public void DeleteCodeFiscale(int labelId, string codeFiscale)
        {
            var url = string.Format("https://213.92.84.21:8843/pgad-accounting-protocol-stub/service/rest/configure/resetResponseByFiscalCode/{0}/{1}",
                            labelId, codeFiscale);

           _requestHandler.ExecuteHttpRequest(url, HttpRequestHandler.HttpMethod.DELETE);
        }
    }
}
