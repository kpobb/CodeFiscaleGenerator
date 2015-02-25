using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CodeFiscaleGenerator.Configurations;
using CodeFiscaleGenerator.Entities.Stub;
using CodeFiscaleGenerator.Infrastucture;

namespace CodeFiscaleGenerator.Services
{
    internal sealed class PlatformStubService
    {
        private readonly HttpRequestHandler _requestHandler;
        private readonly StubConfiguration _configuration;

        public PlatformStubService(HttpRequestHandler requestHandler, StubConfiguration configuration)
        {
            _requestHandler = requestHandler;
            _configuration = configuration;
        }

        public StubResponse GetCodeFiscaleList(int labelId)
        {
            var serializer = new XmlSerializer(typeof(StubResponse));

            var url = string.Format(_configuration.ViewCodeFiscaleUrl, labelId);

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
                Items = new[]
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

            var url = string.Format(_configuration.CreateCodeFiscaleUrl, labelId);

            _requestHandler.ExecutePost(url, builder.ToString());
        }

        public void DeleteCodeFiscale(int labelId, string codeFiscale)
        {
            var url = string.Format(_configuration.DeletetCodeFiscaleUrl, labelId, codeFiscale);

           _requestHandler.ExecuteHttpRequest(url, HttpRequestHandler.HttpMethod.DELETE);
        }
    }
}
