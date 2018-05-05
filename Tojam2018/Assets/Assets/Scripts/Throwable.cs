using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Vector3 velocity;
    public float lifeTime = -1;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (lifeTime > 0 && Time.time - startTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
