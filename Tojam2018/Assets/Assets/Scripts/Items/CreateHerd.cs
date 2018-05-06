using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHerd : Item
{
    public float trackRadius = 5;
    public float verticalOffset = 0.98f;
    public GameObject herdPrefab;

    // Called when the player picks up the item.
    public override void OnPickedUp() { }
    // Called when the player activates the item (so when the player starts aiming).
    public override bool OnActivated() { return false; }
    // Called every tick that the item is held by the player (whether activated or not).
    public override bool OnTick() { return false; }
    // Called when the item has been used up.
    public override void OnCleanUp() { }

    public override bool OnFire(Vector2 direction)
    {
        Vector3 spawnPoint = Vector3.Project(user.cam.transform.position, user.playerManager.baseOrientation) + new Vector3(trackRadius * direction.x, verticalOffset, 0);
        GameObject herd = (GameObject)Instantiate(herdPrefab, spawnPoint, Quaternion.identity);
        return true;
    }

    public override void OnAim(Vector2 direction)
    {

    }
}
