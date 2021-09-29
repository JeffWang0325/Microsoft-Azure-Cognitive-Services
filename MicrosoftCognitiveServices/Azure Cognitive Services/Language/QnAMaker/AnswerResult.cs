using Newtonsoft.Json;
using System.Collections.Generic;

namespace QnAMaker
{
    public class AnswerResult
    {
        [JsonProperty("answers")]
        public List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        [JsonProperty("answer")]
        public string AnswerStr { get; set; }

        [JsonProperty("questions")]
        public List<string> Questions { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }
}
