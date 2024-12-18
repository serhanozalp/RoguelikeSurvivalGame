using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class TypeExtensions 
{
    public static List<Type> GetDerivedTypes(this Type type, Assembly assembly, bool includeSelf = false)
    {
        List<Type> derivedTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(type)).ToList();
        if (includeSelf) derivedTypes.Insert(0, type);
        return derivedTypes;
    }

    public static List<Type> GetConcreteDerivedTypes(this Type type, Assembly assembly, bool includeSelf = false)
    {
        return type.GetDerivedTypes(assembly, includeSelf).Where(t => !t.IsAbstract).ToList();
    }
}
