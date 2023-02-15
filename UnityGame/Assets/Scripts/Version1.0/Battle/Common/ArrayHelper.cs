using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    ///<summary>
    ///数组助手类
    ///</summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T Find<T>(this T[] array, Func<T, bool> condition)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    return array[i];
                }

            }
            return default(T);
        }
        /// <summary>
        /// 查找所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] array, Func<T, bool> condition)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    list.Add(array[i]);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取最大
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T GetMax<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            T max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(max).CompareTo(condition(array[i])) < 0)
                {
                    max = array[i];
                }

            }
            return max;
        }
        /// <summary>
        /// 获取最小
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T GetMin<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            T min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(min).CompareTo(condition(array[i])) > 0)
                {
                    min = array[i];
                }

            }
            return min;
        }
        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        public static void OrderBy<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            //   5 2 4 7 

            for (int c = 0; c < array.Length - 1; c++)
            {
                for (int i = c + 1; i < array.Length; i++)
                {
                    if (condition(array[c]).CompareTo(condition(array[i])) > 0)
                    {

                        T temp = array[c];
                        array[c] = array[i];
                        array[i] = temp;

                    }
                }
            }
        }
        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        public static void OrderDescding<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (condition(array[i]).CompareTo(condition(array[j])) < 0)
                    {
                        T temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
        /// <summary>
        /// 筛选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Q[] Select<T, Q>(this T[] array, Func<T, Q> condition)
        {
            Q[] result = new Q[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = condition(array[i]);
            }
            return result;
        }
     }
}
