using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TankMovement : MonoBehaviour
{
    // Скорость движения и вращения : 
    float tankSpeed;
    float tankRotationSpeed;

    Rigidbody2D body;
    float shotTime = Mathf.Infinity;
    bool canShot;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }
    public void SetParams(float speed, float rotation)
    {
        tankSpeed = speed;
        tankRotationSpeed = rotation;
    }
    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        body.AddForce(transform.right * tankSpeed * v, ForceMode2D.Impulse);
        body.AddTorque(tankRotationSpeed * h * -Mathf.Sign(v), ForceMode2D.Impulse);
    }
}