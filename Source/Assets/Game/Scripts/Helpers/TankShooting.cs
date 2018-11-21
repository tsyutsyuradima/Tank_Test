using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankShooting : MonoBehaviour
{
    public event SimpleDelegate OnShot;

    [SerializeField] ShellBehaviour tankShell; // префаб гильзи
    [SerializeField] Transform tankShellPoint; // откуда вылетают снаряды

    float reloadTime;
    float shotTime = Mathf.Infinity;
    bool canShot;

    void Update()
    {
        CheckCanShot();
        if (Input.GetKeyDown(KeyCode.X) && canShot)
        {
            canShot = false;
            if (OnShot != null)
                OnShot();
        }
    }

    public void ShotShell(Sprite sprite, float speed, int damage, float shellReloadTime)
    {
        reloadTime = shellReloadTime;
        float angle = Mathf.Atan2(tankShellPoint.right.y, tankShellPoint.right.x) * Mathf.Rad2Deg;
        ShellBehaviour shell = ShellPoolManager.GetObject();

        if (shell != null)
        {
            shell.transform.position = tankShellPoint.position;
            shell.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            shell.gameObject.SetActive(true);
        }
        else
        {
            shell = Instantiate(tankShell, tankShellPoint.position, Quaternion.AngleAxis(angle, Vector3.forward)) as ShellBehaviour;
        }
        shell.SetDirection(tankShellPoint.right, sprite, speed, damage);
    }


    void CheckCanShot()
    {
        if (canShot) return;
        shotTime += Time.deltaTime;

        if (shotTime > reloadTime)
        {
            shotTime = 0;
            canShot = true;
        }
    }
}