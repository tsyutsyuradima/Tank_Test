using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraControl : MonoBehaviour 
{
	[SerializeField] float smooth = 2.5f;       // сглаживание при следовании за персонажем
    [SerializeField] float offset;              // значение смещения (отключить = 0)
    [SerializeField] SpriteRenderer boundsMap;  // спрайт, в рамках которого будет перемещаться камера
    [SerializeField] Transform player;          // обект за которим будем следить 

    Vector3 min, max, direction;
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        if (player)
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        CalculateBounds();
    }

    public void CalculateBounds()
    {
        if (boundsMap == null)
            return;
        Bounds bounds = Camera2DBounds();
        min = bounds.max + boundsMap.bounds.min;
        max = bounds.min + boundsMap.bounds.max;
    }

    Bounds Camera2DBounds()
    {
        float height = cam.orthographicSize * 2;
        return new Bounds(Vector3.zero, new Vector3(height * cam.aspect, height, 0));
    }

    Vector3 MoveInside(Vector3 current, Vector3 pMin, Vector3 pMax)
    {
        if (boundsMap == null)
            return current;
        current = Vector3.Max(current, pMin);
        current = Vector3.Min(current, pMax);
        return current;
    }

    void Follow()
    {
        direction = player.right; 
        Vector3 position = player.position + direction * offset;
        position.z = transform.position.z;
        position = MoveInside(position, new Vector3(min.x, min.y, position.z), new Vector3(max.x, max.y, position.z));
        transform.position = Vector3.Lerp(transform.position, position, smooth * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (player)
            Follow();
    }
}