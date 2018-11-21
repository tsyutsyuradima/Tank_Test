using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TankMovement))]
[RequireComponent (typeof(TankShooting))]
[RequireComponent (typeof(GunManager))]

public class TankBehaviour : MonoBehaviour
{
    public Tank TankBase;
    public SpriteRenderer Gun;
    public GameObject Explosion;
    public Transform DamageValuePoint;
    public GameObject DamageValuePrefab;
    public TankAudioController TankAudioController;

    GunManager gunManager;
    TankMovement tankMovement;
    TankShooting tankShooting;
    float currentHealth;

    void Start()
    {
        GameManager.instance.OnDestroyedMonster += OnDestroyedMonster;
        GameManager.instance.OnPlayerDamage += OnPlayerDamage;

        gunManager = GetComponent<GunManager>();

        tankMovement = GetComponent<TankMovement>();
        tankMovement.SetParams(TankBase.Speed, TankBase.RotationSpeed);

        tankShooting = GetComponent<TankShooting>();
        tankShooting.OnShot += TankShooting_OnShot;

        currentHealth = TankBase.Health;
    }
    
    void OnEnable()
    {
        currentHealth = TankBase.Health;
        Explosion.SetActive(false);
        GameManager.instance.UpdatePlayerHealth((int)currentHealth);
    }

    void OnPlayerDamage(int damage)
    {
        Instantiate(DamageValuePrefab, DamageValuePoint).GetComponent<DamageValueComponent>().SetTextValue("-"+damage);

        currentHealth = currentHealth - damage * (1 - TankBase.Defence);
        if (currentHealth <= 0)
        {
            StartCoroutine(GameOver());
            GameManager.instance.UpdatePlayerHealth(0);
        }
        else
        {
            GameManager.instance.UpdatePlayerHealth((int)currentHealth);
        }
    }

    IEnumerator GameOver()
    {
        TankAudioController.Explosion();
        Explosion.SetActive(true);
        yield return new WaitForSeconds(1f);
        Explosion.SetActive(false);
        gameObject.SetActive(false);
        GameManager.instance.GameOver();
    }

    void OnDestroyedMonster(MonsterBehaviour value)
    {
        TankAudioController.Explosion();
    }

    void TankShooting_OnShot()
    {
        tankShooting.ShotShell(gunManager.GetCurrentShellSprite(), gunManager.GetCurrentShellSpeed(), gunManager.GetCurrentShellDamage(), gunManager.GetCurrentReloadTime());
        TankAudioController.Fire();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Gun.sprite = gunManager.SetPrevGun();
        if (Input.GetKeyDown(KeyCode.W))
            Gun.sprite = gunManager.SetNextGun();
    }
}