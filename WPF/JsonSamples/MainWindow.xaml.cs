using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using Newtonsoft.Json;

namespace JsonSamples
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region  Fields
        #endregion Fields

        #region Methods
        #endregion Methods

        #region EventHandlers

        #region OptOut Mode

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JsonSerializeOptOutBtn_OnClick(object sender, RoutedEventArgs e)
        {
            JsonModel model = new JsonModel();
            model.Id = 1;
            model.Name = "smx";
            model.Gender = true;
            model.SecondName = "MingXing";

            var set = new JsonSerializerSettings()
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            string jsonStr = JsonConvert.SerializeObject(model, set); // 使用驼峰
            File.WriteAllText("optout.json", jsonStr);

            Console.WriteLine(jsonStr);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JsonDeserializeOptInBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string jsonStr = File.ReadAllText("optout.json");

            var set = new JsonSerializerSettings()
            { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() };
            JsonModel model = (JsonModel)JsonConvert.DeserializeObject<JsonModel>(jsonStr, set);

            Console.WriteLine($"id:{model.Id}, name:{model.Name}, gender:{model.Gender}, secondName:{model.SecondName}");
        }

        #endregion OptOut Mode	

        #region OptIN Mode

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void JsonSerializeOptInBtn_OnClick(object sender, RoutedEventArgs e)
        {
            JsonModel2 model = new JsonModel2();
            model.Id = 11;
            model.Name = "smx";
            model.Gender = true;
            model.SecondName = "MingXing";

            var set = new JsonSerializerSettings();
            set.DefaultValueHandling = DefaultValueHandling.Ignore;
            set.Formatting = Formatting.Indented;


            string jsonStr = JsonConvert.SerializeObject(model);
            File.WriteAllText("optin.json", jsonStr);
        }


        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JsonDeserilizeOptInBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string jsonStr = File.ReadAllText("optin.json");

            JsonModel2 model = (JsonModel2)JsonConvert.DeserializeObject<JsonModel2>(jsonStr);

            Console.WriteLine($"id:{model.Id}, name:{model.Name}, secondName:{model.SecondName}, gender:{model.Gender}, score:{model.GetScore()}");
        }

        #endregion OptIN Mode	

        #region Windows
        #endregion Windows

        #endregion EventHandlers

        #region LitJson 序列化

        private void LitJsonSerializationBtn_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            LitJson.JsonWriter jsonWriter = new LitJson.JsonWriter(sb);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["name"] = "smx";
            dic["age"] = 18;
            dic["score"] = 19.9;

            var subDic = new Dictionary<string, object>();
            subDic["chinese"] = 100;
            subDic["math"] = 99;
            subDic["english"] = 98;

            dic["scores"] = subDic;

            LitJsonUtil.CustomSerialization(dic, jsonWriter);

            Console.WriteLine(sb.ToString());

        }

        #endregion LitJson 序列化	

        private void CustomJsonWriterBtn_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringWriter stringWriter = new StringWriter(stringBuilder);
            using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["name"] = "smx";
                dic["age"] = 18;
                dic["score"] = 19.9;

                var subDic = new Dictionary<string, object>();
                subDic["chinese"] = 100;
                subDic["math"] = 99;
                subDic["english"] = 98;

                dic["scores"] = subDic;

                dic["birthday"] = DateTime.Now;

                JsonUtil.CustomSerialization(dic, jsonWriter);

            }

            Console.WriteLine(stringBuilder);
        }
    }
}
