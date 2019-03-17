/// 
/// File:				IRSDSerializer.cs
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
/// Description:		Deserializes JSON data returned from the RSD service using a MiniJSON or LitJson implementation,
///                     converting basic type properties and fields to the target class by matching field or property names.
/// 


using System;
using System.Collections.Generic;
using UnityEngine;


namespace Lib.RapidSheetData
{
    /// 
    /// Class:       SheetSerializeDesc
    /// Description: Defines a sheet serialization properties
    ///              
    public class SheetSerializeDesc
    {
        // 
        public string SheetName { get; private set; }
        public Type TargetType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="targetType"></param>
        public SheetSerializeDesc(string sheetName, Type targetType)
        {
            SheetName = sheetName;
            TargetType = targetType;
        }
    }

    /// 
    /// Interface:      IRSDSerializer
    /// Description:    The serializer / deserializer interface used by the Rapid Sheet Data library
    ///              
    public interface IRSDSerializer
    {
        /// <summary>
        /// /// <param name="sheetDesc">A list of one or more SheetSerializeDesc defining how the sheets should be deserialized</param>
        /// <param name="szData">The JSON formated serialized data</param>
        /// </summary>
        /// <param name="sheetDesc"></param>
        /// <param name="szData"></param>
        /// <returns></returns>
        Dictionary<string, Dictionary<string, object>> Deserialize(List<SheetSerializeDesc> sheetDesc, string szData);
    }

    /// 
    /// Class:       RSDSerializerDefaultMini
    /// Description: The default implementation of the IRSDSerializer using the MiniJSON library
    ///              
    public class RSDSerializerDefaultMini : IRSDSerializer
    {
        // 
        private IRSDConverter _converter = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        public RSDSerializerDefaultMini(IRSDConverter converter = null)
        {
            _converter = converter;
            if (_converter == null)
            {
                Debug.LogFormat("[RSDSerializerDefaultMini] Creating default converter 'RSDConverterDefault' ...");
                _converter = new RSDConverterDefault();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetDesc"></param>
        /// <param name="szData"></param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, object>> Deserialize(List<SheetSerializeDesc> sheetDesc, string szData)
        {
            Dictionary<string, Dictionary<string, object>> data = new Dictionary<string, Dictionary<string, object>>();

            // Deserialize JSON string            
            Dictionary<string, object> dzData = MiniJSON.Json.Deserialize(szData) as Dictionary<string, object>;
            foreach(var kvp in dzData)
            {
                // Target class Type
                var desc = sheetDesc.Find((SheetSerializeDesc other) => { return (other.SheetName == kvp.Key); });
                if(desc != null)
                {
                    Type targetType = desc.TargetType;

                    // Add sheet data
                    Dictionary<string, object> sheetData = null;
                    if (!data.TryGetValue(kvp.Key, out sheetData))
                    {
                        sheetData = new Dictionary<string, object>();
                        data.Add(kvp.Key, sheetData);
                    }

                    if(sheetData != null)
                    {
                        var entries = kvp.Value as List<object>;
                        for(int idx = 0; idx < entries.Count; ++idx)
                        {
                            string id = "";
                            var instance = Process(entries[idx] as Dictionary<string, object>, targetType, out id);
                            if(instance != null)
                            {
                                if(string.IsNullOrEmpty(id))
                                {
                                    id = idx.ToString();
                                }

                                sheetData.Add(id, instance);
                            }
                        }
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object Process(Dictionary<string, object> data, Type type, out string id)
        {
            var instance = Activator.CreateInstance(type);

            id = "";

            // Deserialize properties
            var propertyFields = type.GetProperties();
            foreach (var property in propertyFields)
            {
                object value = null;
                if (data.TryGetValue(property.Name, out value))
                {
                    var convertedValue = _converter.Convert(value.ToString(), property.PropertyType);
                    property.SetValue(instance, convertedValue, null);

                    if (string.Compare(property.Name, "id", true) == 0)
                    {
                        id = convertedValue.ToString();
                    }
                }
            }

            // Deserialize fields
            var objectFields = type.GetFields();
            foreach (var field in objectFields)
            {
                object value = null;
                if (data.TryGetValue(field.Name, out value))
                {
                    var convertedValue = _converter.Convert(value.ToString(), field.FieldType);
                    field.SetValue(instance, convertedValue);

                    if (string.Compare(field.Name, "id", true) == 0)
                    {
                        id = convertedValue.ToString();
                    }
                }
            }

            return instance;
        }
    }

    /// 
    /// Class:       RSDSerializerDefaultLit
    /// Description: The default implementation of the IRSDSerializer using the LitJson library
    ///              
    public class RSDSerializerDefaultLit : IRSDSerializer
    {
        // 
        private IRSDConverter _converter = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        public RSDSerializerDefaultLit(IRSDConverter converter = null)
        {
            _converter = converter;
            if (_converter == null)
            {
                Debug.LogFormat("[RSDSerializerDefaultLit] Creating default converter 'RSDConverterDefault' ...");
                _converter = new RSDConverterDefault();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetDesc"></param>
        /// <param name="szData"></param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, object>> Deserialize(List<SheetSerializeDesc> sheetDesc, string szData)
        {
            Dictionary<string, Dictionary<string, object>> data = new Dictionary<string, Dictionary<string, object>>();

            var dzData = LitJson.JsonMapper.ToObject<Dictionary<string, List<LitJson.JsonData>>>(szData);

            foreach(var kvp in dzData)
            {
                // Target class Type
                var desc = sheetDesc.Find((SheetSerializeDesc other) => { return (other.SheetName == kvp.Key); });
                if (desc != null)
                {
                    Type targetType = desc.TargetType;

                    // Add sheet data
                    Dictionary<string, object> sheetData = null;
                    if (!data.TryGetValue(kvp.Key, out sheetData))
                    {
                        sheetData = new Dictionary<string, object>();
                        data.Add(kvp.Key, sheetData);
                    }

                    if (sheetData != null)
                    {
                        var entries = kvp.Value as List<LitJson.JsonData>;
                        for (int idx = 0; idx < entries.Count; ++idx)
                        {
                            string id = "";
                            var instance = Process(entries[idx], targetType, out id);
                            if (instance != null)
                            {
                                if (string.IsNullOrEmpty(id))
                                {
                                    id = idx.ToString();
                                }

                                sheetData.Add(id, instance);
                            }
                        }
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object Process(LitJson.JsonData data, Type type, out string id)
        {
            var instance = Activator.CreateInstance(type);

            id = "";

            // Deserialize properties
            var propertyFields = type.GetProperties();
            foreach (var property in propertyFields)
            {
                if (data.Keys.Contains(property.Name))
                {
                    object value = data[property.Name];
                    var convertedValue = _converter.Convert(value.ToString(), property.PropertyType);
                    property.SetValue(instance, convertedValue, null);

                    if (string.Compare(property.Name, "id", true) == 0)
                    {
                        id = convertedValue.ToString();
                    }
                }
            }

            // Deserialize fields
            var objectFields = type.GetFields();
            foreach (var field in objectFields)
            {
                if (data.Keys.Contains(field.Name))
                {
                    object value = data[field.Name];
                    var convertedValue = _converter.Convert(value.ToString(), field.FieldType);
                    field.SetValue(instance, convertedValue);

                    if (string.Compare(field.Name, "id", true) == 0)
                    {
                        id = convertedValue.ToString();
                    }
                }
            }

            return instance;
        }
    }
} /// Lib.RapidSheetData