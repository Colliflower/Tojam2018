﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerController : MonoBehaviour {

    public float playerSpeed;
    public float baseSpeed;
    public Vector3 baseOrientation;
    public Vector3 baseMovement;

    [Header("Local References")]
    public GameObject character;

    // Use this for initialization
    void Start () {
        GameController controller = GameController.theGame;
        baseSpeed = controller.baseSpeed;
        playerSpeed = controller.playerSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        baseMovement = baseOrientation / baseOrientation.magnitude * baseSpeed;
        if (Input.GetKeyDown("space"))
        {
            baseSpeed += 10;
        }
    }
}
