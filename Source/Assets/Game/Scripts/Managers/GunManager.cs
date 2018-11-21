using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] public List<Gun> AllGuns = new List<Gun>();

    int gunIndex = 0;

    public float GetCurrentShellSpeed()
    {
        return AllGuns[gunIndex].Speed;
    }
    public Sprite GetCurrentShellSprite()
    {
        return AllGuns[gunIndex].ShellSprite;
    }
    public int GetCurrentShellDamage()
    {
        return AllGuns[gunIndex].Damage;
    }
    public float GetCurrentReloadTime()
    {
        return AllGuns[gunIndex].ReloadTime;
    }

    public Sprite SetNextGun()
    {
        if (gunIndex == AllGuns.Count - 1)
            gunIndex = 0;
        else
            gunIndex++;

        return AllGuns[gunIndex].GunSprite;
    }

    public Sprite SetPrevGun()
    {
        if (gunIndex == 0)
            gunIndex = AllGuns.Count - 1;
        else
            gunIndex--;

        return AllGuns[gunIndex].GunSprite;
    }
}