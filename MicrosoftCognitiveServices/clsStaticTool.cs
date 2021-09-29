using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace MicrosoftCognitiveServices
{
    public static class clsStaticTool
    {
        /// <summary>
        /// 寫入文字到 RichTextBox
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="msg1"></param>
        /// <param name="color1"></param>
        /// <param name="b_ScrollToCaret"></param>
        /// <param name="b_returnOrigColor">是否回復到原先顏色</param>
        /// <param name="msg2"></param>
        /// <param name="color2"></param>
        public static void WriteMsg_RichTextBox(RichTextBox rtb, string msg1, Color color1 = new Color(), bool b_ScrollToCaret = false, bool b_returnOrigColor = true,
                                                                 string msg2 = null, Color color2 = new Color())
        {
            Color OrigColor = rtb.SelectionColor;
            rtb.SelectionColor = color1;
            rtb.AppendText(msg1);
            if (msg2 != null)
            {
                rtb.SelectionColor = color2;
                rtb.AppendText(msg2);
            }

            // 回復到原先顏色
            if (b_returnOrigColor)
                rtb.SelectionColor = OrigColor;

            if (b_ScrollToCaret)
                rtb.ScrollToCaret();
        }

        /// <summary>
        /// 寫入文字到 RichTextBox (跨執行緒處理控制項)
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="msg1"></param>
        /// <param name="color1"></param>
        /// <param name="b_ScrollToCaret"></param>
        /// <param name="b_returnOrigColor">是否回復到原先顏色</param>
        /// <param name="msg2"></param>
        /// <param name="color2"></param>
        public static void WriteMsg_RichTextBox_Invoke(RichTextBox rtb, string msg1, Color color1 = new Color(), bool b_ScrollToCaret = false, bool b_returnOrigColor = true,
                                                                 string msg2 = null, Color color2 = new Color())
        {
            Color OrigColor = Color.Red;
            rtb.BeginInvoke(new Action(() => OrigColor = rtb.SelectionColor));
            rtb.BeginInvoke(new Action(() => rtb.SelectionColor = color1));
            rtb.BeginInvoke(new Action(() => rtb.AppendText(msg1)));
            if (msg2 != null)
            {
                rtb.BeginInvoke(new Action(() => rtb.SelectionColor = color2));
                rtb.BeginInvoke(new Action(() => rtb.AppendText(msg2)));
            }

            // 回復到原先顏色
            if (b_returnOrigColor)
                rtb.BeginInvoke(new Action(() => rtb.SelectionColor = OrigColor));

            if (b_ScrollToCaret)
                rtb.BeginInvoke(new Action(() => rtb.ScrollToCaret()));
        }

        /// <summary>
        /// 載入 XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PathFile"></param>
        /// <param name="Recipe"></param>
        /// <returns></returns>
        public static bool LoadXML<T>(string PathFile, out T Recipe) // (20200429) Jeff Revised!
        {
            bool b_status_ = false;
            //Recipe = new T();
            Recipe = default(T); // i.e. Recipe = null

            if (File.Exists(PathFile) == false)
                return false;

            try
            {
                //XmlSerializer XmlS = new XmlSerializer(Recipe.GetType());
                XmlSerializer XmlS = new XmlSerializer(typeof(T));
                Stream S = File.Open(PathFile, FileMode.Open);
                Recipe = (T)XmlS.Deserialize(S);
                S.Close();
                b_status_ = true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return b_status_;
        }

        /// <summary>
        /// 儲存 XML
        /// </summary>
        /// <param name="SrcProduct"></param>
        /// <param name="PathFile"></param>
        /// <returns></returns>
        public static bool SaveXML(Object SrcProduct, string PathFile) // (20200812) Jeff Revised!
        {
            bool b_status_ = false;
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(PathFile));

            try
            {
                // Link: https://stackoverflow.com/questions/760262/xmlserializer-remove-unnecessary-xsi-and-xsd-namespaces
                // Create our own namespaces for the output
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces(); // (20200812) Jeff Revised!

                // Add an empty namespace and empty value
                ns.Add("", ""); // (20200812) Jeff Revised!

                XmlSerializer XmlS = new XmlSerializer(SrcProduct.GetType());
                Stream S = File.Open(PathFile, FileMode.Create);
                //XmlS.Serialize(S, SrcProduct); // 會多一串字: xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                XmlS.Serialize(S, SrcProduct, ns); // (20200812) Jeff Revised!
                S.Close();
                b_status_ = true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }

            return b_status_;
        }

        /// <summary>
        /// 控制項顯示影像之背景顏色設定為透明
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="B_BackgroundImage"></param>
        /// <returns></returns>
        public static bool Control_DispImg_Transparent(Control ctrl, bool B_BackgroundImage = true) // (20210310) Jeff Revised!
        {
            bool b_status_ = false;
            try
            {
                Bitmap bm = null;
                if (B_BackgroundImage)
                {
                    bm = (Bitmap)ctrl.BackgroundImage;
                    bm.MakeTransparent();
                    if (ctrl.InvokeRequired)
                        ctrl.BeginInvoke(new Action(() => ctrl.BackgroundImage = bm));
                    else
                        ctrl.BackgroundImage = bm;
                }
                else
                {
                    if (ctrl is CheckBox)
                    {
                        CheckBox cbx = ctrl as CheckBox;
                        bm = (Bitmap)cbx.Image;
                        bm.MakeTransparent();
                        if (cbx.InvokeRequired)
                            cbx.BeginInvoke(new Action(() => cbx.Image = bm));
                        else
                            cbx.Image = bm;
                    }
                    else if (ctrl is Button)
                    {
                        Button bt = ctrl as Button;
                        bm = (Bitmap)bt.Image;
                        bm.MakeTransparent();
                        if (bt.InvokeRequired)
                            bt.BeginInvoke(new Action(() => bt.Image = bm));
                        else
                            bt.Image = bm;
                    }
                    else if (ctrl is RadioButton) // (20210310) Jeff Revised!
                    {
                        RadioButton rbt = ctrl as RadioButton;
                        bm = (Bitmap)rbt.Image;
                        bm.MakeTransparent();
                        if (rbt.InvokeRequired)
                            rbt.BeginInvoke(new Action(() => rbt.Image = bm));
                        else
                            rbt.Image = bm;
                    }
                    else if (ctrl is PictureBox) // (20210224) Jeff Revised!
                    {
                        PictureBox pb = ctrl as PictureBox;
                        bm = (Bitmap)pb.Image;
                        bm.MakeTransparent();
                        if (pb.InvokeRequired)
                            pb.BeginInvoke(new Action(() => pb.Image = bm));
                        else
                            pb.Image = bm;
                    }
                    else
                        return false;
                }

                b_status_ = true;
            }
            catch (Exception ex)
            { }
            return b_status_;
        }

        /// <summary>
		/// 取得影像長寬
		/// Note: 由於要先載入影像，因此效率較差!
		/// [C#]使用BitmapDecoder快速讀取圖檔的大小: https://dotblogs.com.tw/larrynung/2012/09/05/74627
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static Size GetImageSize2(string file)
        {
            using (var image = Bitmap.FromFile(file))
            {
                return image.Size;
            }
        }

        /// <summary>
        /// Convert Polygon to Region
        /// </summary>
        /// <param name="ptArray"></param>
        /// <returns></returns>
        public static Region GetRegion_FromPolygon(PointF[] ptArray)
        {
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            graphicsPath.AddPolygon(ptArray);
            return new Region(graphicsPath);
        }
    }
}
