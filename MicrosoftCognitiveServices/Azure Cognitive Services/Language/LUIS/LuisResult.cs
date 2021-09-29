using Newtonsoft.Json;

namespace LUIS
{
    /// <summary>
    /// For v2.0
    /// </summary>
    public class LuisResult
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("topScoringIntent")]
        public Topscoringintent TopScoringIntent { get; set; }

        /// <summary>
        /// 實體
        /// </summary>
        [JsonProperty("entities")]
        public Entity[] Entities { get; set; }

        [JsonProperty("sentimentAnalysis")]
        public Sentimentanalysis SentimentAnalysis { get; set; }
    }

    public class Topscoringintent
    {
        /// <summary>
        /// 意圖
        /// </summary>
        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }
    }

    public class Sentimentanalysis
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }
    }

    public class Entity
    {
        [JsonProperty("entity")]
        public string EntityItem { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("startIndex")]
        public int StartIndex { get; set; }

        [JsonProperty("endIndex")]
        public int EndIndex { get; set; }

        [JsonProperty("resolution")]
        public Resolution Resolution { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }
    }

    public class Resolution
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("values")]
        public string[] Values { get; set; }
    }
}
