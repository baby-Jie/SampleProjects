using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonSamples
{
    // OptOut模式下 只有特性为JsonIgnore 的属性才不会被序列化
    [JsonObject(MemberSerialization.OptOut)]
    public class JsonModel
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string Name { get; set; }

        public string SecondName { get; set; }

        public bool Gender { get; set; }
    }


    // OptIn模式下 只有特性为JsonProperty 的属性才会被序列化
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonModel2
    {
        [JsonProperty]
        private int Score { get; set; } = 99;

        [DefaultValue(111)]
        [JsonProperty]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty]
        public string SecondName { get; set; }

        [JsonProperty("hello")]
        public bool Gender { get; set; }

        public int GetScore()
        {
            return Score;
        }
    }

    // 不知道啥特性
    [JsonObject(MemberSerialization.Fields)]
    public class JsonModel3
    {
        public string address;

        public int Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public bool Gender { get; set; }
    }
}
