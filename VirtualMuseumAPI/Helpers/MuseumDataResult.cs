using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace VirtualMuseumAPI.Helpers
{
    public class MuseumDataResult : IHttpActionResult
    {
        private readonly MemoryStream _stream;

        public MuseumDataResult(MemoryStream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            _stream = stream;

        }

        public Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(_stream)
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                return response;

            }, cancellationToken);
        }
    }
}