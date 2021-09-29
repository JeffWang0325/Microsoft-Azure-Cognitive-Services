using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Media; // For SystemSounds

namespace MicrosoftCognitiveServices.ConfigSetting
{
    public partial class SaveSetting_Form : Form
    {
        private Dictionary<string, Label> Dictionary_Label { get; set; } = new Dictionary<string, Label>();

        /// <summary>
        /// ON 時，控制項啟用
        /// </summary>
        private Dictionary<string, List<Control>> Dict_ON_Enabled { get; set; } = new Dictionary<string, List<Control>>();

        private cls_ConfigSetting saveSetting { get; set; } = new cls_ConfigSetting();
        
        public SaveSetting_Form()
        {
            InitializeComponent();

            #region 控制項顯示影像之背景顏色設定為透明

            clsStaticTool.Control_DispImg_Transparent(this.radioButton_Language, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_Vision, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_Speech, false);
            clsStaticTool.Control_DispImg_Transparent(this.pictureBox_QnAMaker, false);
            clsStaticTool.Control_DispImg_Transparent(this.pictureBox_LUIS, false);
            clsStaticTool.Control_DispImg_Transparent(this.pictureBox_ComputerVision, false);
            clsStaticTool.Control_DispImg_Transparent(this.pictureBox_CustomVision, false);
            clsStaticTool.Control_DispImg_Transparent(this.pictureBox_Face, false);
            clsStaticTool.Control_DispImg_Transparent(this.pictureBox_FormRecognizer, false);
            clsStaticTool.Control_DispImg_Transparent(this.groupBox_Speech, false);

            #endregion

            this.Dictionary_Label.Add(this.cbx_QnAMaker.Tag.ToString(), this.lbl_QnAMaker);
            this.Dictionary_Label.Add(this.cbx_LUIS.Tag.ToString(), this.lbl_LUIS);
            this.Dictionary_Label.Add(this.cbx_ComputerVision.Tag.ToString(), this.lbl_ComputerVision);
            this.Dictionary_Label.Add(this.cbx_CustomVision.Tag.ToString(), this.lbl_CustomVision);
            this.Dictionary_Label.Add(this.cbx_Face.Tag.ToString(), this.lbl_Face);
            this.Dictionary_Label.Add(this.cbx_FormRecognizer.Tag.ToString(), this.lbl_FormRecognizer);
            this.Dictionary_Label.Add(this.cbx_Speech.Tag.ToString(), this.lbl_Speech);

            this.Dict_ON_Enabled.Add(this.cbx_QnAMaker.Tag.ToString(), new List<Control>() { this.panel_QnAMaker});
            this.Dict_ON_Enabled.Add(this.cbx_LUIS.Tag.ToString(), new List<Control>() { this.panel_LUIS });
            this.Dict_ON_Enabled.Add(this.cbx_ComputerVision.Tag.ToString(), new List<Control>() { this.panel_ComputerVision });
            this.Dict_ON_Enabled.Add(this.cbx_CustomVision.Tag.ToString(), new List<Control>() { this.panel_CustomVision });
            this.Dict_ON_Enabled.Add(this.cbx_Face.Tag.ToString(), new List<Control>() { this.panel_Face });
            this.Dict_ON_Enabled.Add(this.cbx_FormRecognizer.Tag.ToString(), new List<Control>() { this.panel_FormRecognizer });
            this.Dict_ON_Enabled.Add(this.cbx_Speech.Tag.ToString(), new List<Control>() { this.panel_Speech });

            clsLanguage.clsLanguage.SetLanguateToControls(this, true, "false");
        }

        public void Set_saveSetting(cls_ConfigSetting saveSetting_)
        {
            this.saveSetting = saveSetting_;
        }

        private void SaveSetting_Form_Load(object sender, EventArgs e)
        {
            #region 更新GUI參數

            this.ui_parameters(false);

            #endregion

            // 語言切換後，補回前面空白
            if (this.radioButton_Language.Text.Substring(0, 1) != " ")
                this.LanguageSwitch_PadSpace();
        }

        /// <summary>
        /// 語言切換後，補回前面空白
        /// </summary>
        private void LanguageSwitch_PadSpace()
        {
            this.radioButton_Language.Text = "               " + this.radioButton_Language.Text;
            this.radioButton_Vision.Text = "               " + this.radioButton_Vision.Text;
            this.radioButton_Speech.Text = "               " + this.radioButton_Speech.Text;
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

            if (this.radioButton_Language.Checked)
                this.tabControl_Services.SelectedIndex = 0;
            else if (this.radioButton_Vision.Checked)
                this.tabControl_Services.SelectedIndex = 1;
            else if (this.radioButton_Speech.Checked)
                this.tabControl_Services.SelectedIndex = 2;
        }

        /// <summary>
        /// Service switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_Services_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabControl_Services.SelectedIndex)
            {
                case 0:
                    this.radioButton_Language.Checked = true;
                    break;
                case 1:
                    this.radioButton_Vision.Checked = true;
                    break;
                case 2:
                    this.radioButton_Speech.Checked = true;
                    break;
            }
        }

        /// <summary>
        /// 將 GUI參數 與 saveSetting參數 互傳
        /// </summary>
        /// <param name="ui_2_parameters_">True: UI傳至saveSetting, False: saveSetting傳至UI</param>
        /// <param name="saveSetting_"></param>
        /// <returns></returns>
        private bool ui_parameters(bool ui_2_parameters_, cls_ConfigSetting saveSetting_ = null)
        {
            bool b_status_ = false;
            if (saveSetting_ == null)
                saveSetting_ = this.saveSetting;

            try
            {
                if (ui_2_parameters_)
                {
                    #region 將UI內容回傳至saveSetting_

                    saveSetting_.ConfigQnAMaker.Enabled = this.cbx_QnAMaker.Checked;
                    saveSetting_.ConfigQnAMaker.URL = this.textBox_QnAMaker_URL.Text;
                    saveSetting_.ConfigQnAMaker.EndpointKey = this.textBox_QnAMaker_EndpointKey.Text;

                    saveSetting_.ConfigLUIS.Enabled = this.cbx_LUIS.Checked;
                    saveSetting_.ConfigLUIS.AppID = this.textBox_LUIS_AppID.Text;
                    saveSetting_.ConfigLUIS.Key = this.textBox_LUIS_Key.Text;
                    saveSetting_.ConfigLUIS.Location = this.textBox_LUIS_Location.Text;

                    saveSetting_.ConfigComputerVision.Enabled = this.cbx_ComputerVision.Checked;
                    saveSetting_.ConfigComputerVision.Endpoint = this.textBox_ComputerVision_Endpoint.Text;
                    saveSetting_.ConfigComputerVision.Key = this.textBox_ComputerVision_Key.Text;

                    saveSetting_.ConfigCustomVision.Enabled = this.cbx_CustomVision.Checked;
                    saveSetting_.ConfigCustomVision.Endpoint = this.textBox_CustomVision_Endpoint.Text;
                    saveSetting_.ConfigCustomVision.Key = this.textBox_CustomVision_Key.Text;

                    saveSetting_.ConfigFace.Enabled = this.cbx_Face.Checked;
                    saveSetting_.ConfigFace.Endpoint = this.textBox_Face_Endpoint.Text;
                    saveSetting_.ConfigFace.Key = this.textBox_Face_Key.Text;

                    saveSetting_.ConfigFormRecognizer.Enabled = this.cbx_FormRecognizer.Checked;
                    saveSetting_.ConfigFormRecognizer.Endpoint = this.textBox_FormRecognizer_Endpoint.Text;
                    saveSetting_.ConfigFormRecognizer.Key = this.textBox_FormRecognizer_Key.Text;

                    saveSetting_.ConfigSpeech.Enabled = this.cbx_Speech.Checked;
                    saveSetting_.ConfigSpeech.Key = this.textBox_Speech_Key.Text;
                    saveSetting_.ConfigSpeech.Location = this.textBox_Speech_Location.Text;

                    #endregion
                }
                else
                {
                    #region 將saveSetting_內容傳至UI

                    this.cbx_QnAMaker.Checked = saveSetting_.ConfigQnAMaker.Enabled;
                    this.textBox_QnAMaker_URL.Text = saveSetting_.ConfigQnAMaker.URL;
                    this.textBox_QnAMaker_EndpointKey.Text = saveSetting_.ConfigQnAMaker.EndpointKey;

                    this.cbx_LUIS.Checked = saveSetting_.ConfigLUIS.Enabled;
                    this.textBox_LUIS_AppID.Text = saveSetting_.ConfigLUIS.AppID;
                    this.textBox_LUIS_Key.Text = saveSetting_.ConfigLUIS.Key;
                    this.textBox_LUIS_Location.Text = saveSetting_.ConfigLUIS.Location;

                    this.cbx_ComputerVision.Checked = saveSetting_.ConfigComputerVision.Enabled;
                    this.textBox_ComputerVision_Endpoint.Text = saveSetting_.ConfigComputerVision.Endpoint;
                    this.textBox_ComputerVision_Key.Text = saveSetting_.ConfigComputerVision.Key;

                    this.cbx_CustomVision.Checked = saveSetting_.ConfigCustomVision.Enabled;
                    this.textBox_CustomVision_Endpoint.Text = saveSetting_.ConfigCustomVision.Endpoint;
                    this.textBox_CustomVision_Key.Text = saveSetting_.ConfigCustomVision.Key;

                    this.cbx_Face.Checked = saveSetting_.ConfigFace.Enabled;
                    this.textBox_Face_Endpoint.Text = saveSetting_.ConfigFace.Endpoint;
                    this.textBox_Face_Key.Text = saveSetting_.ConfigFace.Key;

                    this.cbx_FormRecognizer.Checked = saveSetting_.ConfigFormRecognizer.Enabled;
                    this.textBox_FormRecognizer_Endpoint.Text = saveSetting_.ConfigFormRecognizer.Endpoint;
                    this.textBox_FormRecognizer_Key.Text = saveSetting_.ConfigFormRecognizer.Key;

                    this.cbx_Speech.Checked = saveSetting_.ConfigSpeech.Enabled;
                    this.textBox_Speech_Key.Text = saveSetting_.ConfigSpeech.Key;
                    this.textBox_Speech_Location.Text = saveSetting_.ConfigSpeech.Location;

                    #endregion
                }

                b_status_ = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return b_status_;
        }

        /// <summary>
        /// 改變ON/OFF狀態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = sender as CheckBox;
            string Tag = cbx.Tag.ToString();
            if (cbx.Checked) // ON
            {
                cbx.BackgroundImage = Properties.Resources.ON;
                if (this.Dictionary_Label.ContainsKey(Tag))
                {
                    this.Dictionary_Label[Tag].Text = "ON";
                    this.Dictionary_Label[Tag].ForeColor = Color.DeepSkyBlue;
                }
            }
            else // OFF
            {
                cbx.BackgroundImage = Properties.Resources.OFF_edited;
                if (this.Dictionary_Label.ContainsKey(Tag))
                {
                    this.Dictionary_Label[Tag].Text = "OFF";
                    this.Dictionary_Label[Tag].ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                }
            }

            // Enabled狀態切換
            if (this.Dict_ON_Enabled.ContainsKey(Tag))
            {
                foreach (Control c in this.Dict_ON_Enabled[Tag])
                    c.Enabled = cbx.Checked;
            }
        }

        /// <summary>
        /// 【載入預設值】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_LoadDefaultValues_Click(object sender, EventArgs e)
        {
            cls_ConfigSetting saveSetting_ = new cls_ConfigSetting();
            this.ui_parameters(false, saveSetting_);
        }

        /// <summary>
        /// 【關閉】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            //this.Close();
            DialogResult = DialogResult.Cancel; // 會自動關閉表單
        }

        /// <summary>
        /// 【儲存】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to 【Save】?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
                return;

            if (!(this.ui_parameters(true)))
            {
                MessageBox.Show("【Save】fails!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool b_status_ = this.saveSetting.Save();
            
            if (b_status_)
            {
                MessageBox.Show("【Save】succeeds", "Prompt message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.Yes; // 會自動關閉表單
            }
            else
                MessageBox.Show("【Save】fails!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #region 讓使用者可移動視窗

        int curr_x, curr_y;
        bool isWndMove;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.curr_x = e.X;
                this.curr_y = e.Y;
                this.isWndMove = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isWndMove)
            {
                //this.Location = new Point(this.Left + e.X - this.curr_x, this.Top + e.Y - this.curr_y);
                this.Location = new Point(Control.MousePosition.X - e.X + (e.X - this.curr_x), Control.MousePosition.Y - e.Y + (e.Y - this.curr_y));
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.isWndMove = false;
        }

        #endregion
    }
}
