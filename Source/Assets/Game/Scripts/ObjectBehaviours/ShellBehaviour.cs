using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShellBehaviour : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public GameObject ExplosionParticle;
    int damage;

    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StopAllCoroutines();
        Sprite.gameObject.SetActive(true);
        ExplosionParticle.SetActive(false);
    }

    public void SetDirection(Vector3 direction, Sprite sprite, float speed, int shellDamage)
    {
        if(body == null)
            body = GetComponent<Rigidbody2D>();

        Sprite.sprite = sprite;
        body.velocity = direction.normalized * speed;
        damage = shellDamage;
        body.gravityScale = 0;
    }

    public int GetShellDamage()
    {
        return damage;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        body.velocity = Vector2.zero;
        ExplosionParticle.SetActive(true);
        Sprite.gameObject.SetActive(false);
        StartCoroutine(WaitAndHideObject());
    }

    IEnumerator WaitAndHideObject()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        ShellPoolManager.AddObject(this);
    }
}