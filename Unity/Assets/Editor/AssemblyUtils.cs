using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI.Extensions;

public static class AssemblyUtils
    {
        public static List<T> GetInstances<T>()
        {
            List<T> instances = new List<T>();
            Type type = typeof(T);
        
            Assembly[] asemblies = AppDomain.CurrentDomain.GetAssemblies();
                
            foreach (Assembly assembly in asemblies)
            {
                Type[] typesInAssembly = assembly.GetTypes();

                foreach (Type typeInAssembly in typesInAssembly)
                {
                    bool IsAssignableFrom = type.IsAssignableFrom(typeInAssembly);
                    bool isGlobalEventHandler = IsAssignableFrom;
                    isGlobalEventHandler &= typeInAssembly.IsClass;
                    isGlobalEventHandler &= !typeInAssembly.IsAbstract;
                    isGlobalEventHandler &= typeInAssembly.GetConstructor(Type.EmptyTypes) != null;

                    if (isGlobalEventHandler)
                    {
                        T instance = (T)Activator.CreateInstance(typeInAssembly);
                        instances.Add(instance);
                    }
                }
            }

            return instances;
            
        }
    }
