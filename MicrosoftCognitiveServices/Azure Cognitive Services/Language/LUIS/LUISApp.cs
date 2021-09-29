using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;

namespace LUIS
{
    public class LUISApp
    {
        /// <summary>
        /// Call LUIS API
        /// </summary>
        /// <param name="luisappid"></param>
        /// <param name="subscriptionkey"></param>
        /// <param name="usertalk"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static async Task<LuisResult> MakeRequest(string luisappid, string subscriptionkey, string usertalk, string location = "westus")
        {
            var result = new LuisResult();

            try
            {
                var endpoint = "https://" + location + ".api.cognitive.microsoft.com/luis/v2.0/apps/" + luisappid + "?"; // For v2.0
                var client = new HttpClient();
                using (var request = new HttpRequestMessage())
                {
                    // Request parameters
                    // Method 1: 在Console應用程式，輸入中文時，會編碼成Unicode ---> 問題已解決!!!
                    // HttpUtility.ParseQueryString 還原字串中文編碼問題: https://blog.darkthread.net/blog/httpvaluecollection-tostring-urlencode/
                    var querystr = HttpUtility.ParseQueryString(string.Empty); // 輸入中文時，會編碼成Unicode
                    querystr["verbose"] = "true"; //需要回傳的所有 Intent ，將 verbose 設定為 true
                    querystr["q"] = usertalk;
                    querystr["spellCheck"] = "false";
                    querystr["staging"] = "false";
                    querystr["log"] = "true";

                    // Method 2
                    //string querystr2 = "verbose=true&q=" + usertalk + "&spellCheck=false&staging=false&log=true"; // For v2.0

                    // Method 3
                    //var querystr = HttpUtility.ParseQueryString(string.Empty); // 輸入中文時，會編碼成Unicode
                    //querystr["verbose"] = "true"; //需要回傳的所有 Intent ，將 verbose 設定為 true
                    //querystr["spellCheck"] = "false";
                    //querystr["staging"] = "false";
                    //querystr["log"] = "true";
                    //var uri = endpoint + "q=" + HttpUtility.UrlEncode(usertalk) + "&" + querystr;

                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri(endpoint + querystr); // Method 1
                    //request.RequestUri = new Uri(endpoint + querystr2); // Method 2
                    //request.RequestUri = new Uri(uri); // Method 3

                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionkey);

                    var response = await client.SendAsync(request);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<LuisResult>(responseBody);

                    if (result.Query == null)
                        result = null;
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
