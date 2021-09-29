using Newtonsoft.Json;

namespace QnAMaker
{
    public class Question
    {
        /// <summary>
        /// 要詢問的問題
        /// </summary>
        [JsonProperty("question")]
        public string QuestionStr { get; set; } = "";

        /// <summary>
        /// 取回分數最高的答案筆數
        /// </summary>
        [JsonProperty("top")]
        public int Top { get; set; } = 2;
    }
}
