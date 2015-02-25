namespace CodeFiscaleGenerator.Configurations
{
    internal sealed class StubConfiguration
    {
        private const string DefaultViewCodeFiscaleUrl = "https://213.92.84.21:8843/pgad-accounting-protocol-stub/service/rest/configure/getConfiguredResponses/{0}";
        private const string DefaultCreateCodeFiscaleUrl = "https://213.92.84.21:8843/pgad-accounting-protocol-stub/service/rest/configure/addResponses/{0}";
        private const string DefaultDeletetCodeFiscaleUrl = "https://213.92.84.21:8843/pgad-accounting-protocol-stub/service/rest/configure/resetResponseByFiscalCode/{0}/{1}";

        public string ViewCodeFiscaleUrl
        {
            get { return DefaultViewCodeFiscaleUrl; }
        }

        public string CreateCodeFiscaleUrl
        {
            get { return DefaultCreateCodeFiscaleUrl; }
        }

        public string DeletetCodeFiscaleUrl
        {
            get { return DefaultDeletetCodeFiscaleUrl; }
        }
    }
}
