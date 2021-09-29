using System;
using System.Collections.Generic;
using System.Drawing;

using static FileSystem.FileSystem;
using System.Windows.Forms;
using MicrosoftCognitiveServices;

namespace ComputerVision
{
    /// <summary>
    /// 【影像分析】結果及GUI相關操作 
    /// </summary>
    public class GUI_ComputerVision_Analysis
    {
        #region 參數

        public List<Rectangle> DetObj_ListRect { get; set; } = new List<Rectangle>();

        public List<Color> DetObj_ListColor { get; set; } = new List<Color>();

        public List<string> DetObj_ListInfo { get; set; } = new List<string>();

        public string Info { get; set; } = "";

        #endregion

        public GUI_ComputerVision_Analysis() { }

        #region 方法

        public void Clear()
        {
            this.DetObj_ListRect.Clear();
            this.DetObj_ListColor.Clear();
            this.DetObj_ListInfo.Clear();
            this.Info = "";
        }

        public void Parse(ImageAnalysisResult result)
        {
            this.Clear();
            if (result?.RequestId_ != null)
            {
                #region Object (Rectangle + Color + Discription)

                int i = 0;
                if (result.Brands != null)
                {
                    foreach (var obj in result.Brands)
                    {
                        Rectangle rect = new Rectangle(obj.Rectangle.X, obj.Rectangle.Y, obj.Rectangle.W, obj.Rectangle.H);
                        this.DetObj_ListRect.Add(rect);

                        this.DetObj_ListColor.Add(ColorArr[i]);
                        i = (i == ColorArr.Length - 1) ? 0 : ++i;
                        this.DetObj_ListInfo.Add($"Logo of {obj.Name} with confidence {obj.Confidence:0.##}");
                    }
                }
                if (result.Faces_ != null)
                {
                    foreach (var obj in result.Faces_)
                    {
                        Rectangle rect = new Rectangle(obj.FaceRectangle_.Left_, obj.FaceRectangle_.Top_, obj.FaceRectangle_.Width_, obj.FaceRectangle_.Height_);
                        this.DetObj_ListRect.Add(rect);

                        this.DetObj_ListColor.Add(ColorArr[i]);
                        i = (i == ColorArr.Length - 1) ? 0 : ++i;
                        this.DetObj_ListInfo.Add($"A {obj.Gender_} of age {obj.Age_ + 1}"); // Jeff Revised!
                    }
                }
                if (result.Categories_ != null)
                {
                    foreach (var category in result.Categories_)
                    {
                        if (category.Detail_?.Celebrities != null)
                        {
                            foreach (var celeb in category.Detail_.Celebrities)
                            {
                                Rectangle rect = new Rectangle(celeb.FaceRectangle.Left_, celeb.FaceRectangle.Top_, celeb.FaceRectangle.Width_, celeb.FaceRectangle.Height_);
                                this.DetObj_ListRect.Add(rect);

                                this.DetObj_ListColor.Add(ColorArr[i]);
                                i = (i == ColorArr.Length - 1) ? 0 : ++i;
                                this.DetObj_ListInfo.Add($"{celeb.Name} with confidence {celeb.Confidence:0.##}");
                            }
                        }
                    }
                }

                #endregion

                #region Get information

                // Sunmarizes the image content.
                this.Info += "Summary:" + "\n";
                if (result.Description_ != null)
                {
                    foreach (var caption in result.Description_.Captions_)
                        this.Info += $" {caption.Text_} with confidence {caption.Confidence_:0.##}" + "\n";
                }
                else
                    this.Info += "  No Description..." + "\n";
                this.Info += "\n";

                // Display categories the image is divided into.
                this.Info += "Categories:" + "\n";
                foreach (var category in result.Categories_)
                    this.Info += $" {category.Name_} with confidence {category.Score_:0.##}" + "\n";
                this.Info += "\n";

                // Image tags and their confidence score
                this.Info += "Tags:" + "\n";
                if (result.Tags_ != null)
                {
                    foreach (var tag in result.Tags_)
                        this.Info += $" {tag.Name_} {tag.Confidence_:0.##}" + "\n";
                }
                else
                    this.Info += "  No Tags..." + "\n";
                this.Info += "\n";

                // Adult or racy content, if any.
                this.Info += "Adult:" + "\n";
                if (result.Adult_ != null)
                {
                    this.Info += $" Has adult content: {result.Adult_.IsAdultContent_} with confidence {result.Adult_.AdultScore_:0.##}" + "\n";
                    this.Info += $" Has racy content: {result.Adult_.IsRacyContent_} with confidence {result.Adult_.RacyScore_:0.##}" + "\n";
                }
                else
                    this.Info += "  No Adult..." + "\n";
                this.Info += "\n";

                // Identifies the color scheme.
                this.Info += "Color Scheme:" + "\n";
                if (result.Color_ != null)
                {
                    this.Info += "  Is black and white?: " + result.Color_.IsBWImg_ + "\n";
                    this.Info += "  Accent color: " + result.Color_.AccentColor_ + "\n";
                    this.Info += "  Dominant background color: " + result.Color_.DominantColorBackground_ + "\n";
                    this.Info += "  Dominant foreground color: " + result.Color_.DominantColorForeground_ + "\n";
                    this.Info += "  Dominant colors: " + string.Join(",", result.Color_.DominantColors_) + "\n";
                }
                else
                    this.Info += "  No Color..." + "\n";
                this.Info += "\n";

                // Popular landmarks in image, if any.
                this.Info += "Landmarks:" + "\n";
                foreach (var category in result.Categories_)
                {
                    if (category.Detail_?.Landmarks != null)
                    {
                        foreach (var landmark in category.Detail_.Landmarks)
                            this.Info += $" {landmark.Name} with confidence {landmark.Confidence:0.##}" + "\n";
                    }
                }
                this.Info += "\n";

                // Detects the image types.
                this.Info += "Image Type:" + "\n";
                if (result.ImageType != null)
                {
                    this.Info += "  Clip Art Type: " + result.ImageType.ClipArtType + "\n";
                    this.Info += "  Line Drawing Type: " + result.ImageType.LineDrawingType + "\n";
                }
                else
                    this.Info += "  No ImageType..." + "\n";

                #endregion
            }
        }

        public void Draw(Graphics g_, Point mousePoint, Point mousePoint_Windows, bool b_mouseMove = false, Label label = null, RichTextBox richTextBox = null)
        {
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

    /// <summary>
    /// 【物件偵測】結果及GUI相關操作 
    /// </summary>
    public class GUI_ComputerVision_Detect
    {
        #region 參數

        public List<Rectangle> DetObj_ListRect { get; set; } = new List<Rectangle>();

        public List<Color> DetObj_ListColor { get; set; } = new List<Color>();

        public List<string> DetObj_ListInfo { get; set; } = new List<string>();

        #endregion

        public GUI_ComputerVision_Detect() { }

        #region 方法

        public void Clear()
        {
            this.DetObj_ListRect.Clear();
            this.DetObj_ListColor.Clear();
            this.DetObj_ListInfo.Clear();
        }

        public void Parse(ImageAnalysisResult result)
        {
            this.Clear();
            if (result?.Objects != null)
            {
                #region Object (Rectangle + Color + Discription)

                int i = 0;
                foreach (var obj in result.Objects)
                {
                    Rectangle rect = new Rectangle(obj.Rectangle.X, obj.Rectangle.Y, obj.Rectangle.W, obj.Rectangle.H);
                    this.DetObj_ListRect.Add(rect);

                    this.DetObj_ListColor.Add(ColorArr[i]);
                    i = (i == ColorArr.Length - 1) ? 0 : ++i;
                    this.DetObj_ListInfo.Add($"{obj.ObjectProperty} with confidence {obj.Confidence:0.##}");
                }

                #endregion
            }
        }

        public void Draw(Graphics g_, Point mousePoint, Point mousePoint_Windows, bool b_mouseMove = false, 
                         PictureBox pictureBox_ = null, ContextMenuStrip contextMenuStrip_ = null, ToolStripMenuItem toolStripMenuItem_ = null, Label label = null)
        {
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
                    //if (pictureBox_ != null && contextMenuStrip_ != null && toolStripMenuItem_ != null)
                    //{
                    //    toolStripMenuItem_.Text = this.DetObj_ListInfo[i];
                    //    contextMenuStrip_.Show(pictureBox_, mousePoint);
                    //}
                    if (label != null)
                    {
                        label.Text = this.DetObj_ListInfo[i];
                        label.Location = mousePoint_Windows;
                        label.Visible = true;
                    }
                }
                g_.DrawRectangle(new Pen(color_), rect);
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

    /// <summary>
    /// 【文字擷取 (OCR)】結果及GUI相關操作 
    /// </summary>
    public class GUI_ComputerVision_OCR
    {
        #region 參數

        public List<PointF[]> DetObj_ListPointArr { get; set; } = new List<PointF[]>();

        public List<Region> DetObj_ListRegion { get; set; } = new List<Region>();

        public List<Color> DetObj_ListColor { get; set; } = new List<Color>();

        public List<string> DetObj_ListInfo { get; set; } = new List<string>();

        public string Info { get; set; } = "";

        #endregion

        public GUI_ComputerVision_OCR() { }

        #region 方法

        public void Clear()
        {
            this.DetObj_ListPointArr.Clear();
            this.DetObj_ListRegion.Clear();
            this.DetObj_ListColor.Clear();
            this.DetObj_ListInfo.Clear();
            this.Info = "";
        }

        public void Parse(ReadOperationResult result)
        {
            this.Clear();
            if (result?.AnalyzeResult?.ReadResults != null)
            {
                int i = 0;
                var readResults = result.AnalyzeResult.ReadResults;
                foreach (var page in readResults)
                {
                    foreach (var line in page.Lines) // 每一行
                    {
                        this.Info += line.Text + "\n";
                        foreach (var word in line.Words) // 每一字
                        {
                            this.DetObj_ListInfo.Add(word.Text + $"\nConfidence: {word.Confidence:0.##}");
                            this.DetObj_ListColor.Add(ColorArr[i]);
                            i = (i == ColorArr.Length - 1) ? 0 : ++i;

                            List<PointF> listPt = new List<PointF>();
                            for (int pt = 0; pt < word.BoundingBox.Count - 1; pt += 2)
                                listPt.Add(new PointF((float)word.BoundingBox[pt], (float)word.BoundingBox[pt + 1]));
                            PointF[] ptArray = listPt.ToArray();
                            this.DetObj_ListPointArr.Add(ptArray);
                            this.DetObj_ListRegion.Add(clsStaticTool.GetRegion_FromPolygon(ptArray));
                        }
                    }
                }
            }
        }

        public void Draw(Graphics g_, Point mousePoint, Point mousePoint_Windows, bool b_mouseMove = false, 
                         Label label = null, RichTextBox richTextBox = null)
        {
            int index = -1;
            if (b_mouseMove)
                index = this.Index_Point_InRegion(mousePoint);

            if (index == -1 && label != null)
                label.Visible = false;

            for (int i = 0; i < this.DetObj_ListColor.Count; i++)
            {
                Color color_ = this.DetObj_ListColor[i];
                PointF[] polygon = this.DetObj_ListPointArr[i];

                if (i == index)
                {
                    Color c = Color.FromArgb(64, color_);
                    g_.FillRegion(new SolidBrush(c), this.DetObj_ListRegion[i]);

                    // 顯示詳細資訊
                    if (label != null)
                    {
                        label.Text = this.DetObj_ListInfo[i];
                        label.Location = mousePoint_Windows;
                        label.Visible = true;
                    }
                }
                g_.DrawPolygon(new Pen(color_), polygon);
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
                Region reg = this.DetObj_ListRegion[i];
                if (reg.IsVisible(mousePoint)) // 判斷滑鼠點是否位於Region內
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
