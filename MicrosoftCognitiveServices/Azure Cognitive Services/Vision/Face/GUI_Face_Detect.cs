using System;
using System.Collections.Generic;
using System.Drawing;
using static FileSystem.FileSystem;
using System.Windows.Forms;

using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace Face
{
    /// <summary>
    /// 【臉部偵測】結果及GUI相關操作 
    /// </summary>
    public class GUI_Face_Detect
    {
        #region 參數

        public List<Rectangle> DetObj_ListRect { get; set; } = new List<Rectangle>();

        public List<Color> DetObj_ListColor { get; set; } = new List<Color>();

        public List<string> DetObj_ListInfo { get; set; } = new List<string>();

        #endregion

        public GUI_Face_Detect() { }

        #region 方法

        public void Clear()
        {
            this.DetObj_ListRect.Clear();
            this.DetObj_ListColor.Clear();
            this.DetObj_ListInfo.Clear();
        }

        public void Parse(List<DetectedFace> result)
        {
            this.Clear();
            if (result?.Count > 0)
            {
                int i = 0;
                foreach (var face in result)
                {
                    #region Object (Rectangle + Color + Discription)

                    Rectangle rect = new Rectangle(face.FaceRectangle.Left, face.FaceRectangle.Top, face.FaceRectangle.Width, face.FaceRectangle.Height);
                    this.DetObj_ListRect.Add(rect);

                    this.DetObj_ListColor.Add(ColorArr[i]);
                    i = (i == ColorArr.Length - 1) ? 0 : ++i;

                    // Get information
                    string info = "";
                    #region Parse all attributes, and write into <info>

                    // Get accessories of the faces
                    List<Accessory> accessoriesList = (List<Accessory>)face.FaceAttributes.Accessories;
                    int count = face.FaceAttributes.Accessories.Count;
                    string accessory; 
                    string[] accessoryArray = new string[count];
                    if (count == 0)
                        accessory = "NoAccessories";
                    else
                    {
                        for (int j = 0; j < count; ++j) { accessoryArray[j] = accessoriesList[j].Type.ToString(); }
                        accessory = string.Join(",", accessoryArray);
                    }
                    info += $"Accessories : {accessory}" + "\n";

                    // Get face other attributes
                    info += $"Age : {face.FaceAttributes.Age}" + "\n";
                    info += $"Blur : {face.FaceAttributes.Blur.BlurLevel}" + "\n";

                    // Get emotion on the face
                    string emotionType = string.Empty;
                    double emotionValue = 0.0;
                    Emotion emotion = face.FaceAttributes.Emotion;
                    if (emotion.Anger > emotionValue) { emotionValue = emotion.Anger; emotionType = "Anger"; }
                    if (emotion.Contempt > emotionValue) { emotionValue = emotion.Contempt; emotionType = "Contempt"; }
                    if (emotion.Disgust > emotionValue) { emotionValue = emotion.Disgust; emotionType = "Disgust"; }
                    if (emotion.Fear > emotionValue) { emotionValue = emotion.Fear; emotionType = "Fear"; }
                    if (emotion.Happiness > emotionValue) { emotionValue = emotion.Happiness; emotionType = "Happiness"; }
                    if (emotion.Neutral > emotionValue) { emotionValue = emotion.Neutral; emotionType = "Neutral"; }
                    if (emotion.Sadness > emotionValue) { emotionValue = emotion.Sadness; emotionType = "Sadness"; }
                    if (emotion.Surprise > emotionValue) { emotionType = "Surprise"; }
                    info += $"Emotion : {emotionType}" + "\n";

                    // Get more face attributes
                    info += $"Exposure : {face.FaceAttributes.Exposure.ExposureLevel}" + "\n";
                    info += $"FacialHair : {string.Format("{0}", face.FaceAttributes.FacialHair.Moustache + face.FaceAttributes.FacialHair.Beard + face.FaceAttributes.FacialHair.Sideburns > 0 ? "Yes" : "No")}" + "\n";
                    info += $"Gender : {face.FaceAttributes.Gender}" + "\n";
                    info += $"Glasses : {face.FaceAttributes.Glasses}" + "\n";

                    // Get hair color
                    Hair hair = face.FaceAttributes.Hair;
                    string color = null;
                    if (hair.HairColor.Count == 0) { if (hair.Invisible) { color = "Invisible"; } else { color = "Bald"; } }
                    HairColorType returnColor = HairColorType.Unknown;
                    double maxConfidence = 0.0f;
                    foreach (HairColor hairColor in hair.HairColor)
                    {
                        if (hairColor.Confidence <= maxConfidence) { continue; }
                        maxConfidence = hairColor.Confidence; returnColor = hairColor.Color; color = returnColor.ToString();
                    }
                    info += $"Hair : {color}" + "\n";

                    // Get more attributes
                    info += $"HeadPose : {string.Format("Pitch: {0}, Roll: {1}, Yaw: {2}", Math.Round(face.FaceAttributes.HeadPose.Pitch, 2), Math.Round(face.FaceAttributes.HeadPose.Roll, 2), Math.Round(face.FaceAttributes.HeadPose.Yaw, 2))}" + "\n";
                    info += $"Makeup : {string.Format("{0}", (face.FaceAttributes.Makeup.EyeMakeup || face.FaceAttributes.Makeup.LipMakeup) ? "Yes" : "No")}" + "\n";
                    info += $"Noise : {face.FaceAttributes.Noise.NoiseLevel}" + "\n";
                    info += $"Occlusion : {string.Format("EyeOccluded: {0}", face.FaceAttributes.Occlusion.EyeOccluded ? "Yes" : "No")} " +
                        $" {string.Format("ForeheadOccluded: {0}", face.FaceAttributes.Occlusion.ForeheadOccluded ? "Yes" : "No")}   {string.Format("MouthOccluded: {0}", face.FaceAttributes.Occlusion.MouthOccluded ? "Yes" : "No")}" + "\n";
                    info += $"Smile : {face.FaceAttributes.Smile}" + "\n";

                    #endregion
                    this.DetObj_ListInfo.Add(info);

                    #endregion
                }
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
                    string info = this.DetObj_ListInfo[i];
                    if (label != null)
                    {
                        label.Text = info;
                        label.Location = mousePoint_Windows;
                        label.Visible = true;
                    }

                    // 顯示所有文字
                    if (richTextBox != null)
                    {
                        if (richTextBox.Text != info)
                        {
                            richTextBox.Clear();
                            richTextBox.AppendText(info);
                        }
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
}
