using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace group_buy.Controllers
{
    public class JsonContentNegotiator : IContentNegotiator

    {

        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonContentNegotiator(JsonMediaTypeFormatter formatter)

        {

            _jsonFormatter = formatter;

        }

        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)

        {

            var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));

            return result;

        }

    }
}