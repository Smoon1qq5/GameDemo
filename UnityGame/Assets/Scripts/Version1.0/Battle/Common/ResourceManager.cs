using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
    ///<summary>
    ///资源管理器
    ///</summary>
    public class ResourceManager
    {
        private static Dictionary<string, string> configMap;
        static ResourceManager()
        {
            configMap = new Dictionary<string, string>();
            //加载文件
            string fileContent = ConfigurationReader.GetConfigFile("ConfigMap.txt");
            //解析文件(string--->Dictionary<string,string>)
            ConfigurationReader.Reader(fileContent, BuildMap);
            //BuildMap(fileContent);
        }
        /// <summary>
        /// 获取 生成的配置文件  返回一个string 字符串
        /// </summary>
        /// <param name="fileName">配置文件名称需要加后缀（例如： .txt）</param>
        /// <returns></returns>
        //        public static string GetConfigFile(string fileName)
        //        {
        //            string url;
        //#if UNITY_EDITOR || UNITY_STANDALONE
        //            url = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;

        //#elif UNITY_IPHONE
        //        url = "file://" + Application.dataPath + /Raw/+ fileName;
        //#elif UNITY_ANDROID
        //        url = "jar:file://" + Application.dataPath + !/Assets/+ fileName;
        //#endif
        //            //获取生成的资源配置表格文件ConfigMap.txt
        //            UnityWebRequest www = UnityWebRequest.Get(url);
        //            www.SendWebRequest();
        //            while (true)
        //            {
        //                if (www.downloadHandler.isDone)
        //                    return www.downloadHandler.text;
        //            }
        //        }

        //public static void Reader(string fileContent, Action<string> handler)
        //{
        //    using (StringReader stringReader = new StringReader(fileContent))
        //    {
        //        string line;
        //        while ((line = stringReader.ReadLine()) != null)
        //        {
        //            handler(line);
        //        }
        //    }
        //}

        private static void BuildMap(string line)
        {
            //文件名=路径/r/n文件名=路径
            string[] keyValue = line.Split('=');
            configMap.Add(keyValue[0], keyValue[1]);
        }

        public static T Load<T>(string prefabName) where T : UnityEngine.Object
        {
            string prefabPath = configMap[prefabName];
            return Resources.Load<T>(prefabPath);
        }
    }
}
