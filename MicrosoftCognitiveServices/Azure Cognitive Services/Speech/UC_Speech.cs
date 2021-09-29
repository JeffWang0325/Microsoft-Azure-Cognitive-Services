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
using SpeechToText;
using TextToSpeech;
using SpeechTranslation;

namespace MicrosoftCognitiveServices.Azure_Cognitive_Services.Speech
{
    public partial class UC_Speech : UserControl
    {
        private cls_ConfigSetting configSetting = new cls_ConfigSetting();

        private TabControl HidePage { get; set; } = new TabControl();

        public UC_Speech()
        {
            InitializeComponent();

            #region 控制項顯示影像之背景顏色設定為透明

            clsStaticTool.Control_DispImg_Transparent(this.radioButton_SpeechToText, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_TextToSpeech, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_SpeechTranslation, false);

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

        private void UC_Speech_Load(object sender, EventArgs e)
        {
            this.Update_tabControl_Operation();

            #region Set Default

            //【文字轉換語音】
            this.textBox_TextToSpeech_OutputPath.Text = Application.StartupPath + "\\text-to-speech.wav";
            this.comboBox_TextToSpeech_Language.SelectedIndex = 0;

            //【語音翻譯】
            this.comboBox_SpeechTranslation_FromLanguage.SelectedIndex = 0;
            this.comboBox_SpeechTranslation_TextLanguage.SelectedIndex = 0;
            this.comboBox_SpeechTranslation_Language.SelectedIndex = 0;

            #endregion
        }

        /// <summary>
        /// 語言切換前，復原TabControl所有TabPages，以讓TabPages內所有元件做語言切換
        /// </summary>
        public void LanguageSwitch_tabControl()
        {
            foreach (TabPage tp in this.HidePage.TabPages)
                this.tabControl_Operation.TabPages.Add(tp);
        }

        /// <summary>
        /// 語言切換後，補回前面空白，並且更新TabControl顯示頁面
        /// </summary>
        public void LanguageSwitch_PadSpace()
        {
            this.radioButton_SpeechToText.Text = "        " + this.radioButton_SpeechToText.Text;
            this.radioButton_TextToSpeech.Text = "        " + this.radioButton_TextToSpeech.Text;
            this.radioButton_SpeechTranslation.Text = "        " + this.radioButton_SpeechTranslation.Text;

            // 更新TabControl顯示頁面
            this.Update_tabControl_Operation();
        }

        /// <summary>
        /// 更新TabControl顯示頁面
        /// </summary>
        /// <param name="tag"></param>
        private void Update_tabControl_Operation(string tag = null)
        {
            if (tag == null)
            {
                if (this.radioButton_SpeechToText.Checked)
                    tag = this.radioButton_SpeechToText.Tag.ToString();
                else if (this.radioButton_TextToSpeech.Checked)
                    tag = this.radioButton_TextToSpeech.Tag.ToString();
                else if (this.radioButton_SpeechTranslation.Checked)
                    tag = this.radioButton_SpeechTranslation.Tag.ToString();
            }
            foreach (TabPage tp in this.tabControl_Operation.TabPages)
                this.HidePage.TabPages.Add(tp);
            foreach (TabPage tp in this.HidePage.TabPages)
            {
                if (tp.Tag.ToString() == tag)
                {
                    this.tabControl_Operation.TabPages.Add(tp);
                    break;
                }
            }
        }

        /// <summary>
        /// Service switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbt_Service_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbt = sender as RadioButton;
            if (rbt.Checked == false)
                return;

            this.Update_tabControl_Operation(rbt.Tag.ToString());
        }

        public void SetFormClosed2()
        {

        }

        #region 【文字轉換語音】

        /// <summary>
        /// 是否啟用【語音檔輸出(.wav)】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_TextToSpeech_OutputPath_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = sender as CheckBox;
            this.textBox_TextToSpeech_OutputPath.Enabled = cbx.Checked;
            if (cbx.Checked) // ON
            {
                cbx.BackgroundImage = Properties.Resources.ON;
                this.lbl_TextToSpeech_OutputPath.Text = "ON";
                this.lbl_TextToSpeech_OutputPath.ForeColor = Color.DeepSkyBlue;

                if (FileSystem.FileSystem.Language == "English")
                    this.button_TextToSpeech_Play.Text = "Output";
                else
                    this.button_TextToSpeech_Play.Text = "輸出";
                this.button_TextToSpeech_Play.Image = Properties.Resources.wav_file_64;
            }
            else // OFF
            {
                cbx.BackgroundImage = Properties.Resources.OFF_edited;
                this.lbl_TextToSpeech_OutputPath.Text = "OFF";
                this.lbl_TextToSpeech_OutputPath.ForeColor = System.Drawing.SystemColors.ControlDarkDark;

                if (FileSystem.FileSystem.Language == "English")
                    this.button_TextToSpeech_Play.Text = "Play";
                else
                    this.button_TextToSpeech_Play.Text = "播放";
                this.button_TextToSpeech_Play.Image = Properties.Resources.play_button_64;
            }
        }

        /// <summary>
        /// 設定【語音檔輸出(.wav)】路徑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TextToSpeech_OutputPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = this.textBox_TextToSpeech_OutputPath.Text;
            saveFileDialog.Filter = "WAV|*.wav";
            saveFileDialog.Title = "Set Speech File Output Path";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            this.textBox_TextToSpeech_OutputPath.Text = saveFileDialog.FileName;
        }

        /// <summary>
        /// 【播放】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_TextToSpeech_Play_Click(object sender, EventArgs e)
        {
            if (this.textBox_TextToSpeech_TextInput.Text == "")
            {
                MessageBox.Show("Please type your text", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string voiceName = this.comboBox_TextToSpeech_VoiceName.Text;
            if (this.cbx_TextToSpeech_OutputPath.Checked)
                this.Execute_TextToSpeech_SaveAs(voiceName);
            else
                this.Execute_TextToSpeech(voiceName);
        }

        /// <summary>
        /// 執行 【文字轉換語音】
        /// </summary>
        private void Execute_TextToSpeech(string voiceName)
        {
            clsProgressbar m_ProgressBar = new clsProgressbar();
            m_ProgressBar.FormClosedEvent2 += new clsProgressbar.FormClosedHandler2(SetFormClosed2);
            m_ProgressBar.SetShowText("Please wait for playing......");
            m_ProgressBar.SetShowCaption("Playing......");
            m_ProgressBar.ShowWaitProgress();

            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        bool b_result = TextToSpeechApp.MakeRequest(this.configSetting.ConfigSpeech.Key, this.configSetting.ConfigSpeech.Location, this.textBox_TextToSpeech_TextInput.Text, voiceName).GetAwaiter().GetResult();
                        if (b_result == false)
                        {
                            string msg = "Error: Please ensure all settings are configured properly.";
                            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        m_ProgressBar.CloseProgress();
                    }
                ));

            // Start the background process thread
            backgroundThread.Start();
        }

        /// <summary>
        /// 執行 【文字轉換語音】(語音檔輸出)
        /// </summary>
        private void Execute_TextToSpeech_SaveAs(string voiceName)
        {
            clsProgressbar m_ProgressBar = new clsProgressbar();
            m_ProgressBar.FormClosedEvent2 += new clsProgressbar.FormClosedHandler2(SetFormClosed2);
            m_ProgressBar.SetShowText("Please wait for saving......");
            m_ProgressBar.SetShowCaption("Saving......");
            m_ProgressBar.ShowWaitProgress();

            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        bool b_result = TextToSpeechApp.MakeRequest_SaveAs(this.configSetting.ConfigSpeech.Key, this.configSetting.ConfigSpeech.Location, this.textBox_TextToSpeech_TextInput.Text,
                                                                           this.textBox_TextToSpeech_OutputPath.Text, voiceName).GetAwaiter().GetResult();
                        if (b_result == false)
                        {
                            string msg = "Error: Please ensure all settings are configured properly.";
                            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        m_ProgressBar.CloseProgress();
                    }
                ));

            // Start the background process thread
            backgroundThread.Start();
        }

        /// <summary>
        /// 【語言】選擇
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_TextToSpeech_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Update_comboBox_TextToSpeech_VoiceName();
            this.Update_comboBox_VoiceName(this.comboBox_TextToSpeech_Language, this.comboBox_TextToSpeech_Gender, this.comboBox_TextToSpeech_VoiceName);
        }

        /// <summary>
        /// 【性別】選擇
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_TextToSpeech_Gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Update_comboBox_TextToSpeech_VoiceName();
            this.Update_comboBox_VoiceName(this.comboBox_TextToSpeech_Language, this.comboBox_TextToSpeech_Gender, this.comboBox_TextToSpeech_VoiceName);
        }

        /// <summary>
        /// 更新【語音名稱】
        /// </summary>
        private void Update_comboBox_TextToSpeech_VoiceName()
        {
            if (this.comboBox_TextToSpeech_Language.SelectedIndex < 0)
                this.comboBox_TextToSpeech_Language.SelectedIndex = 0;
            if (this.comboBox_TextToSpeech_Gender.SelectedIndex < 0)
                this.comboBox_TextToSpeech_Gender.SelectedIndex = 0;

            this.comboBox_TextToSpeech_VoiceName.Items.Clear();
            List<string> listVoiceName = new List<string>();
            switch (this.comboBox_TextToSpeech_Language.SelectedIndex)
            {
                case 0: // 英文
                    {
                        if (this.comboBox_TextToSpeech_Gender.SelectedIndex == 0) // 女性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_en_Female>();
                        else // 男性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_en_Male>();
                    }
                    break;

                case 1: // 中文
                    {
                        if (this.comboBox_TextToSpeech_Gender.SelectedIndex == 0) // 女性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_zh_Female>();
                        else // 男性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_zh_Male>();
                    }
                    break;

                case 2: // 日文
                    {
                        if (this.comboBox_TextToSpeech_Gender.SelectedIndex == 0) // 女性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_ja_Female>();
                        else // 男性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_ja_Male>();
                    }
                    break;

                case 3: // 韓文
                    {
                        if (this.comboBox_TextToSpeech_Gender.SelectedIndex == 0) // 女性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_ko_Female>();
                        else // 男性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_ko_Male>();
                    }
                    break;
            }

            this.comboBox_TextToSpeech_VoiceName.Items.AddRange(listVoiceName.ToArray());
            this.comboBox_TextToSpeech_VoiceName.SelectedIndex = 0;
        }

        /// <summary>
        /// 更新【語音名稱】
        /// </summary>
        private void Update_comboBox_VoiceName(ComboBox cbx_Language, ComboBox cbx_Gender, ComboBox cbx_VoiceName)
        {
            if (cbx_Language.SelectedIndex < 0)
                cbx_Language.SelectedIndex = 0;
            if (cbx_Gender.SelectedIndex < 0)
                cbx_Gender.SelectedIndex = 0;

            cbx_VoiceName.Items.Clear();
            List<string> listVoiceName = new List<string>();
            switch (cbx_Language.SelectedIndex)
            {
                case 0: // 英文
                    {
                        if (cbx_Gender.SelectedIndex == 0) // 女性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_en_Female>();
                        else // 男性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_en_Male>();
                    }
                    break;

                case 1: // 中文
                    {
                        if (cbx_Gender.SelectedIndex == 0) // 女性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_zh_Female>();
                        else // 男性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_zh_Male>();
                    }
                    break;

                case 2: // 日文
                    {
                        if (cbx_Gender.SelectedIndex == 0) // 女性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_ja_Female>();
                        else // 男性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_ja_Male>();
                    }
                    break;

                case 3: // 韓文
                    {
                        if (cbx_Gender.SelectedIndex == 0) // 女性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_ko_Female>();
                        else // 男性
                            listVoiceName = TextToSpeechApp.Transform_enum_VoiceName<VoiceName_ko_Male>();
                    }
                    break;
            }

            cbx_VoiceName.Items.AddRange(listVoiceName.ToArray());
            cbx_VoiceName.SelectedIndex = 0;
        }

        #endregion

        #region 【語音翻譯】

        Dictionary<string, List<string[]>> Dict_TextLanguage_VoiceName { get; set; } = new Dictionary<string, List<string[]>>();

        /// <summary>
        /// 【說話】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SpeechTranslation_Speak_Click(object sender, EventArgs e)
        {
            if (this.Dict_TextLanguage_VoiceName.Count <= 0)
            {
                MessageBox.Show("Please edit your settings", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Execute_SpeechTranslation();
        }

        /// <summary>
        /// 【清空】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SpeechTranslation_ClearText_Click(object sender, EventArgs e)
        {
            this.richTextBox_SpeechTranslation.Clear();
        }

        /// <summary>
        /// 執行 【語音翻譯】
        /// </summary>
        private void Execute_SpeechTranslation()
        {
            string fromLanguage = SpeechTranslationApp.DictFromLanguage[this.comboBox_SpeechTranslation_FromLanguage.Text];
            Dictionary<string, List<string>> DictVoiceName = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, List<string[]>> item in this.Dict_TextLanguage_VoiceName)
            {
                string key = SpeechTranslationApp.DictTextLanguage[item.Key];
                List<string> value = new List<string>();
                foreach (string[] voiceName in item.Value)
                    value.Add(voiceName[2]);
                DictVoiceName.Add(key, value);
            }

            clsProgressbar m_ProgressBar = new clsProgressbar();
            m_ProgressBar.FormClosedEvent2 += new clsProgressbar.FormClosedHandler2(SetFormClosed2);
            m_ProgressBar.SetShowText("Please start speaking......");
            m_ProgressBar.SetShowCaption("Recognizing......");
            m_ProgressBar.ShowWaitProgress();

            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        bool b_result = SpeechTranslationApp.MakeRequest(this.configSetting.ConfigSpeech.Key, this.configSetting.ConfigSpeech.Location,
                                                                         m_ProgressBar, this.richTextBox_SpeechTranslation,
                                                                         fromLanguage, DictVoiceName).GetAwaiter().GetResult();
                        if (b_result == false)
                        {
                            string msg = "Error: Please ensure all settings are configured properly.";
                            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        m_ProgressBar.CloseProgress();
                    }
                ));

            // Start the background process thread
            backgroundThread.Start();
        }

        /// <summary>
        /// 【語言】選擇
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SpeechTranslation_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Update_comboBox_VoiceName(this.comboBox_SpeechTranslation_Language, this.comboBox_SpeechTranslation_Gender, this.comboBox_SpeechTranslation_VoiceName);
        }

        /// <summary>
        /// 【性別】選擇
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SpeechTranslation_Gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Update_comboBox_VoiceName(this.comboBox_SpeechTranslation_Language, this.comboBox_SpeechTranslation_Gender, this.comboBox_SpeechTranslation_VoiceName);
        }

        /// <summary>
        /// 更新 listView_SpeechTranslation 顯示
        /// </summary>
        private void Update_listView_SpeechTranslation()
        {
            this.listView_SpeechTranslation.ForeColor = Color.DarkViolet;

            this.listView_SpeechTranslation.BeginUpdate();
            this.listView_SpeechTranslation.Items.Clear();

            foreach (KeyValuePair<string, List<string[]>> item in this.Dict_TextLanguage_VoiceName)
            {
                foreach (string[] voiceName in item.Value)
                {
                    ListViewItem lvi = new ListViewItem(item.Key);
                    foreach (string s in voiceName)
                        lvi.SubItems.Add(s);

                    this.listView_SpeechTranslation.Items.Add(lvi);
                }
            }

            this.listView_SpeechTranslation.EndUpdate();
        }

        /// <summary>
        /// 【新增】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SpeechTranslation_Add_Click(object sender, EventArgs e)
        {
            string[] voiceName = new string[] { this.comboBox_SpeechTranslation_Language.Text, this.comboBox_SpeechTranslation_Gender.Text, this.comboBox_SpeechTranslation_VoiceName.Text };
            string key = this.comboBox_SpeechTranslation_TextLanguage.Text;
            if (this.Dict_TextLanguage_VoiceName.ContainsKey(key))
                this.Dict_TextLanguage_VoiceName[key].Add(voiceName);
            else
                this.Dict_TextLanguage_VoiceName.Add(key, new List<string[]>() { voiceName });

            // 更新 listView_SpeechTranslation 顯示
            this.Update_listView_SpeechTranslation();
        }

        /// <summary>
        /// 【刪除】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SpeechTranslation_Remove_Click(object sender, EventArgs e)
        {
            if (this.listView_SpeechTranslation.SelectedIndices.Count <= 0)
            {
                MessageBox.Show("Please choose one item", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListViewItem lvi = this.listView_SpeechTranslation.SelectedItems[0];
            string key = lvi.Text;
            if (this.Dict_TextLanguage_VoiceName[key].Count <= 1)
                this.Dict_TextLanguage_VoiceName.Remove(key);
            else
            {
                string[] voiceName = new string[] { lvi.SubItems[1].Text, lvi.SubItems[2].Text, lvi.SubItems[3].Text };
                // Method 1: Fail!
                //this.Dict_TextLanguage_VoiceName[key].Remove(voiceName);
                // Method 2: Fail!
                //int index = this.Dict_TextLanguage_VoiceName[key].IndexOf(voiceName);
                // Method 3: Succeed!
                int index = -1;
                List<string[]> listVoiceName = this.Dict_TextLanguage_VoiceName[key];
                for (int i = 0; i < listVoiceName.Count; i++)
                {
                    if (listVoiceName[i][0] == voiceName[0] && listVoiceName[i][1] == voiceName[1] && listVoiceName[i][2] == voiceName[2])
                    {
                        index = i;
                        break;
                    }
                }
                this.Dict_TextLanguage_VoiceName[key].RemoveAt(index);
            }

            // 更新 listView_SpeechTranslation 顯示
            this.Update_listView_SpeechTranslation();
        }

        /// <summary>
        /// 【清空】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SpeechTranslation_Clear_Click(object sender, EventArgs e)
        {
            this.Dict_TextLanguage_VoiceName.Clear();

            // 更新 listView_SpeechTranslation 顯示
            this.Update_listView_SpeechTranslation();
        }

        #endregion

        #region 【語音轉換文字】

        /// <summary>
        /// 【說話】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SpeechToText_Speak_Click(object sender, EventArgs e)
        {
            if (this.cbx_SpeechToText_InputPath.Checked)
            {
                if (this.textBox_SpeechToText_InputPath.Text == "")
                {
                    MessageBox.Show("Please choose speech input file", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.Execute_SpeechToText_FromFile();
            }
            else
                this.Execute_SpeechToText_AutoDetectLanguage();
        }

        /// <summary>
        /// 【清空】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SpeechToText_Clear_Click(object sender, EventArgs e)
        {
            this.richTextBox_SpeechToText.Clear();
        }

        /// <summary>
        /// 執行 【語音轉換文字】
        /// </summary>
        private void Execute_SpeechToText_AutoDetectLanguage()
        {
            clsProgressbar m_ProgressBar = new clsProgressbar();
            m_ProgressBar.FormClosedEvent2 += new clsProgressbar.FormClosedHandler2(SetFormClosed2);
            m_ProgressBar.SetShowText("Please start speaking......");
            m_ProgressBar.SetShowCaption("Recognizing......");
            m_ProgressBar.ShowWaitProgress();

            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        bool b_result = SpeechToTextApp.MakeRequest_AutoDetectLanguage(this.configSetting.ConfigSpeech.Key, this.configSetting.ConfigSpeech.Location, this.richTextBox_SpeechToText).GetAwaiter().GetResult();
                        if (b_result == false)
                        {
                            string msg = "Error: Please ensure all settings are configured properly.";
                            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        m_ProgressBar.CloseProgress();
                    }
                ));

            // Start the background process thread
            backgroundThread.Start();
        }

        /// <summary>
        /// 執行 【語音轉換文字】(【語音檔輸入(.wav)】)
        /// </summary>
        private void Execute_SpeechToText_FromFile()
        {
            string fileName = this.textBox_SpeechToText_InputPath.Text;

            clsProgressbar m_ProgressBar = new clsProgressbar();
            m_ProgressBar.FormClosedEvent2 += new clsProgressbar.FormClosedHandler2(SetFormClosed2);
            m_ProgressBar.SetShowText("Please wait recognizing......");
            m_ProgressBar.SetShowCaption("Recognizing......");
            m_ProgressBar.ShowWaitProgress();

            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        bool b_result = SpeechToTextApp.MakeRequest_FromFile(this.configSetting.ConfigSpeech.Key, this.configSetting.ConfigSpeech.Location, this.richTextBox_SpeechToText, fileName).GetAwaiter().GetResult();
                        if (b_result == false)
                        {
                            string msg = "Error: Please ensure all settings are configured properly.";
                            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        m_ProgressBar.CloseProgress();
                    }
                ));

            // Start the background process thread
            backgroundThread.Start();
        }

        /// <summary>
        /// 設定【語音檔輸入(.wav)】路徑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_SpeechToText_InputPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDilg = new OpenFileDialog();
            OpenFileDilg.FileName = this.textBox_SpeechToText_InputPath.Text;
            OpenFileDilg.Filter = "WAV|*.wav";
            OpenFileDilg.Title = "Set Load File Path";
            if (OpenFileDilg.ShowDialog() != DialogResult.OK)
                return;
            this.textBox_SpeechToText_InputPath.Text = OpenFileDilg.FileName;
        }

        /// <summary>
        /// 是否啟用【語音檔輸入(.wav)】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_SpeechToText_InputPath_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = sender as CheckBox;
            this.textBox_SpeechToText_InputPath.Enabled = cbx.Checked;
            if (cbx.Checked) // ON
            {
                cbx.BackgroundImage = Properties.Resources.ON;
                this.lbl_SpeechToText_InputPath.Text = "ON";
                this.lbl_SpeechToText_InputPath.ForeColor = Color.DeepSkyBlue;

                if (FileSystem.FileSystem.Language == "English")
                    this.button_SpeechToText_Speak.Text = "Execute";
                else
                    this.button_SpeechToText_Speak.Text = "執行";
            }
            else // OFF
            {
                cbx.BackgroundImage = Properties.Resources.OFF_edited;
                this.lbl_SpeechToText_InputPath.Text = "OFF";
                this.lbl_SpeechToText_InputPath.ForeColor = System.Drawing.SystemColors.ControlDarkDark;

                if (FileSystem.FileSystem.Language == "English")
                    this.button_SpeechToText_Speak.Text = "Speak";
                else
                    this.button_SpeechToText_Speak.Text = "說話";
            }
        }

        #endregion
    }
}
