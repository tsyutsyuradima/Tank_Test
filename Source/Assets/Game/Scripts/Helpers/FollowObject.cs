using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform Target;
    [HideInInspector] public float Speed = 1f;

    void Update ()
    {
        if (Target != null)
        {
            Vector3 diff = Target.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
	}
}