using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using DeltaSubstrateInspector;
using System.Windows.Forms;
using System.Drawing;
using MicrosoftCognitiveServices;

namespace SpeechToText
{
    public class SpeechToTextApp
    {
        public static async Task<bool> MakeRequest_AutoDetectLanguage(string subscriptionKey, string region, RichTextBox richTextBox)
        {
            bool b_result = false;

            try
            {
                var config = SpeechConfig.FromSubscription(subscriptionKey, region);
                string[] languages = { "en-US", "zh-TW", "ja-JP", "ko-KR" };
                AutoDetectSourceLanguageConfig autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(languages);

                using (var recognizer = new SpeechRecognizer(config, autoDetectSourceLanguageConfig))
                {
                    var result = await recognizer.RecognizeOnceAsync();

                    if (result.Reason == ResultReason.RecognizedSpeech)
                    {
                        clsStaticTool.WriteMsg_RichTextBox_Invoke(richTextBox, "Recognized:" + "\n", Color.Blue, true);
                        clsStaticTool.WriteMsg_RichTextBox_Invoke(richTextBox, result.Text + "\n", Color.Green, true);
                        b_result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                b_result = false;
            }
            finally
            {
                richTextBox.BeginInvoke(new Action(() => richTextBox.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n")));
                richTextBox.BeginInvoke(new Action(() => richTextBox.ScrollToCaret()));
            }

            return b_result;
        }

        public static async Task<bool> MakeRequest_FromFile(string subscriptionKey, string region, RichTextBox richTextBox, string fileName)
        {
            bool b_result = false;

            try
            {
                var config = SpeechConfig.FromSubscription(subscriptionKey, region);

                using (var audioConfig = AudioConfig.FromWavFileInput(fileName))
                using (var recognizer = new SpeechRecognizer(config, audioConfig))
                {
                    var result = await recognizer.RecognizeOnceAsync();

                    if (result.Reason == ResultReason.RecognizedSpeech)
                    {
                        clsStaticTool.WriteMsg_RichTextBox_Invoke(richTextBox, "Recognized:" + "\n", Color.Blue, true);
                        clsStaticTool.WriteMsg_RichTextBox_Invoke(richTextBox, result.Text + "\n", Color.Green, true);
                        b_result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                b_result = false;
            }
            finally
            {
                richTextBox.BeginInvoke(new Action(() => richTextBox.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n")));
                richTextBox.BeginInvoke(new Action(() => richTextBox.ScrollToCaret()));
            }

            return b_result;
        }
    }
}
