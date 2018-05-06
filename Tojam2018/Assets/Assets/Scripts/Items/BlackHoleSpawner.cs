using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSpawner : Item
{
    public GameObject blackholePrefab;
    public override void OnPickedUp() { }

    public override bool OnActivated()
    {
        Vector3 v = user.transform.position;
        v.y = 0;
        Instantiate(blackholePrefab, v, Quaternion.identity).GetComponent<BlackHoleController>().creator = user.gameObject;
        return true;
    }

    public override bool OnTick() { return false; }
    public override void OnCleanUp() { }

    public override bool OnFire(Vector2 direction) { return false; }

    public override void OnAim(Vector2 direction) { }
}
