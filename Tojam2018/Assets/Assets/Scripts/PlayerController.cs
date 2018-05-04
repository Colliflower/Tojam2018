using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PowerUps {None, WaterBottle, Goat, Fan};

public class PlayerController : MonoBehaviour {
    [Header("Input")]
    public string moveHorizontalAxisName;
    public string moveVerticalAxisName;
    public string throwHorizontalAxisName;
    public string throwVerticalAxisName;

    public GameObject cam;

    private float playerSpeed = 1;

    private Rigidbody rb;

    private PlayerManagerController playerManager;

    public float minDistOffset;

    private float initDist = 0;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 1.0f;

        playerManager = transform.parent.gameObject.GetComponent<PlayerManagerController>();
        playerSpeed = playerManager.playerSpeed;
        
        initDist = Vector3.Project(cam.transform.position - transform.position, playerManager.baseOrientation).magnitude;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float moveHorizontal = Input.GetAxis(moveHorizontalAxisName);
        float moveVertical = Input.GetAxis(moveVerticalAxisName);

        Vector3 baseMove = playerManager.baseMovement;

        Vector3 inputMove = new Vector3(moveHorizontal, 0, moveVertical);
        inputMove = inputMove * playerSpeed;
        Vector3 finalMove = inputMove + baseMove;

        Vector3 futurePosition = rb.position + finalMove * Time.fixedDeltaTime;

        float currDist = Vector3.Project(camera.transform.position - transform.position, playerManager.baseOrientation).magnitude;
        
        //Debug.Log("Init: " + initDist.ToString() + ", Curr: " + currDist.ToString() + ", Combined: " + (initDist + minDistOffset).ToString());
        
        if (moveVertical < 0 && currDist < initDist + minDistOffset)
        {
            finalMove.z = baseMove.z;
            futurePosition  = rb.position + finalMove * Time.fixedDeltaTime;
        }

        rb.MovePosition(futurePosition);

        if (finalMove.magnitude > 0)
        {
            Vector3 toRotate = Vector3.RotateTowards(transform.forward, finalMove, 0.5f, 0.0f);

            transform.rotation = Quaternion.LookRotation(toRotate);
        }
        
        finalMove.x = rb.position.x;
        finalMove.y = 0;
        finalMove.z = rb.position.z;

        rb.MovePosition(finalMove);
    }
}
