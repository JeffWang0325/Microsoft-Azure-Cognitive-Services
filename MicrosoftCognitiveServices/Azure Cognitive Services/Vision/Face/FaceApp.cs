using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace Face
{
    public class FaceApp
    {
        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static IFaceClient Authenticate(string endpoint, string key)
        {
            return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        }

        /// <summary>
        /// Call Face SDK (Face Detection)
        /// </summary>
        /// <param name="endpoint_"></param>
        /// <param name="subscriptionKey_"></param>
        /// <param name="imgfilepath"></param>
        /// <returns></returns>
        public static async Task<List<DetectedFace>> MakeRequest(string endpoint_, string subscriptionKey_, string imgfilepath)
        {
            var result = new List<DetectedFace>();

            try
            {
                IFaceClient client = Authenticate(endpoint_, subscriptionKey_);
                using (Stream detectImageStream = File.OpenRead(imgfilepath))
                {
                    string RECOGNITION_MODEL3 = RecognitionModel.Recognition03;

                    // Detect faces with all attributes from image url.
                    IList<DetectedFace> detectedFaces = await client.Face.DetectWithStreamAsync(detectImageStream,
                        returnFaceAttributes: new List<FaceAttributeType?> { FaceAttributeType.Accessories, FaceAttributeType.Age,
                        FaceAttributeType.Blur, FaceAttributeType.Emotion, FaceAttributeType.Exposure, FaceAttributeType.FacialHair,
                        FaceAttributeType.Gender, FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.HeadPose,
                        FaceAttributeType.Makeup, FaceAttributeType.Noise, FaceAttributeType.Occlusion, FaceAttributeType.Smile },
                        // We specify detection model 1 because we are retrieving attributes.
                        detectionModel: DetectionModel.Detection01,
                        recognitionModel: RECOGNITION_MODEL3);

                    result = detectedFaces as List<DetectedFace>;
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
