using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace FSM
{
    public class AIConfigurationReader
    {
        //数据结构
        public Dictionary<string, Dictionary<string, string>> Map { get; private set; }

        public AIConfigurationReader(string fileName)
        {
            Map = new Dictionary<string, Dictionary<string, string>>();
            //读取配置文件
            string file = ConfigurationReader.GetConfigFile(fileName);
            //解析配置文件
            ConfigurationReader.Reader(file, BuildMap);
        }
        private string mainKey ;
        private void BuildMap(string line)
        {

            line = line.Trim();
            if (string.IsNullOrEmpty(line)) return;
            if (line.StartsWith("["))
            {
                mainKey = line.Substring(1, line.Length - 2);
                Map.Add(mainKey, new Dictionary<string, string>());
            }
            else
            {
                string[] keyValue = line.Split('>');
                Map[mainKey].Add(keyValue[0],keyValue[1]);
            }
        }
    }
}
