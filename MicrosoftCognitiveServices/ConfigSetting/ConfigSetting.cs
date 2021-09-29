using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Diagnostics;

namespace MicrosoftCognitiveServices.ConfigSetting
{
    [Serializable]
    public class cls_ConfigSetting
    {
        #region 參數

        /// <summary>
        /// 儲存設定 XML檔案位置
        /// </summary>
        static private string Path_ConfigSetting_XML { get; set; } = Application.StartupPath + "\\ConfigSetting.xml";

        public Config_QnAMaker ConfigQnAMaker { get; set; } = new Config_QnAMaker();

        public Config_LUIS ConfigLUIS { get; set; } = new Config_LUIS();

        public Config_AzureService ConfigComputerVision { get; set; } = new Config_AzureService();

        public Config_AzureService ConfigCustomVision { get; set; } = new Config_AzureService();

        public Config_AzureService ConfigFace { get; set; } = new Config_AzureService();

        public Config_AzureService ConfigFormRecognizer { get; set; } = new Config_AzureService();

        public Config_Speech ConfigSpeech { get; set; } = new Config_Speech();

        #endregion

        public cls_ConfigSetting() { }

        #region 方法

        /// <summary>
        /// 載入工單
        /// </summary>
        /// <param name="Recipe"></param>
        /// <param name="PathFile"></param>
        /// <returns></returns>
        public static bool Load(out cls_ConfigSetting Recipe, string PathFile = null)
        {
            bool b_status_ = false;
            if (PathFile == null)
                PathFile = cls_ConfigSetting.Path_ConfigSetting_XML;

            b_status_ = clsStaticTool.LoadXML(PathFile, out Recipe);
            if (b_status_ == false)
                Recipe = new cls_ConfigSetting();

            return b_status_;
        }

        /// <summary>
        /// 儲存工單
        /// </summary>
        /// <param name="PathFile"></param>
        /// <returns></returns>
        public bool Save(string PathFile = null)
        {
            bool b_status_ = false;
            if (PathFile == null)
                PathFile = cls_ConfigSetting.Path_ConfigSetting_XML;

            try
            {
                b_status_ = clsStaticTool.SaveXML(this, PathFile);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return b_status_;
        }

        #endregion
    }

    [Serializable]
    public class Config_AzureService
    {
        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool Enabled { get; set; } = true;

        public string Key { get; set; } = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        public string Endpoint { get; set; } = "https://";

        public Config_AzureService() { }
    }

    [Serializable]
    public class Config_QnAMaker
    {
        #region 參數

        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool Enabled { get; set; } = true;

        public string URL { get; set; } = "https://qnamakerjeff.azurewebsites.net/qnamaker/knowledgebases/b88f2519-d858-4cc3-8377-eaa0cb09f37d/generateAnswer";

        public string EndpointKey { get; set; } = "4c73f00a-3068-41f0-a3fc-6fcf4a93a67c";

        #endregion

        public Config_QnAMaker() { }
    }

    [Serializable]
    public class Config_LUIS
    {
        #region 參數

        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool Enabled { get; set; } = true;

        public string AppID { get; set; } = "d6596854-5278-4e1d-a70f-b1e009d439c0";

        public string Key { get; set; } = "04e5424ccd1744f8bf15a504ed7de25f";

        public string Location { get; set; } = "westus";

        #endregion

        public Config_LUIS() { }
    }

    [Serializable]
    public class Config_Speech
    {
        #region 參數

        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool Enabled { get; set; } = true;

        public string Key { get; set; } = "d2474399389242adac9661b5152ca9c8";

        public string Location { get; set; } = "westus";

        #endregion

        public Config_Speech() { }
    }
}
