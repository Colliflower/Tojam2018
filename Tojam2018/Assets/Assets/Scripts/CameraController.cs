using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("Velocity")]
    public Vector3 cameraOrientation;

    public GameObject player;

    public float maxDistOffset;

    private float initDist = 0;
    private float currDist = 0;

    private Vector3 baseMove;

    private PlayerManagerController playerManager;
    // Use this for initialization
    void Start () {
        playerManager = transform.parent.gameObject.GetComponent<PlayerManagerController>();
        initDist = Vector3.Project(player.transform.position - transform.position, playerManager.baseOrientation).magnitude;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        baseMove = playerManager.baseMovement;
        transform.position += baseMove * Time.fixedDeltaTime;

        currDist = Vector3.Project(player.transform.position - transform.position, playerManager.baseOrientation).magnitude;

        if (currDist > initDist + maxDistOffset)
        {
            transform.position += (playerManager.baseOrientation / playerManager.baseOrientation.magnitude) * (currDist - (initDist + maxDistOffset));
        }
	}
}
