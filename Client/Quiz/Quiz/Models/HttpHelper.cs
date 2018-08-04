using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Quiz.Models
{
    public class HttpHelper
    {
        private static HttpClient client = new HttpClient();

        private static async Task<HttpResponseMessage> SendAsync(HttpMethod method, string requestUri,
            HttpContent content = null, string acceptHeader = null)
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(requestUri)
            };

            if (content != null)
                request.Content = content;

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(acceptHeader))
                request.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(acceptHeader));
            
            return await client.SendAsync(request);
        }


        public static async Task<HttpResponseMessage> Get(string requestUri)
        {
            return await SendAsync(HttpMethod.Get, requestUri);
        }

        public static async Task<HttpResponseMessage> Post(
            string requestUri, object value)
        {
            return await SendAsync(HttpMethod.Post, requestUri, new JsonContent(value));
        }

        public static async Task<string> ContentAsString(HttpResponseMessage response)
        {
            var formatters = new List<MediaTypeFormatter>() {
                new JsonMediaTypeFormatter(),
                new XmlMediaTypeFormatter()
            };
            var result = await response.Content.ReadAsAsync<string>(formatters);
            return result;
            //return response.Content.ReadAsStringAsync().Result;
        }

        private class JsonContent : StringContent
        {
            public JsonContent(object value)
                : base(JsonConvert.SerializeObject(value), Encoding.UTF8,
                    "application/json")
            {
            }

            public JsonContent(object value, string mediaType)
                : base(JsonConvert.SerializeObject(value), Encoding.UTF8, mediaType)
            {
            }
        }

        //public static class HttpResponseExtensions
        //{
        //    public static T ContentAsType<T>(HttpResponseMessage response)
        //    {
        //        var data = response.Content.ReadAsStringAsync().Result;
        //        return string.IsNullOrEmpty(data) ?
        //            default(T) :
        //            JsonConvert.DeserializeObject<T>(data);
        //    }

        //    public static string ContentAsJson(HttpResponseMessage response)
        //    {
        //        var data = response.Content.ReadAsStringAsync().Result;
        //        return JsonConvert.SerializeObject(data);
        //    }

            
        //}
    }

    
}
