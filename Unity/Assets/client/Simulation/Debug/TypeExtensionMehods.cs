
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class TypeExtensionMehods
    {
        public static FieldInfo[] GetFieldInfoIncludingParents(this Type type, BindingFlags flags)
        {
            List<FieldInfo> fieldInfoList = new List<FieldInfo>();
            if (type.BaseType != typeof(System.Object))
            {
                fieldInfoList.AddRange(type.BaseType.GetFieldInfoIncludingParents(flags));
            }
            
            fieldInfoList.AddRange(type.GetFields(flags));
            return fieldInfoList.ToArray();
        }
    }
