using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace TextToSpeech
{
    #region VoiceName

    /*
        https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/language-support#standard-voices
        *Standard voices: 
            Chinese (Male): zh-HK-Danny, zh-CN-Kangkang, zh-TW-Zhiwei, zh-TW-YunJheNeural
            Chinese (Female): zh-HK-TracyRUS, zh-CN-HuihuiRUS, zh-CN-Yaoyao, zh-TW-HanHanRUS, zh-TW-Yating, zh-TW-HsiaoYuNeural
            English (Male): en-IN-Ravi, en-GB-George, en-US-BenjaminRUS, en-US-GuyRUS
            English (Female): en-AU-Catherine, en-AU-HayleyRUS, en-CA-HeatherRUS, en-CA-Linda, en-IN-Heera, en-IN-PriyaRUS, en-GB-HazelRUS, en-GB-Susan, en-US-AriaRUS
            Japanese (Male): ja-JP-KeitaNeural
            Japanese (Female): ja-JP-NanamiNeural
            Korean (Male): ko-KR-InJoonNeural
            Korean (Female): ko-KR-SunHiNeural
    */
    public enum VoiceName_zh_Male
    {
        zh_HK_Danny, zh_CN_Kangkang, zh_TW_Zhiwei, zh_TW_YunJheNeural
    }

    public enum VoiceName_zh_Female
    {
        zh_HK_TracyRUS, zh_CN_HuihuiRUS, zh_CN_Yaoyao, zh_TW_HanHanRUS, zh_TW_Yating, zh_TW_HsiaoYuNeural
    }

    public enum VoiceName_en_Male
    {
        en_IN_Ravi, en_GB_George, en_US_BenjaminRUS, en_US_GuyRUS
    }

    public enum VoiceName_en_Female
    {
        en_AU_Catherine, en_AU_HayleyRUS, en_CA_HeatherRUS, en_CA_Linda, en_IN_Heera, en_IN_PriyaRUS, en_GB_HazelRUS, en_GB_Susan, en_US_AriaRUS
    }

    public enum VoiceName_ja_Male
    {
        ja_JP_KeitaNeural
    }

    public enum VoiceName_ja_Female
    {
        ja_JP_NanamiNeural
    }

    public enum VoiceName_ko_Male
    {
        ko_KR_InJoonNeural
    }

    public enum VoiceName_ko_Female
    {
        ko_KR_SunHiNeural
    }

    #endregion

    public class TextToSpeechApp
    {
        /// <summary>
        /// Call SDK
        /// </summary>
        /// <param name="subscriptionKey"></param>
        /// <param name="region"></param>
        /// <param name="text"></param>
        /// <param name="voiceName"></param>
        /// <returns></returns>
        public static async Task<bool> MakeRequest(string subscriptionKey, string region, string text, string voiceName = "en-US-GuyRUS")
        {
            bool b_result = false;

            try
            {
                var config = SpeechConfig.FromSubscription(subscriptionKey, region);
                config.SpeechSynthesisVoiceName = voiceName;
                using (var synthesizer = new SpeechSynthesizer(config))
                {
                    using (var result = await synthesizer.SpeakTextAsync(text))
                    {
                        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                            b_result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                b_result = false;
            }

            return b_result;
        }

        /// <summary>
        /// Call SDK (Save speech to the specified WAV file)
        /// </summary>
        /// <param name="subscriptionKey"></param>
        /// <param name="region"></param>
        /// <param name="text"></param>
        /// <param name="fileName"></param>
        /// <param name="voiceName"></param>
        /// <returns></returns>
        public static async Task<bool> MakeRequest_SaveAs(string subscriptionKey, string region, string text, string fileName = "text-to-speech.wav", string voiceName = "en-US-GuyRUS")
        {
            bool b_result = false;

            try
            {
                var config = SpeechConfig.FromSubscription(subscriptionKey, region);
                config.SpeechSynthesisVoiceName = voiceName;

                // 儲存音檔
                AudioConfig audioConfig = AudioConfig.FromWavFileOutput(fileName);

                using (var synthesizer = new SpeechSynthesizer(config, audioConfig))
                {
                    using (var result = await synthesizer.SpeakTextAsync(text))
                    {
                        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                            b_result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                b_result = false;
            }

            return b_result;
        }

        public static List<string> Transform_enum_VoiceName<T>()
        {
            List<string> result = new List<string>();
            Type enumType = typeof(T);
            if (enumType.IsEnum)
            {
                foreach (string name in Enum.GetNames(typeof(T)))
                    result.Add(name.Replace("_", "-"));
            }
            return result;
        }
    }
}
