using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace ComputerVision
{
    public class ReadOperationResult
    {
        public ReadOperationResult() { }
        public ReadOperationResult(OperationStatusCodes status = OperationStatusCodes.NotStarted, string createdDateTime = null, string lastUpdatedDateTime = null, AnalyzeResults analyzeResult = null)
        {

        }

        [JsonProperty(PropertyName = "status")]
        public OperationStatusCodes Status { get; set; }

        [JsonProperty(PropertyName = "createdDateTime")]
        public string CreatedDateTime { get; set; }

        [JsonProperty(PropertyName = "lastUpdatedDateTime")]
        public string LastUpdatedDateTime { get; set; }

        [JsonProperty(PropertyName = "analyzeResult")]
        public AnalyzeResults AnalyzeResult { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OperationStatusCodes
    {
        NotStarted = 0,
        Running = 1,
        Failed = 2,
        Succeeded = 3
    }

    public class AnalyzeResults
    {
        public AnalyzeResults() { }
        public AnalyzeResults(string version, IList<ReadResult> readResults)
        {

        }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "readResults")]
        public IList<ReadResult> ReadResults { get; set; }
    }

    public class ReadResult
    {
        public ReadResult() { }
        public ReadResult(int page, double angle, double width, double height, TextRecognitionResultDimensionUnit unit, IList<Line> lines, string language = null)
        {

        }

        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "angle")]
        public double Angle { get; set; }

        [JsonProperty(PropertyName = "width")]
        public double Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }

        [JsonProperty(PropertyName = "unit")]
        public TextRecognitionResultDimensionUnit Unit { get; set; }

        [JsonProperty(PropertyName = "lines")]
        public IList<Line> Lines { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TextRecognitionResultDimensionUnit
    {
        Pixel = 0,
        Inch = 1
    }

    public class Line
    {
        public Line() { }
        public Line(IList<double?> boundingBox, string text, IList<Word> words, string language = null)
        {

        }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "boundingBox")]
        public IList<double?> BoundingBox { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "words")]
        public IList<Word> Words { get; set; }
    }

    public class Word
    {
        public Word() { }
        public Word(IList<double?> boundingBox, string text, double confidence)
        {

        }

        [JsonProperty(PropertyName = "boundingBox")]
        public IList<double?> BoundingBox { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }
    }
}
