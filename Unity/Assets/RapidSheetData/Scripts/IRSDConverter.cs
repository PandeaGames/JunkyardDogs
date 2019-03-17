/// 
/// File:				IRSDConverter.cs
/// 
/// System:				Rapid Sheet Data (RSD) Unity3D client library
/// Version:			1.0.0
/// 
/// Language:			C#
/// 
/// License:				
/// 
/// Author:				Tasos Giannakopoulos (tasosg@voidinspace.com)
/// Date:				10 Mar 2017
/// 
/// Description:		
/// 


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


namespace Lib.RapidSheetData
{
    /// 
    /// Interface:      IRSDConverter
    /// Description:    Provides the converted interface used to convert a string value to the desired type
    ///
    public interface IRSDConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object Convert(string value, Type type);
    }

    /// 
    /// Class:          RSDConverterDefault
    /// Description:    Default implementation of the IRSDConverter using C# reflection
    ///
    public class RSDConverterDefault : IRSDConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Convert(string value, Type type)
        {
            if (!string.IsNullOrEmpty(value))
            {
                // Returns string as is
                if (type == typeof(string))
                {
                    return value;
                }
                // Comma separated arrays of basic types ("1,2,3,4,5,...") to List<T>
                else if (type.IsGenericType)
                {
                    var listType = typeof(List<>);
                    var elementType = type.GetGenericArguments()[0];
                    var constructedListType = listType.MakeGenericType(elementType);

                    var listInstance = (IList)Activator.CreateInstance(constructedListType);

                    // 
                    string[] elementValues = value.ToString().Split(',');
                    foreach (string elementValue in elementValues)
                    {
                        var convertedValue = Convert(elementValue, elementType);
                        listInstance.Add(convertedValue);
                    }

                    return listInstance;
                }
                // Comma separated arrays of basic types ("1,2,3,4,5,...") to C# array []
                else if (type.IsArray)
                {
                    // 
                    string[] elementValues = value.ToString().Split(',');

                    var elementType = type.GetElementType();
                    var arrayInstance = Array.CreateInstance(elementType, elementValues.Length);

                    for (int idx = 0; idx < elementValues.Length; ++idx)
                    {
                        var convertedValue = Convert(elementValues[idx], elementType);
                        arrayInstance.SetValue(convertedValue, idx);
                    }

                    return arrayInstance;
                }
                // Basic types
                else
                {
                    var converter = TypeDescriptor.GetConverter(type);
                    if (converter != null)
                    {
                        try
                        {
                            return converter.ConvertFromString(value.ToString());
                        }
                        catch
                        {
                            return GetDefault(type);
                        }
                    }
                    else
                    {
                        Debug.LogWarningFormat("[RSDConverter] ConvertValue : Failed to find converter for '{0}'", type);
                    }
                }
            }

            return GetDefault(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object GetDefault(Type type)
        {
            Func<object> f = GetDefault<object>;
            return f.Method.GetGenericMethodDefinition().MakeGenericMethod(type).Invoke(null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static T GetDefault<T>()
        {
            return default(T);
        }
    }

    /// 
    /// Class:          RSDConverterAOT
    /// Description:    Default implementation of the IRSDConverter to support AOT platforms
    ///
    public class RSDConverterAOT : IRSDConverter
    {
        /// <summary>
        /// A crude implementation to support AOT platforms
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Convert(string value, Type type)
        {
            if (!string.IsNullOrEmpty(value))
            {
                // Returns string as is
                if (type == typeof(string))
                {
                    return value;
                }
                // Enum
                else if (type.IsEnum)
                {
                    try
                    {
                        return Enum.Parse(type, value, true);
                    }
                    catch { }
                }
                else if (type == typeof(bool))
                {
                    bool parsedValue = false;
                    bool.TryParse(value, out parsedValue);
                    return parsedValue;
                }
                else if (type == typeof(short))
                {
                    short parsedValue = 0;
                    short.TryParse(value, out parsedValue);
                    return parsedValue;
                }
                else if (type == typeof(ushort))
                {
                    ushort parsedValue = 0;
                    ushort.TryParse(value, out parsedValue);
                    return parsedValue;
                }
                else if(type == typeof(int))
                {
                    int parsedValue = 0;
                    int.TryParse(value, out parsedValue);
                    return parsedValue;
                }
                else if (type == typeof(uint))
                {
                    uint parsedValue = 0U;
                    uint.TryParse(value, out parsedValue);
                    return parsedValue;
                }
                else if (type == typeof(long))
                {
                    long parsedValue = 0L;
                    long.TryParse(value, out parsedValue);
                    return parsedValue;
                }
                else if (type == typeof(ulong))
                {
                    ulong parsedValue = 0UL;
                    ulong.TryParse(value, out parsedValue);
                    return parsedValue;
                }
                else if (type == typeof(float))
                {
                    float parsedValue = 0.0F;
                    float.TryParse(value, out parsedValue);
                    return parsedValue;
                }
                else if (type == typeof(double))
                {
                    double parsedValue = 0.0D;
                    double.TryParse(value, out parsedValue);
                    return parsedValue;
                }
                // Comma separated arrays of basic types ("1,2,3,4,5,...") to List<T>
                else if (type.IsGenericType)
                {
                    var listType = typeof(List<>);
                    var elementType = type.GetGenericArguments()[0];
                    var constructedListType = listType.MakeGenericType(elementType);
                
                    var listInstance = (IList)Activator.CreateInstance(constructedListType);
                
                    // 
                    string[] elementValues = value.ToString().Split(',');
                    foreach (string elementValue in elementValues)
                    {
                        var convertedValue = Convert(elementValue, elementType);
                        listInstance.Add(convertedValue);
                    }
                
                    return listInstance;
                }
                // Comma separated arrays of basic types ("1,2,3,4,5,...") to C# array []
                else if (type.IsArray)
                {
                    // 
                    string[] elementValues = value.ToString().Split(',');
                
                    var elementType = type.GetElementType();
                    var arrayInstance = Array.CreateInstance(elementType, elementValues.Length);
                
                    for (int idx = 0; idx < elementValues.Length; ++idx)
                    {
                        var convertedValue = Convert(elementValues[idx], elementType);
                        arrayInstance.SetValue(convertedValue, idx);
                    }
                
                    return arrayInstance;
                }
            }

            return null;
        }
    }

} /// Lib.RapidSheetData