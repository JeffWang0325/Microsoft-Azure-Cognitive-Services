using System;
using System.Collections.Generic;
using System.Drawing;

using static FileSystem.FileSystem;
using System.Windows.Forms;

namespace CustomVision
{
    /// <summary>
    /// 【自訂視覺-分類, 物件偵測】結果及GUI相關操作 
    /// </summary>
    public class GUI_CustomVision
    {
        #region 參數

        public List<Rectangle> DetObj_ListRect { get; set; } = new List<Rectangle>();

        public List<Color> DetObj_ListColor { get; set; } = new List<Color>();

        public List<string> DetObj_ListInfo { get; set; } = new List<string>();

        public string Info { get; set; } = "";



        /// <summary>
        /// Probability Threshold
        /// </summary>
        private double Threshold { get; set; } = 0.5;

        /// <summary>
        /// 更新 Threshold 時，會使用來做 Parse()
        /// </summary>
        private CustomVisionResult CustomVisionResult_ { get; set; } = new CustomVisionResult();

        #endregion

        public GUI_CustomVision() { }

        #region 方法

        public void Clear(bool b_clear_CustomVisionResult = false)
        {
            this.DetObj_ListRect.Clear();
            this.DetObj_ListColor.Clear();
            this.DetObj_ListInfo.Clear();
            this.Info = "";
            if (b_clear_CustomVisionResult)
                this.CustomVisionResult_ = new CustomVisionResult();
        }

        public void Parse(double threshold = 0.5, CustomVisionResult result = null)
        {
            this.Clear();
            this.Threshold = threshold;
            if (result != null)
                this.CustomVisionResult_ = result;

            if (this.CustomVisionResult_?.Id != null)
            {
                if (this.CustomVisionResult_.Predictions != null)
                {
                    int i = 0;
                    foreach (var prediction in this.CustomVisionResult_.Predictions)
                    {
                        if (prediction?.TagName!= null && prediction?.Probability != null)
                        {
                            double score = prediction.Probability;
                            if (score >= this.Threshold)
                            {
                                #region Get information

                                string info = $"{prediction.TagName} with probability {score:0.###}";
                                this.Info += info + "\n";

                                #endregion

                                #region Object (Rectangle + Color + Discription)

                                var obj = prediction.BoundingBox;
                                if (obj != null) // For Object Detection
                                {
                                    int w = this.CustomVisionResult_.ImageWidth,
                                        h = this.CustomVisionResult_.ImageHeight;
                                    Rectangle rect = new Rectangle((int)(obj.Left * w + 0.5), (int)(obj.Top * h + 0.5), (int)(obj.Width * w + 0.5), (int)(obj.Height * h + 0.5));
                                    this.DetObj_ListRect.Add(rect);

                                    this.DetObj_ListColor.Add(ColorArr[i]);
                                    i = (i == ColorArr.Length - 1) ? 0 : ++i;
                                    this.DetObj_ListInfo.Add(info);
                                }

                                #endregion
                            }
                        }
                    }
                }
            }
        }

        public void Draw(Graphics g_, Point mousePoint, Point mousePoint_Windows, bool b_mouseMove = false, Label label = null, RichTextBox richTextBox = null, double threshold = 0.5)
        {
            #region 更新數值

            if (this.Threshold != threshold)
                this.Parse(threshold);

            #endregion

            int index = -1;
            if (b_mouseMove)
                index = this.Index_Point_InRegion(mousePoint);

            if (index == -1 && label != null)
                label.Visible = false;

            for (int i = 0; i < this.DetObj_ListColor.Count; i++)
            {
                Color color_ = this.DetObj_ListColor[i];
                Rectangle rect = this.DetObj_ListRect[i];

                if (i == index)
                {
                    Color c = Color.FromArgb(64, color_);
                    g_.FillRectangle(new SolidBrush(c), rect);

                    // 顯示詳細資訊
                    if (label != null)
                    {
                        label.Text = this.DetObj_ListInfo[i];
                        label.Location = mousePoint_Windows;
                        label.Visible = true;
                    }
                }
                g_.DrawRectangle(new Pen(color_), rect);
            }

            // 顯示所有文字
            if (richTextBox != null)
            {
                if (richTextBox.Text != this.Info)
                {
                    richTextBox.Clear();
                    richTextBox.AppendText(this.Info);
                }
            }
        }

        /// <summary>
        /// 判斷座標點位於 Region 的 Index (第1次出現)
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <returns>-1 if point NOT in any regions</returns>
        public int Index_Point_InRegion(Point mousePoint)
        {
            int index = -1;
            for (int i = 0; i < this.DetObj_ListColor.Count; i++)
            {
                Rectangle rect = this.DetObj_ListRect[i];
                if (rect.Contains(mousePoint)) // 判斷滑鼠點是否位於Region內
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        #endregion
    }
}
