using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MicrosoftCognitiveServices.Azure_Cognitive_Services.Language;
using MicrosoftCognitiveServices.Azure_Cognitive_Services.Vision;
using MicrosoftCognitiveServices.Azure_Cognitive_Services.Speech;

using System.Threading;
using DeltaSubstrateInspector;
using MicrosoftCognitiveServices.ConfigSetting;
using clsLanguage;
using static FileSystem.FileSystem;

namespace MicrosoftCognitiveServices
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 各Service對應之Panel
        /// </summary>
        private Dictionary<RadioButton, Panel> DictPanel_Services { get; set; }

        private UC_Language uc_Language { get; set; } = new UC_Language();
        private UC_Vision uc_Vision { get; set; } = new UC_Vision();
        private UC_Speech uc_Speech { get; set; } = new UC_Speech();

        private cls_ConfigSetting configSetting = new cls_ConfigSetting();

        public Form1()
        {
            InitializeComponent();

            #region 控制項顯示影像之背景顏色設定為透明

            clsStaticTool.Control_DispImg_Transparent(this.radioButton_Language, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_Vision, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_Speech, false);

            #endregion

            #region GUI Settings

            this.DictPanel_Services = new Dictionary<RadioButton, Panel>()
            {
                {this.radioButton_Language, this.panel_Language },
                {this.radioButton_Vision, this.panel_Vision },
                {this.radioButton_Speech, this.panel_Speech }
            };

            this.panel_Language.Controls.Add(this.uc_Language);
            this.panel_Vision.Controls.Add(this.uc_Vision);
            this.panel_Speech.Controls.Add(this.uc_Speech);

            #endregion

            #region 語言切換

            Language = clsLanguage.clsLanguage.GetLanguage();
            if (Language == "Chinese")
                this.toolStripMenuItem_Chinese_Click(null, null);
            else if (Language == "English")
                this.toolStripMenuItem_English_Click(null, null);

            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (cls_ConfigSetting.Load(out this.configSetting) == false)
                this.configSetting.Save();
            this.Update_GUI_configSetting();
        }

        /// <summary>
        /// Update configSetting of all GUIs (UserControl)
        /// </summary>
        private void Update_GUI_configSetting()
        {
            this.uc_Language.Set_configSetting(this.configSetting);
            this.uc_Vision.Set_configSetting(this.configSetting);
            this.uc_Speech.Set_configSetting(this.configSetting);
        }

        /// <summary>
        /// Service switch and update the GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_Service_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbt = sender as RadioButton;
            if (rbt.Checked == false)
                return;

            foreach (KeyValuePair<RadioButton, Panel> item in this.DictPanel_Services)
                item.Value.Visible = (item.Key == rbt) ? true : false;
        }

        /// <summary>
        /// 【Exit】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 【Config Setting】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_ConfigSetting_Click(object sender, EventArgs e)
        {
            using (SaveSetting_Form f = new SaveSetting_Form())
            {
                f.Set_saveSetting(this.configSetting);
                //f.Location = new Point(0, 0);
                f.StartPosition = FormStartPosition.CenterScreen;
                if (f.ShowDialog() == DialogResult.Yes) //【Save】
                {
                    //this.Update_GUI_configSetting();
                }
            }
        }

        #region 語言切換

        /// <summary>
        /// 轉換語言 (任何語言轉中文)
        /// </summary>
        private void ResetLanguage()
        {
            clsLanguage.clsLanguage.UpdateRestoreLanguageLib();

            clsLanguage.clsLanguage.SetLanguateToControls(this, true);

            clsLanguage.clsLanguage.RefreshLib();
        }

        /// <summary>
        /// 轉換語言 (任何語言轉其他語言)
        /// </summary>
        private void ChangeLanguage()
        {
            clsLanguage.clsLanguage.UpdateLanguageLib();

            clsLanguage.clsLanguage.SetLanguateToControls(this, true);

            clsLanguage.clsLanguage.RefreshLib();
        }

        /// <summary>
        /// 【中文】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_Chinese_Click(object sender, EventArgs e)
        {
            this.toolStripMenuItem_English.Checked = false;
            this.toolStripMenuItem_Chinese.Checked = true;
            Language = "Chinese";

            // 語言切換前，復原TabControl所有TabPages，以讓TabPages內所有元件做語言切換
            if (sender != null)
                this.LanguageSwitch_tabControl();

            /* 特殊字元先刪除 (因為無法存入INI檔) */
            if (this.toolStripMenuItem_Quit.Text == "結束(✖)")
                this.toolStripMenuItem_Quit.Text = this.toolStripMenuItem_Quit.Text.Substring(0, 2);
            else if (this.toolStripMenuItem_Quit.Text == "Quit(✖)")
                this.toolStripMenuItem_Quit.Text = this.toolStripMenuItem_Quit.Text.Substring(0, 4);

            clsIniFile iniSystem = new clsIniFile(clsData.g_strSystemIniFilePath); // clsData.g_strSystemIniFilePath = Application.StartupPath + "\\INI\\System.ini"
            this.ResetLanguage();
            iniSystem.WriteValue("System", "Language", "Chinese"); // 儲存目前使用之語言種類

            /* 特殊字元補回 (因為無法存入INI檔) */
            this.toolStripMenuItem_Quit.Text += "(✖)";

            // 語言切換後，補回前面空白，並且更新TabControl顯示頁面 (Note: 如果一開始是中文，則不用!)
            if (sender != null)
                this.LanguageSwitch_PadSpace();
        }

        /// <summary>
        /// 【English】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_English_Click(object sender, EventArgs e)
        {
            this.toolStripMenuItem_English.Checked = true;
            this.toolStripMenuItem_Chinese.Checked = false;
            Language = "English";

            // 語言切換前，復原TabControl所有TabPages，以讓TabPages內所有元件做語言切換
            if (sender != null)
                this.LanguageSwitch_tabControl();

            /* 特殊字元先刪除 (因為無法存入INI檔) */
            if (this.toolStripMenuItem_Quit.Text == "結束(✖)")
                this.toolStripMenuItem_Quit.Text = this.toolStripMenuItem_Quit.Text.Substring(0, 2);
            else if (this.toolStripMenuItem_Quit.Text == "Quit(✖)")
                this.toolStripMenuItem_Quit.Text = this.toolStripMenuItem_Quit.Text.Substring(0, 4);

            clsIniFile iniSystem = new clsIniFile(clsData.g_strSystemIniFilePath); // clsData.g_strSystemIniFilePath = Application.StartupPath + "\\INI\\System.ini"
            this.ResetLanguage();
            this.ChangeLanguage();
            iniSystem.WriteValue("System", "Language", "English"); // 儲存目前使用之語言種類

            /* 特殊字元補回 (因為無法存入INI檔) */
            this.toolStripMenuItem_Quit.Text += "(✖)";

            // 語言切換後，補回前面空白，並且更新TabControl顯示頁面
            this.LanguageSwitch_PadSpace();
        }

        /// <summary>
        /// 語言切換前，復原TabControl所有TabPages，以讓TabPages內所有元件做語言切換
        /// </summary>
        private void LanguageSwitch_tabControl()
        {
            this.uc_Vision.LanguageSwitch_tabControl();
            this.uc_Speech.LanguageSwitch_tabControl();
        }

        /// <summary>
        /// 語言切換後，補回前面空白，並且更新TabControl顯示頁面
        /// </summary>
        private void LanguageSwitch_PadSpace()
        {
            this.radioButton_Language.Text = "               " + this.radioButton_Language.Text;
            this.radioButton_Vision.Text = "               " + this.radioButton_Vision.Text;
            this.radioButton_Speech.Text = "               " + this.radioButton_Speech.Text;
            this.uc_Language.LanguageSwitch_PadSpace();
            this.uc_Vision.LanguageSwitch_PadSpace();
            this.uc_Speech.LanguageSwitch_PadSpace();
        }

        #endregion
    }
}
