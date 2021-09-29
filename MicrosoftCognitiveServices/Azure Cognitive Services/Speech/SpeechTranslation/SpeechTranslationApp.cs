using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;
using DeltaSubstrateInspector;
using System.Windows.Forms;
using System.Drawing;
using MicrosoftCognitiveServices;

namespace SpeechTranslation
{
    public class SpeechTranslationApp
    {
        public static Dictionary<string, string> DictFromLanguage = new Dictionary<string, string>()
        {
            { "English (United States)", "en-US"},
            { "English (United Kingdom)", "en-GB"},
            { "English (Australia)", "en-AU"},
            { "English (Canada)", "en-CA"},
            { "Chinese (Taiwanese Mandarin)", "zh-TW"},
            { "Chinese (Mandarin, Simplified)", "zh-CN"},
            { "Japanese (Japan)", "ja-JP"},
            { "Korean (Korea)", "ko-KR"},

            { "英文 (美國)", "en-US"},
            { "英文 (英國)", "en-GB"},
            { "英文 (澳洲)", "en-AU"},
            { "英文 (加拿大)", "en-CA"},
            { "中文 (台灣)", "zh-TW"},
            { "中文 (中國)", "zh-CN"},
            { "日文 (日本)", "ja-JP"},
            { "韓文 (韓國)", "ko-KR"},
        };

        public static Dictionary<string, string> DictTextLanguage = new Dictionary<string, string>()
        {
            { "English", "en"},
            { "Chinese Traditional", "zh-Hant"},
            { "Chinese Simplified", "zh-Hans"},
            { "Japanese", "ja"},
            { "Korean", "ko"},

            { "英文", "en"},
            { "繁體中文", "zh-Hant"},
            { "簡體中文", "zh-Hans"},
            { "日文", "ja"},
            { "韓文", "ko"},
        };

        public static async Task<bool> MakeRequest(string subscriptionKey, string region, clsProgressbar m_ProgressBar, RichTextBox richTextBox_SpeechTranslation, string fromLanguage = "en-US",
                                                   Dictionary<string, List<string>> DictVoiceName = null)
        {
            bool b_result = false;

            try
            {
                var translationConfig = SpeechTranslationConfig.FromSubscription(subscriptionKey, region);
                var config = SpeechConfig.FromSubscription(subscriptionKey, region);

                if (DictVoiceName == null)
                {
                    DictVoiceName = new Dictionary<string, List<string>>()
                    {
                        { "en", new List<string>() { "en-US-GuyRUS" } },
                        { "zh-Hant", new List<string>(){ "zh-TW-YunJheNeural", "zh-CN-YunyeNeural" } },
                        { "ja", new List<string>(){ "ja-JP-NanamiNeural" } },
                    };
                }
                List<string> toLanguages = new List<string>(DictVoiceName.Keys);

                translationConfig.SpeechRecognitionLanguage = fromLanguage;
                toLanguages.ForEach(translationConfig.AddTargetLanguage);

                using var recognizer = new TranslationRecognizer(translationConfig);

                //Console.Write($"Say something in '{fromLanguage}'");
                m_ProgressBar.SetShowText($"Say something in '{fromLanguage}'");
                clsStaticTool.WriteMsg_RichTextBox_Invoke(richTextBox_SpeechTranslation, $"Say something in '{fromLanguage}'" + "\n", Color.Blue, true);

                var result = await recognizer.RecognizeOnceAsync();
                if (result.Reason == ResultReason.TranslatedSpeech)
                {
                    //Console.WriteLine($"Recognized: \"{result.Text}\":");
                    m_ProgressBar.SetShowText($"Recognized: \"{result.Text}\":");
                    clsStaticTool.WriteMsg_RichTextBox_Invoke(richTextBox_SpeechTranslation, $"Recognized: \"{result.Text}\"" + "\n", Color.Green, true);

                    foreach (var (language, translation) in result.Translations)
                    {
                        //Console.WriteLine($"Translated into '{language}': {translation}");
                        m_ProgressBar.SetShowText($"Translated into '{language}': {translation}");
                        clsStaticTool.WriteMsg_RichTextBox_Invoke(richTextBox_SpeechTranslation, $"Translated into '{language}': {translation}" + "\n", Color.Green, true);

                        // Text To Speech
                        foreach (var voiceName in DictVoiceName[language])
                        {
                            m_ProgressBar.SetShowText($"Speaker: {voiceName}");
                            config.SpeechSynthesisVoiceName = voiceName;
                            using (var synthesizer = new SpeechSynthesizer(config))
                            {
                                await synthesizer.SpeakTextAsync(translation);
                            }
                        }
                    }
                    b_result = true;
                }
            }
            catch (Exception ex)
            {
                b_result = false;
            }
            finally
            {
                richTextBox_SpeechTranslation.BeginInvoke(new Action(() => richTextBox_SpeechTranslation.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n")));
                richTextBox_SpeechTranslation.BeginInvoke(new Action(() => richTextBox_SpeechTranslation.ScrollToCaret()));
            }

            return b_result;
        }
    }
}
