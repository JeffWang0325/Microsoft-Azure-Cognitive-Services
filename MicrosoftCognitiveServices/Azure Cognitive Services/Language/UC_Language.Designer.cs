
namespace MicrosoftCognitiveServices.Azure_Cognitive_Services.Language
{
    partial class UC_Language
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Language));
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton_QnAMaker = new System.Windows.Forms.RadioButton();
            this.radioButton_LUIS = new System.Windows.Forms.RadioButton();
            this.textBox_Message = new System.Windows.Forms.TextBox();
            this.richTextBox_Answer = new System.Windows.Forms.RichTextBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Search = new System.Windows.Forms.Button();
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
            this.label1.TabIndex = 11;
            this.label1.Text = "服務 :";
            // 
            // radioButton_QnAMaker
            // 
            this.radioButton_QnAMaker.AutoSize = true;
            this.radioButton_QnAMaker.Checked = true;
            this.radioButton_QnAMaker.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton_QnAMaker.ForeColor = System.Drawing.Color.Blue;
            this.radioButton_QnAMaker.Image = ((System.Drawing.Image)(resources.GetObject("radioButton_QnAMaker.Image")));
            this.radioButton_QnAMaker.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButton_QnAMaker.Location = new System.Drawing.Point(330, 6);
            this.radioButton_QnAMaker.Name = "radioButton_QnAMaker";
            this.radioButton_QnAMaker.Size = new System.Drawing.Size(220, 36);
            this.radioButton_QnAMaker.TabIndex = 18;
            this.radioButton_QnAMaker.TabStop = true;
            this.radioButton_QnAMaker.Text = "        製作問與答的人員";
            this.radioButton_QnAMaker.UseVisualStyleBackColor = true;
            this.radioButton_QnAMaker.CheckedChanged += new System.EventHandler(this.radioButton_Service_CheckedChanged);
            // 
            // radioButton_LUIS
            // 
            this.radioButton_LUIS.AutoSize = true;
            this.radioButton_LUIS.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButton_LUIS.ForeColor = System.Drawing.Color.Blue;
            this.radioButton_LUIS.Image = ((System.Drawing.Image)(resources.GetObject("radioButton_LUIS.Image")));
            this.radioButton_LUIS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radioButton_LUIS.Location = new System.Drawing.Point(580, 6);
            this.radioButton_LUIS.Name = "radioButton_LUIS";
            this.radioButton_LUIS.Size = new System.Drawing.Size(144, 36);
            this.radioButton_LUIS.TabIndex = 19;
            this.radioButton_LUIS.Text = "        語言理解";
            this.radioButton_LUIS.UseVisualStyleBackColor = true;
            this.radioButton_LUIS.CheckedChanged += new System.EventHandler(this.radioButton_Service_CheckedChanged);
            // 
            // textBox_Message
            // 
            this.textBox_Message.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_Message.Location = new System.Drawing.Point(10, 65);
            this.textBox_Message.Name = "textBox_Message";
            this.textBox_Message.Size = new System.Drawing.Size(1200, 35);
            this.textBox_Message.TabIndex = 21;
            // 
            // richTextBox_Answer
            // 
            this.richTextBox_Answer.Location = new System.Drawing.Point(10, 110);
            this.richTextBox_Answer.Name = "richTextBox_Answer";
            this.richTextBox_Answer.Size = new System.Drawing.Size(1565, 790);
            this.richTextBox_Answer.TabIndex = 20;
            this.richTextBox_Answer.Text = "";
            // 
            // button_Clear
            // 
            this.button_Clear.BackColor = System.Drawing.Color.Blue;
            this.button_Clear.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_Clear.ForeColor = System.Drawing.Color.White;
            this.button_Clear.Image = ((System.Drawing.Image)(resources.GetObject("button_Clear.Image")));
            this.button_Clear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Clear.Location = new System.Drawing.Point(1445, 6);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(130, 71);
            this.button_Clear.TabIndex = 30;
            this.button_Clear.Text = "清空";
            this.button_Clear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_Clear.UseVisualStyleBackColor = false;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_Search
            // 
            this.button_Search.BackColor = System.Drawing.Color.Green;
            this.button_Search.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button_Search.ForeColor = System.Drawing.Color.Yellow;
            this.button_Search.Image = ((System.Drawing.Image)(resources.GetObject("button_Search.Image")));
            this.button_Search.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Search.Location = new System.Drawing.Point(1290, 6);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(130, 71);
            this.button_Search.TabIndex = 29;
            this.button_Search.Text = "搜尋";
            this.button_Search.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_Search.UseVisualStyleBackColor = false;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // UC_Language
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.button_Search);
            this.Controls.Add(this.textBox_Message);
            this.Controls.Add(this.richTextBox_Answer);
            this.Controls.Add(this.radioButton_LUIS);
            this.Controls.Add(this.radioButton_QnAMaker);
            this.Controls.Add(this.label1);
            this.Name = "UC_Language";
            this.Size = new System.Drawing.Size(1590, 900);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_QnAMaker;
        private System.Windows.Forms.RadioButton radioButton_LUIS;
        private System.Windows.Forms.TextBox textBox_Message;
        private System.Windows.Forms.RichTextBox richTextBox_Answer;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Button button_Search;
    }
}
