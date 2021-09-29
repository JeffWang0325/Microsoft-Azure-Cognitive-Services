using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CustomVision
{
    public class CustomVisionResult
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "project")]
        public Guid Project { get; set; }

        [JsonProperty(PropertyName = "iteration")]
        public Guid Iteration { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "predictions")]
        public IList<PredictionModel> Predictions { get; set; }



        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }

        public void Set_Image_WidthHeight(int imageWidth, int imageHeight)
        {
            this.ImageWidth = imageWidth;
            this.ImageHeight = imageHeight;
        }
    }

    public class PredictionModel
    {
        [JsonProperty(PropertyName = "probability")]
        public double Probability { get; set; }

        [JsonProperty(PropertyName = "tagId")]
        public Guid TagId { get; set; }

        [JsonProperty(PropertyName = "tagName")]
        public string TagName { get; set; }

        [JsonProperty(PropertyName = "boundingBox")]
        public BoundingBox BoundingBox { get; set; }

        [JsonProperty(PropertyName = "tagType")]
        public string TagType { get; set; }
    }

    public class BoundingBox
    {
        [JsonProperty(PropertyName = "left")]
        public double Left { get; set; }

        [JsonProperty(PropertyName = "top")]
        public double Top { get; set; }

        [JsonProperty(PropertyName = "width")]
        public double Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }
    }
}
