using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MicrosoftCognitiveServices.ConfigSetting;
using System.Threading;
using DeltaSubstrateInspector;
using QnAMaker;
using LUIS;

namespace MicrosoftCognitiveServices.Azure_Cognitive_Services.Language
{
    public partial class UC_Language : UserControl
    {
        private cls_ConfigSetting configSetting = new cls_ConfigSetting();

        public UC_Language()
        {
            InitializeComponent();

            #region 控制項顯示影像之背景顏色設定為透明

            clsStaticTool.Control_DispImg_Transparent(this.radioButton_QnAMaker, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_LUIS, false);

            #endregion
        }

        /// <summary>
        /// Set configSetting
        /// </summary>
        /// <param name="configSetting_"></param>
        public void Set_configSetting(cls_ConfigSetting configSetting_)
        {
            this.configSetting = configSetting_;
        }

        /// <summary>
        /// 語言切換後，補回前面空白
        /// </summary>
        public void LanguageSwitch_PadSpace()
        {
            this.radioButton_QnAMaker.Text = "        " + this.radioButton_QnAMaker.Text;
            this.radioButton_LUIS.Text = "        " + this.radioButton_LUIS.Text;
        }

        /// <summary>
        /// 【Search】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Search_Click(object sender, EventArgs e)
        {
            if (this.textBox_Message.Text == "")
            {
                string msg = this.radioButton_QnAMaker.Checked ? "Please type your message" : "Please type your utterance";
                MessageBox.Show(msg, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.radioButton_QnAMaker.Checked)
                this.Execute_QnAMaker();
            else
                this.Execute_LUIS();
        }

        /// <summary>
        /// 執行 【QnA Maker】
        /// </summary>
        private void Execute_QnAMaker()
        {
            clsProgressbar m_ProgressBar = new clsProgressbar();
            m_ProgressBar.FormClosedEvent2 += new clsProgressbar.FormClosedHandler2(SetFormClosed2);
            m_ProgressBar.SetShowText("Please wait for searching......");
            m_ProgressBar.SetShowCaption("Searching......");
            m_ProgressBar.ShowWaitProgress();

            Question question = new Question();
            question.QuestionStr = this.textBox_Message.Text;
            clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, "Question: \n\t", Color.Blue, false, true, question.QuestionStr + "\n", this.richTextBox_Answer.SelectionColor);
            clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, "Answer: \n", Color.Blue, true);
            //var result = QnAMakerApp.MakeRequest(url, endpoint_key, this.question).GetAwaiter().GetResult(); // Note: 程式會在await卡住!!!
            //var result = QnAMakerApp.MakeRequest(url, endpoint_key, this.question);

            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        var result = QnAMakerApp.MakeRequest(this.configSetting.ConfigQnAMaker.URL, this.configSetting.ConfigQnAMaker.EndpointKey, question).GetAwaiter().GetResult();
                        if (result != null)
                        {
                            if (this.richTextBox_Answer.InvokeRequired)
                            {
                                this.richTextBox_Answer.BeginInvoke(new Action(() => clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, result.Answers[0].AnswerStr + "\n", Color.Green)));
                                this.richTextBox_Answer.BeginInvoke(new Action(() => this.richTextBox_Answer.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n")));
                                this.richTextBox_Answer.BeginInvoke(new Action(() => this.richTextBox_Answer.ScrollToCaret()));
                            }
                            else
                            {
                                clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, result.Answers[0].AnswerStr + "\n", Color.Green);
                                this.richTextBox_Answer.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n");
                                this.richTextBox_Answer.ScrollToCaret();
                            }
                        }
                        else
                        {
                            string msg = "Error: Please ensure all settings are configured properly.";
                            if (this.richTextBox_Answer.InvokeRequired)
                            {
                                this.richTextBox_Answer.BeginInvoke(new Action(() => clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, msg + "\n", Color.Red)));
                                this.richTextBox_Answer.BeginInvoke(new Action(() => this.richTextBox_Answer.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n")));
                                this.richTextBox_Answer.BeginInvoke(new Action(() => this.richTextBox_Answer.ScrollToCaret()));
                            }
                            else
                            {
                                this.richTextBox_Answer.AppendText(msg + "\n");
                                this.richTextBox_Answer.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n");
                                this.richTextBox_Answer.ScrollToCaret();
                            }
                        }
                        m_ProgressBar.CloseProgress();
                    }
                ));

            // Start the background process thread
            backgroundThread.Start();
        }

        public void SetFormClosed2()
        {

        }

        /// <summary>
        /// 執行 【LUIS】
        /// </summary>
        private void Execute_LUIS()
        {
            clsProgressbar m_ProgressBar = new clsProgressbar();
            m_ProgressBar.FormClosedEvent2 += new clsProgressbar.FormClosedHandler2(SetFormClosed2);
            m_ProgressBar.SetShowText("Please wait for analyzing......");
            m_ProgressBar.SetShowCaption("Analyzing......");
            m_ProgressBar.ShowWaitProgress();

            string userstr = this.textBox_Message.Text;
            clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, "Utterance: \n\t", Color.Blue, false, true, userstr + "\n", this.richTextBox_Answer.SelectionColor);
            clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, "Predicted Result: \n", Color.Blue, true);

            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        var result = LUISApp.MakeRequest(this.configSetting.ConfigLUIS.AppID, this.configSetting.ConfigLUIS.Key, userstr, this.configSetting.ConfigLUIS.Location).GetAwaiter().GetResult();
                        if (result != null)
                        {
                            string PredictedResult = "\tIntent : " + result.TopScoringIntent.Intent + ", Score : " + result.TopScoringIntent.Score.ToString("0.##");
                            foreach (var item in result.Entities)
                            {
                                PredictedResult += "\n\tEntities : " + item.EntityItem;
                                PredictedResult += "\n\tEntities Type: " + item.Type;
                            }

                            if (this.richTextBox_Answer.InvokeRequired)
                            {
                                this.richTextBox_Answer.BeginInvoke(new Action(() => clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, PredictedResult + "\n", Color.Green)));
                                this.richTextBox_Answer.BeginInvoke(new Action(() => this.richTextBox_Answer.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n")));
                                this.richTextBox_Answer.BeginInvoke(new Action(() => this.richTextBox_Answer.ScrollToCaret()));
                            }
                            else
                            {
                                clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, PredictedResult + "\n", Color.Green);
                                this.richTextBox_Answer.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n");
                                this.richTextBox_Answer.ScrollToCaret();
                            }
                        }
                        else
                        {
                            string msg = "Error: Please ensure all settings are configured properly.";
                            if (this.richTextBox_Answer.InvokeRequired)
                            {
                                this.richTextBox_Answer.BeginInvoke(new Action(() => clsStaticTool.WriteMsg_RichTextBox(this.richTextBox_Answer, msg + "\n", Color.Red)));
                                this.richTextBox_Answer.BeginInvoke(new Action(() => this.richTextBox_Answer.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n")));
                                this.richTextBox_Answer.BeginInvoke(new Action(() => this.richTextBox_Answer.ScrollToCaret()));
                            }
                            else
                            {
                                this.richTextBox_Answer.AppendText(msg + "\n");
                                this.richTextBox_Answer.AppendText("----------   ----------   ----------   ----------   ----------   ----------   ----------   ----------" + "\n");
                                this.richTextBox_Answer.ScrollToCaret();
                            }
                        }
                        m_ProgressBar.CloseProgress();
                    }
                ));

            // Start the background process thread
            backgroundThread.Start();
        }

        /// <summary>
        /// 【Clear】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Clear_Click(object sender, EventArgs e)
        {
            this.richTextBox_Answer.Clear();
        }

        /// <summary>
        /// Service switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_Service_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton_QnAMaker.Checked)
            {
                if (FileSystem.FileSystem.Language == "English")
                    this.button_Search.Text = "Search";
                else
                    this.button_Search.Text = "搜尋";
                this.button_Search.Image = Properties.Resources.search_32;
            }
            else
            {
                if (FileSystem.FileSystem.Language == "English")
                    this.button_Search.Text = "Analyze";
                else
                    this.button_Search.Text = "分析";
                this.button_Search.Image = Properties.Resources.evaluation_32;
            }
        }
    }
}
