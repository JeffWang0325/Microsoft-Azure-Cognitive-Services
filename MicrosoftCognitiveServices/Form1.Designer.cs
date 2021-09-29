
namespace MicrosoftCognitiveServices
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Tool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ConfigSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Language = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_English = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Chinese = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.panel_Language = new System.Windows.Forms.Panel();
            this.panel_Vision = new System.Windows.Forms.Panel();
            this.panel_Speech = new System.Windows.Forms.Panel();
            this.radioButton_Language = new System.Windows.Forms.RadioButton();
            this.radioButton_Vision = new System.Windows.Forms.RadioButton();
            this.radioButton_Speech = new System.Windows.Forms.RadioButton();
            this.button_Exit = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File,
            this.toolStripMenuItem_Tool});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1584, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_File
            // 
            this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Quit});
            this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem_File.Text = "檔案";
            // 
            // toolStripMenuItem_Quit
            // 
            this.toolStripMenuItem_Quit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_Quit.Image")));
            this.toolStripMenuItem_Quit.Name = "toolStripMenuItem_Quit";
            this.toolStripMenuItem_Quit.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem_Quit.Text = "結束(✖)";
            this.toolStripMenuItem_Quit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // toolStripMenuItem_Tool
            // 
            this.toolStripMenuItem_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ConfigSetting,
            this.toolStripMenuItem_Language});
            this.toolStripMenuItem_Tool.Name = "toolStripMenuItem_Tool";
            this.toolStripMenuItem_Tool.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem_Tool.Text = "工具";
            // 
            // toolStripMenuItem_ConfigSetting
            // 
            this.toolStripMenuItem_ConfigSetting.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_ConfigSetting.Image")));
            this.toolStripMenuItem_ConfigSetting.Name = "toolStripMenuItem_ConfigSetting";
            this.toolStripMenuItem_ConfigSetting.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem_ConfigSetting.Text = "Config 設定";
            this.toolStripMenuItem_ConfigSetting.Click += new System.EventHandler(this.toolStripMenuItem_ConfigSetting_Click);
            // 
            // toolStripMenuItem_Language
            // 
            this.toolStripMenuItem_Language.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_English,
            this.toolStripMenuItem_Chinese});
            this.toolStripMenuItem_Language.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_Language.Image")));
            this.toolStripMenuItem_Language.Name = "toolStripMenuItem_Language";
            this.toolStripMenuItem_Language.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem_Language.Text = "語言設定";
            // 
            // toolStripMenuItem_English
            // 
            this.toolStripMenuItem_English.Name = "toolStripMenuItem_English";
            this.toolStripMenuItem_English.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItem_English.Text = "English";
            this.toolStripMenuItem_English.Click += new System.EventHandler(this.toolStripMenuItem_English_Click);
            // 
            // toolStripMenuItem_Chinese
            // 
            this.toolStripMenuItem_Chinese.Name = "toolStripMenuItem_Chinese";
            this.toolStripMenuItem_Chinese.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItem_Chinese.Text = "中文";
            this.toolStripMenuItem_Chinese.Click += new System.EventHandler(this.toolStripMenuItem_Chinese_Click);
            // 
            // pictureBox_Logo
            // 
            this.pictureBox_Logo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Logo.Image")));
            this.pictureBox_Logo.Location = new System.Drawing.Point(10, 25);
            this.pictureBox_Logo.Name = "pictureBox_Logo";
            this.pictureBox_Logo.Size = new System.Drawing.Size(155, 142);
            this.pictureBox_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Logo.TabIndex = 14;
            this.pictureBox_Logo.TabStop = false;
            // 
            // panel_Language
            // 
            this.panel_Language.Location = new System.Drawing.Point(0, 110);
            this.panel_Language.Name = "panel_Language";
            this.panel_Language.Size = new System.Drawing.Size(1590, 900);
            this.panel_Language.TabIndex = 15;
            // 
            // panel_Vision
            // 
            this.panel_Vision.Location = new System.Drawing.Point(0, 110);
            this.panel_Vision.Name = "panel_Vision";
            this.panel_Vision.Size = new System.Drawing.Size(1590, 900);
            this.panel_Vision.TabIndex = 16;
            this.panel_Vision.Visible = false;
            // 
            // panel_Speech
            // 
            this.panel_Speech.Location = new System.Drawing.Point(0, 110);
            this.panel_Speech.Name = "panel_Speech";
            this.panel_Speech.Size = new System.Drawing.Size(1590, 900);
            this.panel_Speech.TabIndex = 16;
            this.panel_Speech.Visible = false;
            // 
            // radioButton_Language
            // 
            this.radioButton_Language.AutoSize = true;
            this.radioButton_Language.Checked = true;
            this.radioButton_Language.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton_Language.ForeColor = System.Drawing.Color.SteelBlue;
            this.radioButton_Language.Image = ((System.Drawing.Image)(resources.GetObject("radioButton_Language.Image")));
            this.radioButton_Language.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButton_Language.Location = new System.Drawing.Point(180, 25);
            this.radioButton_Language.Name = "radioButton_Language";
            this.radioButton_Language.Size = new System.Drawing.Size(219, 81);
            this.radioButton_Language.TabIndex = 17;
            this.radioButton_Language.TabStop = true;
            this.radioButton_Language.Text = "               語言";
            this.radioButton_Language.UseVisualStyleBackColor = true;
            this.radioButton_Language.CheckedChanged += new System.EventHandler(this.radioButton_Service_CheckedChanged);
            // 
            // radioButton_Vision
            // 
            this.radioButton_Vision.AutoSize = true;
            this.radioButton_Vision.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton_Vision.ForeColor = System.Drawing.Color.SteelBlue;
            this.radioButton_Vision.Image = ((System.Drawing.Image)(resources.GetObject("radioButton_Vision.Image")));
            this.radioButton_Vision.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButton_Vision.Location = new System.Drawing.Point(500, 25);
            this.radioButton_Vision.Name = "radioButton_Vision";
            this.radioButton_Vision.Size = new System.Drawing.Size(219, 81);
            this.radioButton_Vision.TabIndex = 18;
            this.radioButton_Vision.Text = "               辨識";
            this.radioButton_Vision.UseVisualStyleBackColor = true;
            this.radioButton_Vision.CheckedChanged += new System.EventHandler(this.radioButton_Service_CheckedChanged);
            // 
            // radioButton_Speech
            // 
            this.radioButton_Speech.AutoSize = true;
            this.radioButton_Speech.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton_Speech.ForeColor = System.Drawing.Color.SteelBlue;
            this.radioButton_Speech.Image = ((System.Drawing.Image)(resources.GetObject("radioButton_Speech.Image")));
            this.radioButton_Speech.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButton_Speech.Location = new System.Drawing.Point(820, 25);
            this.radioButton_Speech.Name = "radioButton_Speech";
            this.radioButton_Speech.Size = new System.Drawing.Size(219, 81);
            this.radioButton_Speech.TabIndex = 19;
            this.radioButton_Speech.Text = "               語音";
            this.radioButton_Speech.UseVisualStyleBackColor = true;
            this.radioButton_Speech.CheckedChanged += new System.EventHandler(this.radioButton_Service_CheckedChanged);
            // 
            // button_Exit
            // 
            this.button_Exit.BackColor = System.Drawing.Color.Red;
            this.button_Exit.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_Exit.ForeColor = System.Drawing.Color.White;
            this.button_Exit.Location = new System.Drawing.Point(1460, 49);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(112, 40);
            this.button_Exit.TabIndex = 20;
            this.button_Exit.Text = "離開";
            this.button_Exit.UseVisualStyleBackColor = false;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 1015);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.pictureBox_Logo);
            this.Controls.Add(this.radioButton_Speech);
            this.Controls.Add(this.radioButton_Vision);
            this.Controls.Add(this.radioButton_Language);
            this.Controls.Add(this.panel_Language);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel_Speech);
            this.Controls.Add(this.panel_Vision);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MicrosoftCognitiveServices";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Quit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tool;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConfigSetting;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Language;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_English;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Chinese;
        private System.Windows.Forms.PictureBox pictureBox_Logo;
        private System.Windows.Forms.Panel panel_Language;
        private System.Windows.Forms.Panel panel_Vision;
        private System.Windows.Forms.Panel panel_Speech;
        private System.Windows.Forms.RadioButton radioButton_Language;
        private System.Windows.Forms.RadioButton radioButton_Vision;
        private System.Windows.Forms.RadioButton radioButton_Speech;
        private System.Windows.Forms.Button button_Exit;
    }
}

