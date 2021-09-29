using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using MicrosoftCognitiveServices.ConfigSetting;
using ComputerVision;
using CustomVision;
using Face;
using FormRecognizer;
using DeltaSubstrateInspector;

namespace MicrosoftCognitiveServices.Azure_Cognitive_Services.Vision
{
    public partial class UC_Vision : UserControl
    {
        private cls_ConfigSetting configSetting = new cls_ConfigSetting();

        private TabControl HidePage { get; set; } = new TabControl();

        private int Initial_pictureBox1Width = 1200, Initial_pictureBox1Height = 835;

        /// <summary>
        /// 滑鼠是否移動
        /// </summary>
        private bool B_mouseMove { get; set; } = false;

        /// <summary>
        /// 滑鼠座標
        /// </summary>
        private Point MousePoint { get; set; } = new Point();

        /// <summary>
        /// 滑鼠座標 (相對視窗)
        /// </summary>
        private Point MousePoint_Windows { get; set; } = new Point();

        private GUI_ComputerVision_Analysis gui_ComputerVision_Analysis { get; set; } = new GUI_ComputerVision_Analysis();

        private GUI_ComputerVision_Detect gui_ComputerVision_Detect { get; set; } = new GUI_ComputerVision_Detect();

        private GUI_ComputerVision_OCR gui_ComputerVision_OCR { get; set; } = new GUI_ComputerVision_OCR();

        private GUI_Face_Detect gui_Face_Detect { get; set; } = new GUI_Face_Detect();

        private GUI_CustomVision gui_CustomVision { get; set; } = new GUI_CustomVision();

        private GUI_FormRecognizer gui_FormRecognizer { get; set; } = new GUI_FormRecognizer();

        /// <summary>
        /// 繪圖區
        /// </summary>
        private Graphics g { get; set; }

        private string path { get; set; } = "";

        public UC_Vision()
        {
            InitializeComponent();

            #region 控制項顯示影像之背景顏色設定為透明

            clsStaticTool.Control_DispImg_Transparent(this.radioButton_ComputerVision, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_CustomVision, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_Face, false);
            clsStaticTool.Control_DispImg_Transparent(this.radioButton_FormRecognizer, false);

            #endregion

            this.g = this.pictureBox_image.CreateGraphics(); // 取得繪圖區物件
            this.Initial_pictureBox1Width = this.pictureBox_image.Width;
            this.Initial_pictureBox1Height = this.pictureBox_image.Height;
        }

        /// <summary>
        /// Set configSetting
        /// </summary>
        /// <param name="configSetting_"></param>
        public void Set_configSetting(cls_ConfigSetting configSetting_)
        {
            this.configSetting = configSetting_;
        }

        private void UC_Vision_Load(object sender, EventArgs e)
        {
            this.Update_tabControl_Operation();
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
            this.radioButton_ComputerVision.Text = "        " + this.radioButton_ComputerVision.Text;
            this.radioButton_CustomVision.Text = "        " + this.radioButton_CustomVision.Text;
            this.radioButton_Face.Text = "        " + this.radioButton_Face.Text;
            this.radioButton_FormRecognizer.Text = "        " + this.radioButton_FormRecognizer.Text;

            // 更新TabControl顯示頁面
            this.Update_tabControl_Operation();
        }

        /// <summary>
        /// 【載入影像】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenImgDilg = new OpenFileDialog();
            OpenImgDilg.FileName = this.path;
            if (OpenImgDilg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            this.path = OpenImgDilg.FileName;
            this.Clear();

            Bitmap LoadImage_bmp = new Bitmap(this.path);
            this.Initial_pictureBox1Width = LoadImage_bmp.Width;
            this.Initial_pictureBox1Height = LoadImage_bmp.Height;
            this.pictureBox_image.Image = LoadImage_bmp;

            this.pictureBox_image.Width = this.Initial_pictureBox1Width;
            this.pictureBox_image.Height = this.Initial_pictureBox1Height;

            this.button_Execute.Enabled = true;
        }

        private void Clear()
        {
            this.gui_ComputerVision_Analysis.Clear();
            this.gui_ComputerVision_Detect.Clear();
            this.gui_ComputerVision_OCR.Clear();
            this.gui_Face_Detect.Clear();
            this.gui_CustomVision.Clear(true);
            this.gui_FormRecognizer.Clear();
        }

        /// <summary>
        /// 更新TabControl顯示頁面
        /// </summary>
        /// <param name="tag"></param>
        private void Update_tabControl_Operation(string tag = null)
        {
            if (tag == null)
            {
                if (this.radioButton_ComputerVision.Checked)
                    tag = this.radioButton_ComputerVision.Tag.ToString();
                else if (this.radioButton_CustomVision.Checked)
                    tag = this.radioButton_CustomVision.Tag.ToString();
                else if (this.radioButton_Face.Checked)
                    tag = this.radioButton_Face.Tag.ToString();
                else if (this.radioButton_FormRecognizer.Checked)
                    tag = this.radioButton_FormRecognizer.Tag.ToString();
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
            this.Clear();
            this.richTextBox_Info.Clear();
        }

        /// <summary>
        /// Operation switch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbt_Operation_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbt = sender as RadioButton;
            if (rbt.Checked == false)
                return;

            this.Clear();
            this.richTextBox_Info.Clear();
        }

        /// <summary>
        /// 【Execute】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Execute_Click(object sender, EventArgs e)
        {
            this.Clear();
            if (this.radioButton_ComputerVision.Checked) //【電腦視覺】
            {
                if (this.rbt_ComputerVision_Analysis.Checked) //【影像分析】
                    this.Execute_ComputerVision_Analysis();
                else if (this.rbt_ComputerVision_Detect.Checked) //【物件偵測】
                    this.Execute_ComputerVision_Detect();
                else if (this.rbt_ComputerVision_OCR.Checked) //【文字擷取 (OCR)】
                    this.Execute_ComputerVision_OCR();
            }
            else if (this.radioButton_CustomVision.Checked) //【自訂視覺】
                this.Execute_CustomVision();
            else if (this.radioButton_Face.Checked) //【臉部】
                this.Execute_Face_Detection();
            else if (this.radioButton_FormRecognizer.Checked) //【表單辨識器】
                this.Execute_FormRecognizer();
        }

        private clsProgressbar ShowWaitProgress()
        {
            clsProgressbar m_ProgressBar = new clsProgressbar();
            m_ProgressBar.FormClosedEvent2 += new clsProgressbar.FormClosedHandler2(SetFormClosed2);
            m_ProgressBar.SetShowText("Please wait for executing......");
            m_ProgressBar.SetShowCaption("Executing......");
            m_ProgressBar.ShowWaitProgress();
            return m_ProgressBar;
        }

        public void SetFormClosed2()
        {

        }

        #region Executions

        /// <summary>
        /// 執行 【影像分析】
        /// </summary>
        private void Execute_ComputerVision_Analysis()
        {
            clsProgressbar m_ProgressBar = this.ShowWaitProgress();

            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        var result = ComputerVisionApp.MakeRequest_Analysis(this.configSetting.ConfigComputerVision.Endpoint, this.configSetting.ConfigComputerVision.Key, this.path).GetAwaiter().GetResult();
                        if (result?.RequestId_ != null)
                        {
                            this.gui_ComputerVision_Analysis.Parse(result);

                            if (this.pictureBox_image.InvokeRequired)
                                this.pictureBox_image.BeginInvoke(new Action(() => this.pictureBox_image.Invalidate()));
                            else
                                this.pictureBox_image.Invalidate();
                        }
                        else
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
        /// 執行 【物件偵測】
        /// </summary>
        private void Execute_ComputerVision_Detect()
        {
            clsProgressbar m_ProgressBar = this.ShowWaitProgress();
            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        List<ComputerVisionApp.VisualFeatureTypes> reqFeatureTypes = new List<ComputerVisionApp.VisualFeatureTypes>();
                        reqFeatureTypes.Add(ComputerVisionApp.VisualFeatureTypes.Objects);
                        var result = ComputerVisionApp.MakeRequest_Analysis(this.configSetting.ConfigComputerVision.Endpoint, this.configSetting.ConfigComputerVision.Key, this.path, reqFeatureTypes).GetAwaiter().GetResult();
                        if (result?.Objects != null)
                        {
                            this.gui_ComputerVision_Detect.Parse(result);

                            if (this.pictureBox_image.InvokeRequired)
                                this.pictureBox_image.BeginInvoke(new Action(() => this.pictureBox_image.Invalidate()));
                            else
                                this.pictureBox_image.Invalidate();
                        }
                        else
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
        /// 執行 【文字擷取 (OCR)】
        /// </summary>
        private void Execute_ComputerVision_OCR()
        {
            clsProgressbar m_ProgressBar = this.ShowWaitProgress();
            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        var result = ComputerVisionApp.MakeRequest_OCR(this.configSetting.ConfigComputerVision.Endpoint, this.configSetting.ConfigComputerVision.Key, this.path).GetAwaiter().GetResult();
                        if (result?.Status != null)
                        {
                            this.gui_ComputerVision_OCR.Parse(result);

                            if (this.pictureBox_image.InvokeRequired)
                                this.pictureBox_image.BeginInvoke(new Action(() => this.pictureBox_image.Invalidate()));
                            else
                                this.pictureBox_image.Invalidate();
                        }
                        else
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
        /// 執行 【自訂視覺】
        /// </summary>
        private void Execute_CustomVision()
        {
            clsProgressbar m_ProgressBar = this.ShowWaitProgress();
            double threshold = this.trackBar_Threshold.Value / 100.0;
            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        var result = CustomVisionApp.MakeRequest(this.configSetting.ConfigCustomVision.Endpoint, this.configSetting.ConfigCustomVision.Key, this.path).GetAwaiter().GetResult();
                        if (result != null)
                        {
                            this.gui_CustomVision.Parse(threshold, result);

                            if (this.pictureBox_image.InvokeRequired)
                                this.pictureBox_image.BeginInvoke(new Action(() => this.pictureBox_image.Invalidate()));
                            else
                                this.pictureBox_image.Invalidate();
                        }
                        else
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
        /// 執行 【臉部偵測】
        /// </summary>
        private void Execute_Face_Detection()
        {
            clsProgressbar m_ProgressBar = this.ShowWaitProgress();
            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        var result = FaceApp.MakeRequest(this.configSetting.ConfigFace.Endpoint, this.configSetting.ConfigFace.Key, this.path).GetAwaiter().GetResult();
                        if (result != null)
                        {
                            this.gui_Face_Detect.Parse(result);

                            if (this.pictureBox_image.InvokeRequired)
                                this.pictureBox_image.BeginInvoke(new Action(() => this.pictureBox_image.Invalidate()));
                            else
                                this.pictureBox_image.Invalidate();
                        }
                        else
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
        /// 執行 【表單辨識器】
        /// </summary>
        private void Execute_FormRecognizer()
        {
            clsProgressbar m_ProgressBar = this.ShowWaitProgress();
            Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        var result = FormRecognizerApp.MakeRequest(this.configSetting.ConfigFormRecognizer.Endpoint, this.configSetting.ConfigFormRecognizer.Key, this.path).GetAwaiter().GetResult();
                        if (result != null)
                        {
                            this.gui_FormRecognizer.Parse(result);

                            if (this.pictureBox_image.InvokeRequired)
                                this.pictureBox_image.BeginInvoke(new Action(() => this.pictureBox_image.Invalidate()));
                            else
                                this.pictureBox_image.Invalidate();
                        }
                        else
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

        #endregion

        private void pictureBox_image_MouseMove(object sender, MouseEventArgs e)
        {
            this.B_mouseMove = true;
            this.MousePoint = e.Location;
            //this.MousePoint_Windows = new Point(Control.MousePosition.X - this.Location.X - this.panel_PictureBox.Left,
            //                                    Control.MousePosition.Y - this.Location.Y - this.panel_PictureBox.Top);
            Point pt = this.PointToClient(Control.MousePosition);
            this.MousePoint_Windows = new Point(pt.X + 5, pt.Y - 40);
            this.pictureBox_image.Invalidate();
        }

        /// <summary>
        /// 更新 pictureBox 顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_image_Paint(object sender, PaintEventArgs e)
        {
            this.g = e.Graphics;

            if (this.radioButton_ComputerVision.Checked) //【電腦視覺】
            {
                if (this.rbt_ComputerVision_Analysis.Checked) //【影像分析】
                    this.gui_ComputerVision_Analysis.Draw(this.g, this.MousePoint, this.MousePoint_Windows, this.B_mouseMove, this.label_DispInfo, this.richTextBox_Info);
                else if (this.rbt_ComputerVision_Detect.Checked) //【物件偵測】
                    this.gui_ComputerVision_Detect.Draw(this.g, this.MousePoint, this.MousePoint_Windows, this.B_mouseMove,
                                                        this.pictureBox_image, this.contextMenuStrip_DispInfo, this.toolStripMenuItem_Info, this.label_DispInfo);
                else if (this.rbt_ComputerVision_OCR.Checked) //【文字擷取 (OCR)】
                    this.gui_ComputerVision_OCR.Draw(this.g, this.MousePoint, this.MousePoint_Windows, this.B_mouseMove, this.label_DispInfo, this.richTextBox_Info);
            }
            else if (this.radioButton_CustomVision.Checked) //【自訂視覺】
                this.gui_CustomVision.Draw(this.g, this.MousePoint, this.MousePoint_Windows, this.B_mouseMove, this.label_DispInfo, this.richTextBox_Info, this.trackBar_Threshold.Value / 100.0);
            else if (this.radioButton_Face.Checked) //【臉部】
                this.gui_Face_Detect.Draw(this.g, this.MousePoint, this.MousePoint_Windows, this.B_mouseMove, this.label_DispInfo, this.richTextBox_Info);
            else if (this.radioButton_FormRecognizer.Checked) //【表單辨識器】
                this.gui_FormRecognizer.Draw(this.g, this.MousePoint, this.MousePoint_Windows, this.B_mouseMove, this.label_DispInfo, this.richTextBox_Info);

            this.B_mouseMove = false;
        }

        /// <summary>
        /// 【Probability Threshold】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar_Threshold_Scroll(object sender, EventArgs e)
        {
            int value = this.trackBar_Threshold.Value;

            // 更新顯示數值
            //this.label_ThresholdValue.Text = value.ToString().PadLeft(3) + "%";
            this.label_ThresholdValue.Text = $"{value}".PadLeft(3) + "%";

            // 更新顯示
            if (this.radioButton_CustomVision.Checked) // For 【自訂視覺】
                this.pictureBox_image.Invalidate();
        }

        #region 點擊RichTextBox中的超連結，開啟瀏覽器

        /*
            作法：使用 Windows Forms RichTextBox 控制項顯示 Web 樣式連結: https://docs.microsoft.com/zh-tw/dotnet/desktop/winforms/controls/how-to-display-web-style-links-with-the-windows-forms-richtextbox-control?view=netframeworkdesktop-4.8
            C#呼叫預設瀏覽器開啟網頁的幾種方法: https://www.itread01.com/content/1546728863.html
        */
        public System.Diagnostics.Process p { get; set; } = new System.Diagnostics.Process();

        private void richTextBox_Info_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            // Call Process.Start method to open a browser
            // with link text as URL.

            //p = System.Diagnostics.Process.Start("iexplore.exe", e.LinkText); // Exception: System.ComponentModel.Win32Exception: '系統找不到指定的檔案'!
            //p = System.Diagnostics.Process.Start("chrome.exe", e.LinkText); // Exception: System.ComponentModel.Win32Exception: '系統找不到指定的檔案'!
            //p = System.Diagnostics.Process.Start("explorer.exe", e.LinkText);
            //p = System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", e.LinkText);
            p = System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", e.LinkText);
        }

        #endregion
    }
}
