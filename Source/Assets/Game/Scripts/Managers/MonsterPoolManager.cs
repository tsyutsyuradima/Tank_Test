using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonsterPoolManager 
{
    static List<MonsterBehaviour> FreeItems = new List<MonsterBehaviour>();

    public static void AddObject(MonsterBehaviour obj)
    {
        FreeItems.Add(obj);
    }
    public static MonsterBehaviour GetObject()
    {
        MonsterBehaviour res = null;
        if (FreeItems.Count > 0)
        {
            res = FreeItems[0];
            FreeItems.RemoveAt(0);
        }
        return res;
    }
}