using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web;

namespace QnAMaker
{
    public class QnAMakerApp
    {
        /// <summary>
        /// Call QnA Maker API
        /// </summary>
        /// <param name="url"></param>
        /// <param name="endpoint_key"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        public static async Task<AnswerResult> MakeRequest(string url, string endpoint_key, Question question)
        {
            var result = new AnswerResult();

            try
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage())
                    {
                        request.Method = HttpMethod.Post;
                        request.RequestUri = new Uri(url);
                        request.Headers.Add("Authorization", "EndpointKey "
                            + endpoint_key);

                        string content = JsonConvert.SerializeObject(question);
                        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

                        var response = await client.SendAsync(request);
                        var val = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<AnswerResult>(val);

                        if (result.Answers == null)
                            result = null;
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }
    }
}
