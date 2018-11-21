using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShellPoolManager
{
    static List<ShellBehaviour> FreeItems = new List<ShellBehaviour>();

    public static void AddObject(ShellBehaviour obj)
    {
        FreeItems.Add(obj);
    }
    public static ShellBehaviour GetObject()
    {
        ShellBehaviour res = null;
        if (FreeItems.Count > 0)
        {
            res = FreeItems[0];
            FreeItems.RemoveAt(0);
        }
        return res;
    }
}