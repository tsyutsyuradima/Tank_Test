using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FollowObject))]
public class MonsterBehaviour : MonoBehaviour
{
    public Monster MonsterBase;
    public SpriteRenderer Sprite;
    public GameObject Explosion;
    public Transform DamageValuePoint;
    public GameObject DamageValuePrefab;

    FollowObject followObject;
    float currentHealth;
    BoxCollider2D boxCollider2D;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        followObject = GetComponent<FollowObject>();
        followObject.Speed = MonsterBase.Speed;
        followObject.Target = GameManager.instance.Tank.transform;
        currentHealth = MonsterBase.Health;
    }

    void OnEnable()
    {
        boxCollider2D.enabled = true;
        Sprite.gameObject.SetActive(true);
        Explosion.SetActive(false);
        currentHealth = MonsterBase.Health;
        StartCoroutine(WaitAndGo(0.3f));
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Shell")
        {
            int damage = coll.GetComponent<ShellBehaviour>().GetShellDamage();
            StartCoroutine(WaitAndGo(0.3f));
            currentHealth = currentHealth - damage * (1 - MonsterBase.Defence);

            if (currentHealth <= 0)
                HideMonster();
            else
                Instantiate(DamageValuePrefab, DamageValuePoint).GetComponent<DamageValueComponent>().SetTextValue("-" + damage);

        }
        else if (coll.tag == "Player")
        {
            GameManager.instance.DamagePlayer(MonsterBase.Damage);
            HideMonster();
        }
    }

    public void HideMonster()
    {
        StopAllCoroutines();
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        boxCollider2D.enabled = false;
        followObject.Speed = 0;

        Explosion.SetActive(true);
        Sprite.gameObject.SetActive(false);
        GameManager.instance.DestroyedMonster(this);

        yield return new WaitForSeconds(1f);
        Explosion.SetActive(false);
        MonsterPoolManager.AddObject(this);
        gameObject.SetActive(false);
    }

    IEnumerator WaitAndGo(float delay)
    {
        Sprite.DOFade(0.1f, 0.1f);
        followObject.Speed = 0;
        yield return new WaitForSeconds(delay);
        Sprite.DOFade(1f, 0.1f);
        followObject.Speed = MonsterBase.Speed;
    }
}   