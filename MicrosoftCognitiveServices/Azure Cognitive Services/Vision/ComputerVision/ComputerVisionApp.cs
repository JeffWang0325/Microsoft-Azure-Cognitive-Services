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

namespace ComputerVision
{
    public class ComputerVisionApp
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum VisualFeatureTypes
        {
            ImageType = 0,
            Faces = 1,
            Adult = 2,
            Categories = 3,
            Color = 4,
            Tags = 5,
            Description = 6,
            Objects = 7,
            Brands = 8
        }

        /// <summary>
        /// Call Computer Vision API (Analyze Image & Detect Object)
        /// </summary>
        /// <param name="endpoint_"></param>
        /// <param name="subscriptionKey_"></param>
        /// <param name="imgfilepath"></param>
        /// <param name="reqFeatureTypes"></param>
        /// <returns></returns>
        public static async Task<ImageAnalysisResult> MakeRequest_Analysis(string endpoint_, string subscriptionKey_, string imgfilepath, List<VisualFeatureTypes> reqFeatureTypes = null)
        {
            var result = new ImageAnalysisResult();

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey_);

                // Request parameters.
                /* visualFeatures :
                 *      Categories, Tags, Description, Faces, ImageType, Color, Adult 
                 * details : 
                 *      Celebrities, Landmarks
                 */
                //string reqparameter = "visualFeatures=Categories,Tags,Description,Color,Faces,Adult";
                //string reqparameter = "visualFeatures=Categories,Tags,Description,Color,Faces,Adult,Objects,ImageType,Brands";
                string reqparameter = "";
                if (reqFeatureTypes == null || reqFeatureTypes.Count > 0)
                {
                    reqparameter = "visualFeatures=";
                    if (reqFeatureTypes == null) // All features
                    {
                        reqFeatureTypes = new List<VisualFeatureTypes>();
                        foreach (var i in Enum.GetNames(typeof(VisualFeatureTypes)))
                            reqFeatureTypes.Add((VisualFeatureTypes)Enum.Parse(typeof(VisualFeatureTypes), i, true));
                    }
                    reqparameter += string.Join(",", reqFeatureTypes);
                    reqparameter += "&details=Celebrities,Landmarks";
                    //reqparameter += "&details=Landmarks"; // Jeff Revised!
                }
                else
                    reqparameter = "details=Celebrities,Landmarks";

                // Assemble the URI for the REST API Call.
                //string uri = endpoint_ + "vision/v3.0/analyze" + "?" + reqparameter; // API位置: vision/v3.0/analyze
                string uri = endpoint_ + "vision/v3.1/analyze" + "?" + reqparameter; // API位置: vision/v3.1/analyze

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

                    result = JsonConvert.DeserializeObject<ImageAnalysisResult>(val);
                    if (result.RequestId_ == null)
                        result = null;
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Call Computer Vision API (Read Text (OCR))
        /// </summary>
        /// <param name="endpoint_"></param>
        /// <param name="subscriptionKey_"></param>
        /// <param name="imgfilepath"></param>
        /// <returns></returns>
        public static async Task<ReadOperationResult> MakeRequest_OCR(string endpoint_, string subscriptionKey_, string imgfilepath)
        {
            var result = new ReadOperationResult();

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey_);

                // Assemble the URI for the REST API Call.
                string uri = endpoint_ + "vision/v3.1/read/analyze"; // API位置: vision/v3.1/read/analyze

                HttpResponseMessage response;

                // Two REST API methods are required to extract text.
                // One method to submit the image for processing, the other method
                // to retrieve the text found in the image.

                // operationLocation stores the URI of the second REST API method,
                // returned by the first REST API method.
                string operationLocation;

                // Reads the contents of the specified local image
                // into a byte array.
                byte[] imgdata = GetImageAsByteArray(imgfilepath);

                // Adds the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(imgdata))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json"
                    // and "multipart/form-data".
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    // The first REST API method, Batch Read, starts
                    // the async process to analyze the written text in the image.
                    response = await client.PostAsync(uri, content);
                }

                // The response header for the Batch Read method contains the URI
                // of the second method, Read Operation Result, which
                // returns the results of the process in the response body.
                // The Batch Read operation does not return anything in the response body.
                if (response.IsSuccessStatusCode)
                    operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
                else
                {
                    // Display the JSON error data.
                    string errorString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("\n\nResponse:\n{0}\n", JToken.Parse(errorString).ToString());
                    result = null;
                    return result;
                }

                // If the first REST API method completes successfully, the second 
                // REST API method retrieves the text written in the image.
                //
                // Note: The response may not be immediately available. Text
                // recognition is an asynchronous operation that can take a variable
                // amount of time depending on the length of the text.
                // You may need to wait or retry this operation.
                //
                // This example checks once per second for ten seconds.
                string contentString;
                int i = 0;
                do
                {
                    System.Threading.Thread.Sleep(1000);
                    response = await client.GetAsync(operationLocation);
                    contentString = await response.Content.ReadAsStringAsync();
                    ++i;
                }
                while (i < 60 && contentString.IndexOf("\"status\":\"succeeded\"") == -1);

                if (i == 60 && contentString.IndexOf("\"status\":\"succeeded\"") == -1)
                {
                    Console.WriteLine("\nTimeout error.\n");
                    result = null;
                    return result;
                }

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n\n{0}\n", JToken.Parse(contentString).ToString());

                result = JsonConvert.DeserializeObject<ReadOperationResult>(contentString);
                if (result.Status != OperationStatusCodes.Succeeded)
                    result = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
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
