using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpawner : Item
{
    void Start()
    {
        colors = new Color[5];
        colors[0] = Color.blue;
        colors[1] = Color.green;
        colors[2] = Color.red;
        colors[3] = Color.yellow;
        colors[4] = Color.magenta;
    }

    public GameObject fan;
    public float duration;
    public float timerOffset;
    private Color[] colors;
    // Called when the player picks up the item.
    public override void OnPickedUp() { }
    // Called when the player activates the item (so when the player starts aiming).
    public override bool OnActivated() {
        for (int i = 0; i < 5; i++)
        {
            GameObject fanInstance = Instantiate(fan, user.transform.position, Quaternion.Euler(0, 90, 0));
            fanInstance.GetComponent<FanController>().idol = GameController.theGame.lastPlayer;
            fanInstance.GetComponent<FanController>().direction = i;
            fanInstance.GetComponent<FanController>().baseSpeed = GameController.theGame.baseSpeed;
            fanInstance.GetComponent<FanController>().timer = 0;
            fanInstance.GetComponent<FanController>().animOffset = i * timerOffset / 5;
            fanInstance.GetComponent<FanController>().duration = duration;
            foreach (Transform child in fanInstance.transform)
            {
                child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colors[i]);
            }

        }
        return true;
    }
    // Called every tick that the item is held by the player (whether activated or not).
    public override bool OnTick() { return false; }
    // Called when the item has been used up.
    public override void OnCleanUp() { }

    public override bool OnFire(Vector2 direction) { return false; }

    public override void OnAim(Vector2 direction) { }
}
