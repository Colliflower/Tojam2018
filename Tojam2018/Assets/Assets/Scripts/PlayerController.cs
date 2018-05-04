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

    private Vector3 inputMove = Vector3.zero;
    private Vector3 baseMove = Vector3.zero;
    private Vector3 finalMove = Vector3.zero;
    private Vector3 toRotate = Vector3.zero;

    private float playerSpeed = 1;

    private Rigidbody rb;

    private float sinCounter = 0;

    private float yOffset = 0;

    private PlayerManagerController playerManager;

    public float minDistOffset;

    private float initDist = 0;
    private float currDist = 0;

    private Vector3 futurePosition;

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

        baseMove = playerManager.baseMovement;

        inputMove = new Vector3(moveHorizontal, 0, moveVertical);
        inputMove = inputMove * playerSpeed;
        finalMove = inputMove + baseMove;

        futurePosition = rb.position + finalMove * Time.fixedDeltaTime;

        currDist = Vector3.Project(cam.transform.position - transform.position, playerManager.baseOrientation).magnitude;

        Debug.Log("Init: " + initDist.ToString() + ", Curr: " + currDist.ToString() + ", Combined: " + (initDist + minDistOffset).ToString());
        
        if (moveVertical < 0 && currDist < initDist + minDistOffset)
        {
            finalMove.z = baseMove.z;
            futurePosition  = rb.position + finalMove * Time.fixedDeltaTime;
        }

        rb.MovePosition(futurePosition);

        if (finalMove.magnitude > 0)
        {
            toRotate = Vector3.RotateTowards(transform.forward, finalMove, 0.5f, 0.0f);

            transform.rotation = Quaternion.LookRotation(toRotate);
        }

        // Bobbing
        sinCounter += finalMove.magnitude * Time.fixedDeltaTime * 2;

        if (sinCounter >= Mathf.PI * 100)
            sinCounter -= Mathf.PI * 100;

        yOffset = Mathf.Sin(sinCounter) + 2f;

        finalMove.x = rb.position.x;
        finalMove.y = yOffset;
        finalMove.z = rb.position.z;

        rb.MovePosition(finalMove);
    }
}
