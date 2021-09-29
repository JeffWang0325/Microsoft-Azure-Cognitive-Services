using System;
using System.Collections.Generic;
using System.Drawing;

using static FileSystem.FileSystem;
using System.Windows.Forms;
using MicrosoftCognitiveServices;

using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Training;

namespace FormRecognizer
{
    /// <summary>
    /// 【表單辨識器】結果及GUI相關操作 
    /// </summary>
    public class GUI_FormRecognizer
    {
        #region 參數

        public List<PointF[]> DetObj_ListPointArr { get; set; } = new List<PointF[]>();

        public List<Region> DetObj_ListRegion { get; set; } = new List<Region>();

        public List<Color> DetObj_ListColor { get; set; } = new List<Color>();

        public List<string> DetObj_ListInfo { get; set; } = new List<string>();

        public string Info { get; set; } = "";

        #endregion

        public GUI_FormRecognizer() { }

        #region 方法

        public void Clear()
        {
            this.DetObj_ListPointArr.Clear();
            this.DetObj_ListRegion.Clear();
            this.DetObj_ListColor.Clear();
            this.DetObj_ListInfo.Clear();
            this.Info = "";
        }

        public void Parse(RecognizedFormCollection result)
        {
            this.Clear();
            if (result != null)
            {
                int i = 0;
                var receipts = result;
                foreach (RecognizedForm receipt in receipts)
                {
                    if (receipt.Fields.TryGetValue("MerchantName", out FormField merchantNameField))
                    {
                        if (merchantNameField.Value.ValueType == FieldValueType.String)
                        {
                            string merchantName = merchantNameField.Value.AsString();

                            this.Info += $"Merchant Name: '{merchantName}', with confidence {merchantNameField.Confidence:0.##}" + "\n";
                            FieldBoundingBox fieldBoundingBox = merchantNameField.ValueData.BoundingBox;
                            PointF[] ptArray = new PointF[] { fieldBoundingBox[0], fieldBoundingBox[1], fieldBoundingBox[2], fieldBoundingBox[3] };
                            this.DetObj_ListPointArr.Add(ptArray);
                            this.DetObj_ListRegion.Add(clsStaticTool.GetRegion_FromPolygon(ptArray));

                            this.DetObj_ListInfo.Add(merchantNameField.ValueData.Text);
                            this.DetObj_ListColor.Add(ColorArr[i]);
                            i = (i == ColorArr.Length - 1) ? 0 : ++i;
                        }
                    }

                    if (receipt.Fields.TryGetValue("TransactionDate", out FormField transactionDateField))
                    {
                        if (transactionDateField.Value.ValueType == FieldValueType.Date)
                        {
                            DateTime transactionDate = transactionDateField.Value.AsDate();

                            this.Info += $"Transaction Date: '{transactionDate}', with confidence {transactionDateField.Confidence:0.##}" + "\n";
                            FieldBoundingBox fieldBoundingBox = transactionDateField.ValueData.BoundingBox;
                            PointF[] ptArray = new PointF[] { fieldBoundingBox[0], fieldBoundingBox[1], fieldBoundingBox[2], fieldBoundingBox[3] };
                            this.DetObj_ListPointArr.Add(ptArray);
                            this.DetObj_ListRegion.Add(clsStaticTool.GetRegion_FromPolygon(ptArray));

                            this.DetObj_ListInfo.Add(transactionDateField.ValueData.Text);
                            this.DetObj_ListColor.Add(ColorArr[i]);
                            i = (i == ColorArr.Length - 1) ? 0 : ++i;
                        }
                    }

                    if (receipt.Fields.TryGetValue("Items", out FormField itemsField))
                    {
                        if (itemsField.Value.ValueType == FieldValueType.List)
                        {
                            foreach (FormField itemField in itemsField.Value.AsList())
                            {
                                this.Info += "Item:" + "\n";

                                if (itemField.Value.ValueType == FieldValueType.Dictionary)
                                {
                                    IReadOnlyDictionary<string, FormField> itemFields = itemField.Value.AsDictionary();

                                    if (itemFields.TryGetValue("Name", out FormField itemNameField))
                                    {
                                        if (itemNameField.Value.ValueType == FieldValueType.String)
                                        {
                                            string itemName = itemNameField.Value.AsString();

                                            this.Info += $"  Name: '{itemName}', with confidence {itemNameField.Confidence:0.##}" + "\n";
                                            FieldBoundingBox fieldBoundingBox = itemNameField.ValueData.BoundingBox;
                                            PointF[] ptArray = new PointF[] { fieldBoundingBox[0], fieldBoundingBox[1], fieldBoundingBox[2], fieldBoundingBox[3] };
                                            this.DetObj_ListPointArr.Add(ptArray);
                                            this.DetObj_ListRegion.Add(clsStaticTool.GetRegion_FromPolygon(ptArray));

                                            this.DetObj_ListInfo.Add(itemNameField.ValueData.Text);
                                            this.DetObj_ListColor.Add(ColorArr[i]);
                                            i = (i == ColorArr.Length - 1) ? 0 : ++i;
                                        }
                                    }

                                    if (itemFields.TryGetValue("TotalPrice", out FormField itemTotalPriceField))
                                    {
                                        if (itemTotalPriceField.Value.ValueType == FieldValueType.Float)
                                        {
                                            float itemTotalPrice = itemTotalPriceField.Value.AsFloat();

                                            this.Info += $"  Total Price: '{itemTotalPrice}', with confidence {itemTotalPriceField.Confidence:0.##}" + "\n";
                                            FieldBoundingBox fieldBoundingBox = itemTotalPriceField.ValueData.BoundingBox;
                                            PointF[] ptArray = new PointF[] { fieldBoundingBox[0], fieldBoundingBox[1], fieldBoundingBox[2], fieldBoundingBox[3] };
                                            this.DetObj_ListPointArr.Add(ptArray);
                                            this.DetObj_ListRegion.Add(clsStaticTool.GetRegion_FromPolygon(ptArray));

                                            this.DetObj_ListInfo.Add(itemTotalPriceField.ValueData.Text);
                                            this.DetObj_ListColor.Add(ColorArr[i]);
                                            i = (i == ColorArr.Length - 1) ? 0 : ++i;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (receipt.Fields.TryGetValue("Total", out FormField totalField))
                    {
                        if (totalField.Value.ValueType == FieldValueType.Float)
                        {
                            float total = totalField.Value.AsFloat();

                            this.Info += $"Total: '{total}', with confidence '{totalField.Confidence:0.##}'" + "\n";
                            FieldBoundingBox fieldBoundingBox = totalField.ValueData.BoundingBox;
                            PointF[] ptArray = new PointF[] { fieldBoundingBox[0], fieldBoundingBox[1], fieldBoundingBox[2], fieldBoundingBox[3] };
                            this.DetObj_ListPointArr.Add(ptArray);
                            this.DetObj_ListRegion.Add(clsStaticTool.GetRegion_FromPolygon(ptArray));

                            this.DetObj_ListInfo.Add(totalField.ValueData.Text);
                            this.DetObj_ListColor.Add(ColorArr[i]);
                            i = (i == ColorArr.Length - 1) ? 0 : ++i;
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
