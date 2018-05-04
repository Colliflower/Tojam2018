using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {
    [Header("Velocity")]
    public Vector3 cameraOrientation;
    public GameObject playerManager;
    
    private Vector3 baseMove;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        baseMove = playerManager.GetComponent<PlayerManagerController>().baseMovement;
        transform.position += baseMove * Time.fixedDeltaTime;
    }
}
