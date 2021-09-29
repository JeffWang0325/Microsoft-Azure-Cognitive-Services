using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Training;

namespace FormRecognizer
{
    public class FormRecognizerApp
    {
        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private static FormRecognizerClient AuthenticateClient(string apiKey, string endpoint)
        {
            var credential = new AzureKeyCredential(apiKey);
            var client = new FormRecognizerClient(new Uri(endpoint), credential);
            return client;
        }

        public static async Task<RecognizedFormCollection> MakeRequest(string endpoint, string apiKey, string imgfilepath)
        {
            RecognizedFormCollection result = null;

            try
            {
                var client = AuthenticateClient(apiKey, endpoint);
                //using var stream = new FileStream(imgfilepath, FileMode.Open); // Exception: The process cannot access the file '...\\...jpg' because it is being used by another process.
                using (var stream = new FileStream(imgfilepath, FileMode.Open, FileAccess.Read))
                {
                    RecognizeReceiptsOperation operation = await client.StartRecognizeReceiptsAsync(stream);
                    Response<RecognizedFormCollection> operationResponse = await operation.WaitForCompletionAsync();
                    result = operationResponse.Value;
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
