using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SimpleDelegate();
public delegate void IntDelegate(int value);
public delegate void MonsterDelegate(MonsterBehaviour value);

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public event MonsterDelegate OnDestroyedMonster;
    public event SimpleDelegate OnGameOver;
    public event SimpleDelegate OnRestartGame;
    public event IntDelegate OnPlayerDamage;
    public event IntDelegate OnPlayerHealthUpdate;

    public TankBehaviour Tank;

    void Awake()
    {
        instance = this;
    }

    public void DamagePlayer(int damage)
    {
        if (OnPlayerDamage != null)
            OnPlayerDamage(damage);
    }

    public void DestroyedMonster(MonsterBehaviour monster)
    {
        if (OnDestroyedMonster != null)
            OnDestroyedMonster(monster);
    }

    public void GameOver()
    {
        if (OnGameOver != null)
            OnGameOver();
    }

    public void Restart()
    {
        if (OnRestartGame != null)
            OnRestartGame();

        Tank.gameObject.SetActive(true);
    }

    public void UpdatePlayerHealth(int value)
    {
        if (OnPlayerHealthUpdate != null)
            OnPlayerHealthUpdate(value);
    }
}