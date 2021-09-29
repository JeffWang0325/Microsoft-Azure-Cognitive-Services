
namespace MicrosoftCognitiveServices.Azure_Cognitive_Services.Vision
{
    partial class UC_Vision
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Vision));
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton_CustomVision = new System.Windows.Forms.RadioButton();
            this.radioButton_ComputerVision = new System.Windows.Forms.RadioButton();
            this.radioButton_Face = new System.Windows.Forms.RadioButton();
            this.radioButton_FormRecognizer = new System.Windows.Forms.RadioButton();
            this.panel_PictureBox = new System.Windows.Forms.Panel();
            this.pictureBox_image = new System.Windows.Forms.PictureBox();
            this.richTextBox_Info = new System.Windows.Forms.RichTextBox();
            this.tabControl_Operation = new System.Windows.Forms.TabControl();
            this.tabPage_ComputerVision = new System.Windows.Forms.TabPage();
            this.rbt_ComputerVision_OCR = new System.Windows.Forms.RadioButton();
            this.rbt_ComputerVision_Detect = new System.Windows.Forms.RadioButton();
            this.rbt_ComputerVision_Analysis = new System.Windows.Forms.RadioButton();
            this.tabPage_CustomVision = new System.Windows.Forms.TabPage();
            this.rbt_CustomVision_ClassifyDetect = new System.Windows.Forms.RadioButton();
            this.label_ThresholdValue = new System.Windows.Forms.Label();
            this.label_Threshold = new System.Windows.Forms.Label();
            this.trackBar_Threshold = new System.Windows.Forms.TrackBar();
            this.tabPage_Face = new System.Windows.Forms.TabPage();
            this.rbt_Face_Detection = new System.Windows.Forms.RadioButton();
            this.tabPage_FormRecognizer = new System.Windows.Forms.TabPage();
            this.rbt_FormRecognizer_RecogReceipt = new System.Windows.Forms.RadioButton();
            this.button_LoadImage = new System.Windows.Forms.Button();
            this.button_Execute = new System.Windows.Forms.Button();
            this.contextMenuStrip_DispInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Info = new System.Windows.Forms.ToolStripMenuItem();
            this.label_DispInfo = new System.Windows.Forms.Label();
            this.panel_PictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).BeginInit();
            this.tabControl_Operation.SuspendLayout();
            this.tabPage_ComputerVision.SuspendLayout();
            this.tabPage_CustomVision.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Threshold)).BeginInit();
            this.tabPage_Face.SuspendLayout();
            this.tabPage_FormRecognizer.SuspendLayout();
            this.contextMenuStrip_DispInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(200, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 26);
            this.label1.TabIndex = 12;
            this.label1.Text = "服務 :";
            // 
            // radioButton_CustomVision
            // 
            this.radioButton_CustomVision.AutoSize = true;
            this.radioButton_CustomVision.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton_CustomVision.ForeColor = System.Drawing.Color.Blue;
            this.radioButton_CustomVision.Image = ((System.Drawing.Image)(resources.GetObject("radioButton_CustomVision.Image")));
            this.radioButton_CustomVision.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButton_CustomVision.Location = new System.Drawing.Point(580, 6);
            this.radioButton_CustomVision.Name = "radioButton_CustomVision";
            this.radioButton_CustomVision.Size = new System.Drawing.Size(144, 40);
            this.radioButton_CustomVision.TabIndex = 21;
            this.radioButton_CustomVision.Tag = "CustomVision";
            this.radioButton_CustomVision.Text = "        自訂視覺";
            this.radioButton_CustomVision.UseVisualStyleBackColor = true;
            this.radioButton_CustomVision.CheckedChanged += new System.EventHandler(this.rbt_Service_CheckedChanged);
            // 
            // radioButton_ComputerVision
            // 
            this.radioButton_ComputerVision.AutoSize = true;
            this.radioButton_ComputerVision.Checked = true;
            this.radioButton_ComputerVision.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton_ComputerVision.ForeColor = System.Drawing.Color.Blue;
            this.radioButton_ComputerVision.Image = ((System.Drawing.Image)(resources.GetObject("radioButton_ComputerVision.Image")));
            this.radioButton_ComputerVision.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButton_ComputerVision.Location = new System.Drawing.Point(330, 6);
            this.radioButton_ComputerVision.Name = "radioButton_ComputerVision";
            this.radioButton_ComputerVision.Size = new System.Drawing.Size(144, 37);
            this.radioButton_ComputerVision.TabIndex = 20;
            this.radioButton_ComputerVision.TabStop = true;
            this.radioButton_ComputerVision.Tag = "ComputerVision";
            this.radioButton_ComputerVision.Text = "        電腦視覺";
            this.radioButton_ComputerVision.UseVisualStyleBackColor = true;
            this.radioButton_ComputerVision.CheckedChanged += new System.EventHandler(this.rbt_Service_CheckedChanged);
            // 
            // radioButton_Face
            // 
            this.radioButton_Face.AutoSize = true;
            this.radioButton_Face.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton_Face.ForeColor = System.Drawing.Color.Blue;
            this.radioButton_Face.Image = ((System.Drawing.Image)(resources.GetObject("radioButton_Face.Image")));
            this.radioButton_Face.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButton_Face.Location = new System.Drawing.Point(830, 6);
            this.radioButton_Face.Name = "radioButton_Face";
            this.radioButton_Face.Size = new System.Drawing.Size(106, 40);
            this.radioButton_Face.TabIndex = 22;
            this.radioButton_Face.Tag = "Face";
            this.radioButton_Face.Text = "        臉部";
            this.radioButton_Face.UseVisualStyleBackColor = true;
            this.radioButton_Face.CheckedChanged += new System.EventHandler(this.rbt_Service_CheckedChanged);
            // 
            // radioButton_FormRecognizer
            // 
            this.radioButton_FormRecognizer.AutoSize = true;
            this.radioButton_FormRecognizer.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton_FormRecognizer.ForeColor = System.Drawing.Color.Blue;
            this.radioButton_FormRecognizer.Image = ((System.Drawing.Image)(resources.GetObject("radioButton_FormRecognizer.Image")));
            this.radioButton_FormRecognizer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButton_FormRecognizer.Location = new System.Drawing.Point(1047, 9);
            this.radioButton_FormRecognizer.Name = "radioButton_FormRecognizer";
            this.radioButton_FormRecognizer.Size = new System.Drawing.Size(163, 35);
            this.radioButton_FormRecognizer.TabIndex = 23;
            this.radioButton_FormRecognizer.Tag = "FormRecognizer";
            this.radioButton_FormRecognizer.Text = "        表單辨識器";
            this.radioButton_FormRecognizer.UseVisualStyleBackColor = true;
            this.radioButton_FormRecognizer.CheckedChanged += new System.EventHandler(this.rbt_Service_CheckedChanged);
            // 
            // panel_PictureBox
            // 
            this.panel_PictureBox.AutoScroll = true;
            this.panel_PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_PictureBox.Controls.Add(this.pictureBox_image);
            this.panel_PictureBox.Location = new System.Drawing.Point(10, 65);
            this.panel_PictureBox.Name = "panel_PictureBox";
            this.panel_PictureBox.Size = new System.Drawing.Size(1200, 835);
            this.panel_PictureBox.TabIndex = 24;
            // 
            // pictureBox_image
            // 
            this.pictureBox_image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_image.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_image.Name = "pictureBox_image";
            this.pictureBox_image.Size = new System.Drawing.Size(1200, 835);
            this.pictureBox_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_image.TabIndex = 1;
            this.pictureBox_image.TabStop = false;
            this.pictureBox_image.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_image_Paint);
            this.pictureBox_image.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_image_MouseMove);
            // 
            // richTextBox_Info
            // 
            this.richTextBox_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_Info.Location = new System.Drawing.Point(1220, 262);
            this.richTextBox_Info.Name = "richTextBox_Info";
            this.richTextBox_Info.Size = new System.Drawing.Size(355, 638);
            this.richTextBox_Info.TabIndex = 25;
            this.richTextBox_Info.Text = "";
            this.richTextBox_Info.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox_Info_LinkClicked);
            // 
            // tabControl_Operation
            // 
            this.tabControl_Operation.Controls.Add(this.tabPage_ComputerVision);
            this.tabControl_Operation.Controls.Add(this.tabPage_CustomVision);
            this.tabControl_Operation.Controls.Add(this.tabPage_Face);
            this.tabControl_Operation.Controls.Add(this.tabPage_FormRecognizer);
            this.tabControl_Operation.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControl_Operation.Location = new System.Drawing.Point(1217, 88);
            this.tabControl_Operation.Name = "tabControl_Operation";
            this.tabControl_Operation.SelectedIndex = 0;
            this.tabControl_Operation.Size = new System.Drawing.Size(358, 168);
            this.tabControl_Operation.TabIndex = 26;
            // 
            // tabPage_ComputerVision
            // 
            this.tabPage_ComputerVision.Controls.Add(this.rbt_ComputerVision_OCR);
            this.tabPage_ComputerVision.Controls.Add(this.rbt_ComputerVision_Detect);
            this.tabPage_ComputerVision.Controls.Add(this.rbt_ComputerVision_Analysis);
            this.tabPage_ComputerVision.Location = new System.Drawing.Point(4, 29);
            this.tabPage_ComputerVision.Name = "tabPage_ComputerVision";
            this.tabPage_ComputerVision.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_ComputerVision.Size = new System.Drawing.Size(350, 135);
            this.tabPage_ComputerVision.TabIndex = 0;
            this.tabPage_ComputerVision.Tag = "ComputerVision";
            this.tabPage_ComputerVision.Text = "電腦視覺操作";
            this.tabPage_ComputerVision.UseVisualStyleBackColor = true;
            // 
            // rbt_ComputerVision_OCR
            // 
            this.rbt_ComputerVision_OCR.AutoSize = true;
            this.rbt_ComputerVision_OCR.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbt_ComputerVision_OCR.Location = new System.Drawing.Point(70, 95);
            this.rbt_ComputerVision_OCR.Name = "rbt_ComputerVision_OCR";
            this.rbt_ComputerVision_OCR.Size = new System.Drawing.Size(139, 24);
            this.rbt_ComputerVision_OCR.TabIndex = 9;
            this.rbt_ComputerVision_OCR.Text = "文字擷取 (OCR)";
            this.rbt_ComputerVision_OCR.UseVisualStyleBackColor = true;
            this.rbt_ComputerVision_OCR.CheckedChanged += new System.EventHandler(this.rbt_Operation_CheckedChanged);
            // 
            // rbt_ComputerVision_Detect
            // 
            this.rbt_ComputerVision_Detect.AutoSize = true;
            this.rbt_ComputerVision_Detect.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbt_ComputerVision_Detect.Location = new System.Drawing.Point(70, 55);
            this.rbt_ComputerVision_Detect.Name = "rbt_ComputerVision_Detect";
            this.rbt_ComputerVision_Detect.Size = new System.Drawing.Size(91, 24);
            this.rbt_ComputerVision_Detect.TabIndex = 8;
            this.rbt_ComputerVision_Detect.Text = "物件偵測";
            this.rbt_ComputerVision_Detect.UseVisualStyleBackColor = true;
            this.rbt_ComputerVision_Detect.CheckedChanged += new System.EventHandler(this.rbt_Operation_CheckedChanged);
            // 
            // rbt_ComputerVision_Analysis
            // 
            this.rbt_ComputerVision_Analysis.AutoSize = true;
            this.rbt_ComputerVision_Analysis.Checked = true;
            this.rbt_ComputerVision_Analysis.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbt_ComputerVision_Analysis.Location = new System.Drawing.Point(70, 15);
            this.rbt_ComputerVision_Analysis.Name = "rbt_ComputerVision_Analysis";
            this.rbt_ComputerVision_Analysis.Size = new System.Drawing.Size(91, 24);
            this.rbt_ComputerVision_Analysis.TabIndex = 7;
            this.rbt_ComputerVision_Analysis.TabStop = true;
            this.rbt_ComputerVision_Analysis.Text = "影像分析";
            this.rbt_ComputerVision_Analysis.UseVisualStyleBackColor = true;
            this.rbt_ComputerVision_Analysis.CheckedChanged += new System.EventHandler(this.rbt_Operation_CheckedChanged);
            // 
            // tabPage_CustomVision
            // 
            this.tabPage_CustomVision.Controls.Add(this.rbt_CustomVision_ClassifyDetect);
            this.tabPage_CustomVision.Controls.Add(this.label_ThresholdValue);
            this.tabPage_CustomVision.Controls.Add(this.label_Threshold);
            this.tabPage_CustomVision.Controls.Add(this.trackBar_Threshold);
            this.tabPage_CustomVision.Location = new System.Drawing.Point(4, 29);
            this.tabPage_CustomVision.Name = "tabPage_CustomVision";
            this.tabPage_CustomVision.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_CustomVision.Size = new System.Drawing.Size(350, 135);
            this.tabPage_CustomVision.TabIndex = 1;
            this.tabPage_CustomVision.Tag = "CustomVision";
            this.tabPage_CustomVision.Text = "自訂視覺操作";
            this.tabPage_CustomVision.UseVisualStyleBackColor = true;
            // 
            // rbt_CustomVision_ClassifyDetect
            // 
            this.rbt_CustomVision_ClassifyDetect.AutoSize = true;
            this.rbt_CustomVision_ClassifyDetect.Checked = true;
            this.rbt_CustomVision_ClassifyDetect.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbt_CustomVision_ClassifyDetect.Location = new System.Drawing.Point(70, 16);
            this.rbt_CustomVision_ClassifyDetect.Name = "rbt_CustomVision_ClassifyDetect";
            this.rbt_CustomVision_ClassifyDetect.Size = new System.Drawing.Size(139, 24);
            this.rbt_CustomVision_ClassifyDetect.TabIndex = 18;
            this.rbt_CustomVision_ClassifyDetect.TabStop = true;
            this.rbt_CustomVision_ClassifyDetect.Text = "分類、物件偵測";
            this.rbt_CustomVision_ClassifyDetect.UseVisualStyleBackColor = true;
            // 
            // label_ThresholdValue
            // 
            this.label_ThresholdValue.AutoSize = true;
            this.label_ThresholdValue.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_ThresholdValue.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label_ThresholdValue.Location = new System.Drawing.Point(243, 55);
            this.label_ThresholdValue.Name = "label_ThresholdValue";
            this.label_ThresholdValue.Size = new System.Drawing.Size(45, 20);
            this.label_ThresholdValue.TabIndex = 17;
            this.label_ThresholdValue.Text = " 50%";
            // 
            // label_Threshold
            // 
            this.label_Threshold.AutoSize = true;
            this.label_Threshold.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_Threshold.Location = new System.Drawing.Point(70, 55);
            this.label_Threshold.Name = "label_Threshold";
            this.label_Threshold.Size = new System.Drawing.Size(173, 20);
            this.label_Threshold.TabIndex = 16;
            this.label_Threshold.Text = "Probability Threshold:";
            // 
            // trackBar_Threshold
            // 
            this.trackBar_Threshold.Location = new System.Drawing.Point(70, 82);
            this.trackBar_Threshold.Maximum = 100;
            this.trackBar_Threshold.Name = "trackBar_Threshold";
            this.trackBar_Threshold.Size = new System.Drawing.Size(216, 45);
            this.trackBar_Threshold.TabIndex = 15;
            this.trackBar_Threshold.Value = 50;
            this.trackBar_Threshold.Scroll += new System.EventHandler(this.trackBar_Threshold_Scroll);
            // 
            // tabPage_Face
            // 
            this.tabPage_Face.Controls.Add(this.rbt_Face_Detection);
            this.tabPage_Face.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Face.Name = "tabPage_Face";
            this.tabPage_Face.Size = new System.Drawing.Size(350, 135);
            this.tabPage_Face.TabIndex = 2;
            this.tabPage_Face.Tag = "Face";
            this.tabPage_Face.Text = "臉部操作";
            this.tabPage_Face.UseVisualStyleBackColor = true;
            // 
            // rbt_Face_Detection
            // 
            this.rbt_Face_Detection.AutoSize = true;
            this.rbt_Face_Detection.Checked = true;
            this.rbt_Face_Detection.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbt_Face_Detection.Location = new System.Drawing.Point(70, 55);
            this.rbt_Face_Detection.Name = "rbt_Face_Detection";
            this.rbt_Face_Detection.Size = new System.Drawing.Size(91, 24);
            this.rbt_Face_Detection.TabIndex = 10;
            this.rbt_Face_Detection.TabStop = true;
            this.rbt_Face_Detection.Text = "臉部偵測";
            this.rbt_Face_Detection.UseVisualStyleBackColor = true;
            // 
            // tabPage_FormRecognizer
            // 
            this.tabPage_FormRecognizer.Controls.Add(this.rbt_FormRecognizer_RecogReceipt);
            this.tabPage_FormRecognizer.Location = new System.Drawing.Point(4, 29);
            this.tabPage_FormRecognizer.Name = "tabPage_FormRecognizer";
            this.tabPage_FormRecognizer.Size = new System.Drawing.Size(350, 135);
            this.tabPage_FormRecognizer.TabIndex = 3;
            this.tabPage_FormRecognizer.Tag = "FormRecognizer";
            this.tabPage_FormRecognizer.Text = "表單辨識器操作";
            this.tabPage_FormRecognizer.UseVisualStyleBackColor = true;
            // 
            // rbt_FormRecognizer_RecogReceipt
            // 
            this.rbt_FormRecognizer_RecogReceipt.AutoSize = true;
            this.rbt_FormRecognizer_RecogReceipt.Checked = true;
            this.rbt_FormRecognizer_RecogReceipt.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbt_FormRecognizer_RecogReceipt.Location = new System.Drawing.Point(70, 55);
            this.rbt_FormRecognizer_RecogReceipt.Name = "rbt_FormRecognizer_RecogReceipt";
            this.rbt_FormRecognizer_RecogReceipt.Size = new System.Drawing.Size(91, 24);
            this.rbt_FormRecognizer_RecogReceipt.TabIndex = 16;
            this.rbt_FormRecognizer_RecogReceipt.TabStop = true;
            this.rbt_FormRecognizer_RecogReceipt.Text = "識別收據";
            this.rbt_FormRecognizer_RecogReceipt.UseVisualStyleBackColor = true;
            // 
            // button_LoadImage
            // 
            this.button_LoadImage.BackColor = System.Drawing.Color.Green;
            this.button_LoadImage.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_LoadImage.ForeColor = System.Drawing.Color.Yellow;
            this.button_LoadImage.Image = ((System.Drawing.Image)(resources.GetObject("button_LoadImage.Image")));
            this.button_LoadImage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_LoadImage.Location = new System.Drawing.Point(1290, 6);
            this.button_LoadImage.Name = "button_LoadImage";
            this.button_LoadImage.Size = new System.Drawing.Size(130, 71);
            this.button_LoadImage.TabIndex = 27;
            this.button_LoadImage.Text = "載入影像";
            this.button_LoadImage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_LoadImage.UseVisualStyleBackColor = false;
            this.button_LoadImage.Click += new System.EventHandler(this.button_LoadImage_Click);
            // 
            // button_Execute
            // 
            this.button_Execute.BackColor = System.Drawing.Color.Green;
            this.button_Execute.Enabled = false;
            this.button_Execute.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_Execute.ForeColor = System.Drawing.Color.Yellow;
            this.button_Execute.Image = ((System.Drawing.Image)(resources.GetObject("button_Execute.Image")));
            this.button_Execute.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Execute.Location = new System.Drawing.Point(1445, 6);
            this.button_Execute.Name = "button_Execute";
            this.button_Execute.Size = new System.Drawing.Size(130, 71);
            this.button_Execute.TabIndex = 28;
            this.button_Execute.Text = "執行";
            this.button_Execute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_Execute.UseVisualStyleBackColor = false;
            this.button_Execute.Click += new System.EventHandler(this.button_Execute_Click);
            // 
            // contextMenuStrip_DispInfo
            // 
            this.contextMenuStrip_DispInfo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.contextMenuStrip_DispInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Info});
            this.contextMenuStrip_DispInfo.Name = "contextMenuStrip_DispInfo";
            this.contextMenuStrip_DispInfo.Size = new System.Drawing.Size(170, 28);
            // 
            // toolStripMenuItem_Info
            // 
            this.toolStripMenuItem_Info.BackColor = System.Drawing.Color.Yellow;
            this.toolStripMenuItem_Info.ForeColor = System.Drawing.Color.Red;
            this.toolStripMenuItem_Info.Name = "toolStripMenuItem_Info";
            this.toolStripMenuItem_Info.Size = new System.Drawing.Size(169, 24);
            this.toolStripMenuItem_Info.Text = "Information";
            // 
            // label_DispInfo
            // 
            this.label_DispInfo.AutoSize = true;
            this.label_DispInfo.BackColor = System.Drawing.Color.Yellow;
            this.label_DispInfo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_DispInfo.ForeColor = System.Drawing.Color.Red;
            this.label_DispInfo.Location = new System.Drawing.Point(0, 0);
            this.label_DispInfo.Name = "label_DispInfo";
            this.label_DispInfo.Size = new System.Drawing.Size(74, 20);
            this.label_DispInfo.TabIndex = 30;
            this.label_DispInfo.Text = "DispInfo";
            this.label_DispInfo.Visible = false;
            // 
            // UC_Vision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_DispInfo);
            this.Controls.Add(this.button_Execute);
            this.Controls.Add(this.button_LoadImage);
            this.Controls.Add(this.tabControl_Operation);
            this.Controls.Add(this.richTextBox_Info);
            this.Controls.Add(this.panel_PictureBox);
            this.Controls.Add(this.radioButton_FormRecognizer);
            this.Controls.Add(this.radioButton_Face);
            this.Controls.Add(this.radioButton_CustomVision);
            this.Controls.Add(this.radioButton_ComputerVision);
            this.Controls.Add(this.label1);
            this.Name = "UC_Vision";
            this.Size = new System.Drawing.Size(1590, 900);
            this.Load += new System.EventHandler(this.UC_Vision_Load);
            this.panel_PictureBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).EndInit();
            this.tabControl_Operation.ResumeLayout(false);
            this.tabPage_ComputerVision.ResumeLayout(false);
            this.tabPage_ComputerVision.PerformLayout();
            this.tabPage_CustomVision.ResumeLayout(false);
            this.tabPage_CustomVision.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Threshold)).EndInit();
            this.tabPage_Face.ResumeLayout(false);
            this.tabPage_Face.PerformLayout();
            this.tabPage_FormRecognizer.ResumeLayout(false);
            this.tabPage_FormRecognizer.PerformLayout();
            this.contextMenuStrip_DispInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_CustomVision;
        private System.Windows.Forms.RadioButton radioButton_ComputerVision;
        private System.Windows.Forms.RadioButton radioButton_Face;
        private System.Windows.Forms.RadioButton radioButton_FormRecognizer;
        private System.Windows.Forms.Panel panel_PictureBox;
        private System.Windows.Forms.PictureBox pictureBox_image;
        private System.Windows.Forms.RichTextBox richTextBox_Info;
        private System.Windows.Forms.TabControl tabControl_Operation;
        private System.Windows.Forms.TabPage tabPage_ComputerVision;
        private System.Windows.Forms.TabPage tabPage_CustomVision;
        private System.Windows.Forms.TabPage tabPage_Face;
        private System.Windows.Forms.TabPage tabPage_FormRecognizer;
        private System.Windows.Forms.RadioButton rbt_CustomVision_ClassifyDetect;
        private System.Windows.Forms.Label label_ThresholdValue;
        private System.Windows.Forms.Label label_Threshold;
        private System.Windows.Forms.TrackBar trackBar_Threshold;
        private System.Windows.Forms.RadioButton rbt_ComputerVision_OCR;
        private System.Windows.Forms.RadioButton rbt_ComputerVision_Detect;
        private System.Windows.Forms.RadioButton rbt_ComputerVision_Analysis;
        private System.Windows.Forms.RadioButton rbt_Face_Detection;
        private System.Windows.Forms.RadioButton rbt_FormRecognizer_RecogReceipt;
        private System.Windows.Forms.Button button_LoadImage;
        private System.Windows.Forms.Button button_Execute;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_DispInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Info;
        private System.Windows.Forms.Label label_DispInfo;
    }
}
