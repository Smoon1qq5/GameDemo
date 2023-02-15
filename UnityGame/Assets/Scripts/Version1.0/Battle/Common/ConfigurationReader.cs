using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
    public class ConfigurationReader
    {
        public static string GetConfigFile(string fileName)
        {
            string url;
#if UNITY_EDITOR || UNITY_STANDALONE
            url = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;

#elif UNITY_IPHONE
        url = "file://" + Application.dataPath + /Raw/+ fileName;
#elif UNITY_ANDROID
        url = "jar:file://" + Application.dataPath + !/Assets/+ fileName;
#endif
            //获取生成的资源配置表格文件ConfigMap.txt
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.SendWebRequest();
            while (true)
            {
                if (www.downloadHandler.isDone)
                    return www.downloadHandler.text;
            }
        }

        public static void Reader(string fileContent, Action<string> handler)
        {
            using (StringReader stringReader = new StringReader(fileContent))
            {
                string line;
                while ((line = stringReader.ReadLine()) != null)
                {
                    handler(line);
                }
            }
        }
    }


}
