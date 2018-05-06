using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHerd : Item
{
    public float trackRadius = 5;
    public float verticalOffset = 0.98f;
    public GameObject herdPrefab;
    public GameObject herdAimPrefab;

    private GameObject herdAimObject;

    // Called when the player picks up the item.
    public override void OnPickedUp() { }
    // Called every tick that the item is held by the player (whether activated or not).
    public override bool OnTick() { return false; }
    // Called when the item has been used up.
    public override void OnCleanUp() { }
    // Called when the player activates the item (so when the player starts aiming).

    public override bool OnActivated()
    {
        herdAimObject = Instantiate(herdAimPrefab);
        herdAimObject.GetComponent<AssignLayerToRenderers>().AssignLayer(user.playerId + 7);
        return false;
    }

    public override bool OnFire(Vector2 direction)
    {
        Vector3 spawnPoint = Vector3.Project(GameController.theGame.lastPlayer.GetComponent<PlayerController>().cam.transform.position,
            GameController.theGame.lastManager.GetComponent<PlayerManagerController>().baseOrientation) + new Vector3(trackRadius * direction.x, verticalOffset, 0);
        Instantiate(herdPrefab, spawnPoint, Quaternion.identity);
        Destroy(herdAimObject);
        herdAimObject = null;
        return true;
    }

    public override void OnAim(Vector2 direction)
    {
        Vector3 spawnPoint = Vector3.Project(user.cam.transform.position, user.playerManager.baseOrientation) + new Vector3(trackRadius * direction.x, verticalOffset, 0);
        herdAimObject.transform.position = spawnPoint;
    }
}
