using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : Item
{
    public float throwSpeed;
    public float throwSpawnRadius;
    public float throwSpawnHeight;
    public GameObject throwPrefab;
    public bool addPlayerSpeed;

    public override void OnPickedUp() { }
    public override bool OnActivated() { return false; }
    public override bool OnTick() { return false; }
    public override void OnCleanUp() { }
    /*
    public override bool OnFireLeft() { return OnFire(new Vector2(-1, 0)); }
    public override bool OnFireRight() { return OnFire(new Vector2(1, 0)); }
    public override bool OnFireUp() { return OnFire(new Vector2(0, -1)); }
    public override bool OnFireDown() { return OnFire(new Vector2(0, 1)); }
    */
    public override bool OnFire(Vector2 direction)
    {
        Vector3 spawnLocation = user.transform.position + throwSpawnRadius * new Vector3(direction.x, direction.y, 0) + Vector3.up * throwSpawnHeight;
        GameObject go = (GameObject)Instantiate(throwPrefab, spawnLocation, Quaternion.LookRotation(direction));
        Throwable t = go.GetComponent<Throwable>();
        t.velocity = new Vector3(direction.x, direction.y, 0) * throwSpeed + (addPlayerSpeed ? user.GetVelocity() : Vector3.zero);
        return true;
    }
    /*
    public override void OnAimLeft() { OnAim(new Vector2(-1, 0)); }
    public override void OnAimRight() { OnAim(new Vector2(1, 0)); }
    public override void OnAimUp() { OnAim(new Vector2(0, -1)); }
    public override void OnAimDown() { OnAim(new Vector2(0, 1)); }
    */
    public override void OnAim(Vector2 direction) { }
}
