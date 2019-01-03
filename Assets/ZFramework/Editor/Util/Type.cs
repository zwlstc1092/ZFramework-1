﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Reflection;

namespace Zframework.Editor
{

    /// <summary>
    /// 类型相关的实用函数。
    /// </summary>
    internal static class Type //TODO copy check
    {
        private static readonly string[] AssemblyNames =
        {
#if UNITY_2017_3_OR_NEWER
            "ZFramework",
#endif
            "Assembly-CSharp"
        };

        private static readonly string[] EditorAssemblyNames =
        {
#if UNITY_2017_3_OR_NEWER
            "ZFramework.Editor",
#endif
            "Assembly-CSharp-Editor"
        };

        ///// <summary>
        ///// 获取配置路径。
        ///// </summary>
        ///// <typeparam name="T">配置类型。</typeparam>
        ///// <returns>配置路径。</returns>
        //internal static string GetConfigurationPath<T>() where T : ConfigPathAttribute
        //{
        //    foreach (System.Type type in Utility.Assembly.GetTypes())
        //    {
        //        if (!type.IsAbstract || !type.IsSealed)
        //        {
        //            continue;
        //        }

        //        foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
        //        {
        //            if (fieldInfo.FieldType == typeof(string) && fieldInfo.IsDefined(typeof(T), false))
        //            {
        //                return (string)fieldInfo.GetValue(null);
        //            }
        //        }

        //        foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
        //        {
        //            if (propertyInfo.PropertyType == typeof(string) && propertyInfo.IsDefined(typeof(T), false))
        //            {
        //                return (string)propertyInfo.GetValue(null, null);
        //            }
        //        }
        //    }

        //    return null;
        //}

        /// <summary>
        /// 获取指定基类的所有子类的名称。
        /// </summary>
        /// <param name="typeBase">基类类型。</param>
        /// <returns>指定基类的所有子类的名称。</returns>
        internal static string[] GetTypeNames(System.Type typeBase)
        {
            return GetTypeNames(typeBase, AssemblyNames);
        }

        /// <summary>
        /// 获取指定基类的所有子类的名称。
        /// </summary>
        /// <param name="typeBase">基类类型。</param>
        /// <returns>指定基类的所有子类的名称。</returns>
        internal static string[] GetEditorTypeNames(System.Type typeBase)
        {
            return GetTypeNames(typeBase, EditorAssemblyNames);
        }

        private static string[] GetTypeNames(System.Type typeBase, string[] assemblyNames)
        {
            List<string> typeNames = new List<string>();
            foreach (string assemblyName in assemblyNames)
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.Load(assemblyName);
                }
                catch
                {
                    continue;
                }

                if (assembly == null)
                {
                    continue;
                }

                System.Type[] types = assembly.GetTypes();
                foreach (System.Type type in types)
                {
                    if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                    {
                        typeNames.Add(type.FullName);
                    }
                }
            }

            typeNames.Sort();
            return typeNames.ToArray();
        }
    }
}