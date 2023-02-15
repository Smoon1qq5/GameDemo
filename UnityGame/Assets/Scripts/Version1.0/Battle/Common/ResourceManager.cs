using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
    ///<summary>
    ///��Դ������
    ///</summary>
    public class ResourceManager
    {
        private static Dictionary<string, string> configMap;
        static ResourceManager()
        {
            configMap = new Dictionary<string, string>();
            //�����ļ�
            string fileContent = ConfigurationReader.GetConfigFile("ConfigMap.txt");
            //�����ļ�(string--->Dictionary<string,string>)
            ConfigurationReader.Reader(fileContent, BuildMap);
            //BuildMap(fileContent);
        }
        /// <summary>
        /// ��ȡ ���ɵ������ļ�  ����һ��string �ַ���
        /// </summary>
        /// <param name="fileName">�����ļ�������Ҫ�Ӻ�׺�����磺 .txt��</param>
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
        //            //��ȡ���ɵ���Դ���ñ���ļ�ConfigMap.txt
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
            //�ļ���=·��/r/n�ļ���=·��
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
