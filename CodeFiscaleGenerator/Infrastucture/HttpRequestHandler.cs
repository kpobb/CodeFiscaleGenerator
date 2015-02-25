using System.IO;
using System.Net;
using System.Text;

namespace CodeFiscaleGenerator.Infrastucture
{
    internal sealed class HttpRequestHandler
    {
        private const int RequestTimeoutMs = 5000;
        private const string ContentType = "application/xml";

        public enum HttpMethod
        {
            GET,
            DELETE
        }

        public void ExecutePost(string url, string data)
        {
            var request = WebRequest.Create(url);
            request.ContentType = ContentType;
            request.Method = "POST";
            request.Timeout = RequestTimeoutMs;

            var postData = Encoding.ASCII.GetBytes(data);
            request.ContentLength = postData.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
            }
        }

        public string ExecuteHttpRequest(string url, HttpMethod method = HttpMethod.GET)
        {
            var request = WebRequest.Create(url);
            request.ContentType = ContentType;
            request.Method = method.ToString();
            request.Timeout = RequestTimeoutMs;

            using (var response = request.GetResponse())
            {
                var stream = response.GetResponseStream();

                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            return null;
        } 
    }
}
