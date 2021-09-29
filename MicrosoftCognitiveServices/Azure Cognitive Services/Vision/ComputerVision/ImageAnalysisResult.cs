using Newtonsoft.Json;
using System.Collections.Generic;

namespace ComputerVision
{
    public class ImageAnalysisResult
    {
        [JsonProperty("categories")]
        //public Category[] Categories_ { get; set; }
        public IList<Category> Categories_ { get; set; }

        [JsonProperty("adult")]
        public Adult Adult_ { get; set; }

        [JsonProperty("color")]
        public ColorInfo Color_ { get; set; }

        [JsonProperty(PropertyName = "imageType")]
        public ImageType ImageType { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags_ { get; set; }

        [JsonProperty("description")]
        public Description Description_ { get; set; }

        [JsonProperty("faces")]
        public Face[] Faces_ { get; set; }

        [JsonProperty(PropertyName = "objects")]
        public IList<DetectedObject> Objects { get; set; }

        [JsonProperty(PropertyName = "brands")]
        public IList<DetectedBrand> Brands { get; set; }

        [JsonProperty("requestId")]
        public string RequestId_ { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata_ { get; set; }
    }

    public class Category
    {
        public Category() { }
        public Category(string name = null, double score = 0, CategoryDetail detail = null)
        {
            this.Name_ = name;
            this.Score_ = score;
            this.Detail_ = detail;
        }

        [JsonProperty("name")]
        public string Name_ { get; set; }

        [JsonProperty("score")]
        public double Score_ { get; set; }

        [JsonProperty("detail")]
        //public object Detail_ { get; set; }
        //public Dictionary<string, object[]> Detail_ { get; set; }
        //public Dictionary<string, Detail[]> Detail_ { get; set; } // Method 1
        public CategoryDetail Detail_ { get; set; }
    }

    public class Detail
    {
        [JsonProperty("name")]
        public string Name_ { get; set; }

        [JsonProperty("confidence")]
        public double Confidence_ { get; set; }

        [JsonProperty("faceRectangle")]
        public Rect FaceRectangle_ { get; set; }
    }

    public class CategoryDetail
    {
        public CategoryDetail() { }
        public CategoryDetail(IList<CelebritiesModel> celebrities = null, IList<LandmarksModel> landmarks = null)
        {
            this.Celebrities = celebrities;
            this.Landmarks = landmarks;
        }

        [JsonProperty(PropertyName = "celebrities")]
        public IList<CelebritiesModel> Celebrities { get; set; }

        [JsonProperty(PropertyName = "landmarks")]
        public IList<LandmarksModel> Landmarks { get; set; }
    }

    public class CelebritiesModel
    {
        public CelebritiesModel() { }
        public CelebritiesModel(string name = null, double confidence = 0, Rect faceRectangle = null)
        {
            this.Name = name;
            this.Confidence = confidence;
            this.FaceRectangle = faceRectangle;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }

        [JsonProperty(PropertyName = "faceRectangle")]
        public Rect FaceRectangle { get; set; }
    }

    public class LandmarksModel
    {
        public LandmarksModel() { }
        public LandmarksModel(string name = null, double confidence = 0)
        {
            this.Name = name;
            this.Confidence = confidence;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }
    }

    public class Rect
    {
        [JsonProperty("left")]
        public int Left_ { get; set; }

        [JsonProperty("top")]
        public int Top_ { get; set; }

        [JsonProperty("width")]
        public int Width_ { get; set; }

        [JsonProperty("height")]
        public int Height_ { get; set; }
    }

    public class Adult
    {
        [JsonProperty("isAdultContent")]
        public bool IsAdultContent_ { get; set; }

        [JsonProperty("isRacyContent")]
        public bool IsRacyContent_ { get; set; }

        [JsonProperty("isGoryContent")]
        public bool IsGoryContent_ { get; set; }

        [JsonProperty("adultScore")]
        public double AdultScore_ { get; set; }

        [JsonProperty("racyScore")]
        public double RacyScore_ { get; set; }

        [JsonProperty("goreScore")]
        public double GoreScore_ { get; set; }
    }

    public class ColorInfo
    {
        [JsonProperty("dominantColorForeground")]
        public string DominantColorForeground_ { get; set; }

        [JsonProperty("dominantColorBackground")]
        public string DominantColorBackground_ { get; set; }

        [JsonProperty("dominantColors")]
        public string[] DominantColors_ { get; set; } // OK!
        //public List<string> DominantColors_ { get; set; } // OK!

        [JsonProperty("accentColor")]
        public string AccentColor_ { get; set; }

        [JsonProperty("isBwImg")]
        public bool IsBwImg_ { get; set; }

        /// <summary>
        /// Is black and white image?
        /// </summary>
        [JsonProperty("isBWImg")]
        public bool IsBWImg_ { get; set; }
    }

    public class ImageType
    {
        public ImageType() { }
        public ImageType(int clipArtType = 0, int lineDrawingType = 0)
        {
            this.ClipArtType = clipArtType;
            this.LineDrawingType = lineDrawingType;
        }

        [JsonProperty(PropertyName = "clipArtType")]
        public int ClipArtType { get; set; }

        [JsonProperty(PropertyName = "lineDrawingType")]
        public int LineDrawingType { get; set; }
    }

    public class Tag
    {
        [JsonProperty("name")]
        public string Name_ { get; set; }

        [JsonProperty("confidence")]
        public double Confidence_ { get; set; }
    }

    public class Description
    {
        [JsonProperty("tags")]
        public string[] Tags_ { get; set; }

        [JsonProperty("captions")]
        public Caption[] Captions_ { get; set; }
    }

    public class Caption
    {
        [JsonProperty("text")]
        public string Text_ { get; set; }

        [JsonProperty("confidence")]
        public double Confidence_ { get; set; }
    }

    public class Face
    {
        [JsonProperty("age")]
        public int Age_ { get; set; }

        [JsonProperty("gender")]
        public string Gender_ { get; set; }

        [JsonProperty("faceRectangle")]
        public Rect FaceRectangle_ { get; set; }
    }

    public class DetectedObject
    {
        public DetectedObject() { }
        public DetectedObject(BoundingRect rectangle = null, string objectProperty = null, double confidence = 0, ObjectHierarchy parent = null)
        {
            this.Rectangle = rectangle;
            this.ObjectProperty = objectProperty;
            this.Confidence = confidence;
            this.Parent = parent;
        }

        [JsonProperty(PropertyName = "rectangle")]
        public BoundingRect Rectangle { get; set; }

        [JsonProperty(PropertyName = "object")]
        public string ObjectProperty { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }

        [JsonProperty(PropertyName = "parent")]
        public ObjectHierarchy Parent { get; set; }
    }

    public class BoundingRect
    {
        public BoundingRect() { }
        public BoundingRect(int x = 0, int y = 0, int w = 0, int h = 0)
        {
            this.X = x;
            this.Y = y;
            this.W = w;
            this.H = h;
        }

        [JsonProperty(PropertyName = "x")]
        public int X { get; set; }

        [JsonProperty(PropertyName = "y")]
        public int Y { get; set; }

        [JsonProperty(PropertyName = "w")]
        public int W { get; set; }

        [JsonProperty(PropertyName = "h")]
        public int H { get; set; }
    }

    public class ObjectHierarchy
    {
        public ObjectHierarchy() { }
        public ObjectHierarchy(string objectProperty = null, double confidence = 0, ObjectHierarchy parent = null)
        {
            this.ObjectProperty = objectProperty;
            this.Confidence = confidence;
            this.Parent = parent;
        }

        [JsonProperty(PropertyName = "object")]
        public string ObjectProperty { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }

        [JsonProperty(PropertyName = "parent")]
        public ObjectHierarchy Parent { get; set; }
    }

    public class DetectedBrand
    {
        public DetectedBrand() { }
        public DetectedBrand(string name = null, double confidence = 0, BoundingRect rectangle = null)
        {
            this.Name = name;
            this.Confidence = confidence;
            this.Rectangle = rectangle;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }

        [JsonProperty(PropertyName = "rectangle")]
        public BoundingRect Rectangle { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("width")]
        public int Width_ { get; set; }

        [JsonProperty("height")]
        public int Height_ { get; set; }

        [JsonProperty("format")]
        public string Format_ { get; set; }
    }}
