using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.LowLevel;

public static class PlayerLoopSystemExtensions
{
    public static void PrintSystem(this PlayerLoopSystem system)
    {
        if (system.subSystemList == null) return;
        foreach (var subSystem in system.subSystemList)
        {
            Debug.Log(subSystem.type);
            subSystem.PrintSystem();
        }
    }

    public static bool IsSystemPresent(this PlayerLoopSystem system,PlayerLoopSystem systemToCheck)
    {
        if (Type.Equals(system.type, systemToCheck.type)) return true;
        if (systemToCheck.subSystemList == null) return false;
        foreach (var subSystem in systemToCheck.subSystemList)
        {
            if (system.IsSystemPresent(subSystem)) return true;
        }
        return false;
    }

    public static bool TryInsertSystem<T>(this PlayerLoopSystem system, ref PlayerLoopSystem systemToInsert, int index)
    {
        if(Type.Equals(systemToInsert.type, typeof(T)))
        {
            List<PlayerLoopSystem> subSystems = new List<PlayerLoopSystem>();
            if (systemToInsert.subSystemList != null) subSystems.AddRange(systemToInsert.subSystemList);
            subSystems.Insert(index, system);
            systemToInsert.subSystemList = subSystems.ToArray();
            return true;
        }
        if (systemToInsert.subSystemList == null) return false;
        for (int i = 0; i < systemToInsert.subSystemList.Count(); i++)
        {
            if (system.TryInsertSystem<T>(ref systemToInsert.subSystemList[i], index)) return true;
        }
        return false;
    }
}
