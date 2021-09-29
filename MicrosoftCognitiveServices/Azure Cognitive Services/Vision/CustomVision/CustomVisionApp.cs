using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

using MicrosoftCognitiveServices;
using System.Drawing;

namespace CustomVision
{
    public class CustomVisionApp
    {
        public static async Task<CustomVisionResult> MakeRequest(string endpoint_, string subscriptionKey_, string imgfilepath)
        {
            var result = new CustomVisionResult();

            try
            {
                HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey_);
                client.DefaultRequestHeaders.Add("Prediction-Key", subscriptionKey_);

                // Assemble the URI for the REST API Call.
                string uri = endpoint_;

                HttpResponseMessage response;

                // Request body. Posts a locally stored JPEG image.
                byte[] imgdata = GetImageAsByteArray(imgfilepath);

                // Adds the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(imgdata))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    // Execute the REST API call.
                    response = await client.PostAsync(uri, content);

                    // Get the JSON response.
                    string val = await response.Content.ReadAsStringAsync();

                    // Display the JSON response.
                    Console.WriteLine("======== 辨識結果 ================");
                    Console.WriteLine(JToken.Parse(val).ToString());
                    //Console.WriteLine(val);

                    result = JsonConvert.DeserializeObject<CustomVisionResult>(val);
                    if (result.Id == null)
                        result = null;
                    else
                    {
                        Size size = clsStaticTool.GetImageSize2(imgfilepath);
                        result.Set_Image_WidthHeight(size.Width, size.Height);
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }

        private static byte[] GetImageAsByteArray(string imgfilepath)
        {
            using (FileStream fileStream = new FileStream(imgfilepath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
    }
}