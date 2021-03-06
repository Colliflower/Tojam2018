﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PowerUps {None, WaterBottle, Goat, Fan};

public class PlayerController : MonoBehaviour {
    [Header("Input")]
    public string moveHorizontalAxisName;
    public string moveVerticalAxisName;
    public string throwHorizontalAxisName;
    public string throwVerticalAxisName;

	public AudioClip[] footsteps;

    public GameObject cam;

    private float playerSpeed = 1;

	private AudioSource audioSource;

	private float audioStep;
	private float audioCounter;

	private bool canStep;

    private Rigidbody rb;

    public PlayerManagerController playerManager;

    public float minDistOffset;

    private float initDist = 0;

    private Vector3 lastFrameVelocity;

    [Range(1, 4)]
    public int playerId;

    public ParticleSystem sys1;
    public ParticleSystem sys2;
    public float teleportParticlesPlayTime;

    public AudioSource cheer;

    // ===== Item stuff ===== //
    [Header("Throwing")]
    public float throwDeadzone = .5f;
    public Image itemIcon;
    public Color heldColour = Color.white;
    public Color activeColour = Color.green;

    private Item storedItem;
    private bool itemIsActive;

    private Vector2 previousThrowVector;

    [HideInInspector]
    public float speedUpAmount = 0;

    [Header("Debug")]
    public bool log;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 1.0f;

        playerManager = transform.parent.gameObject.GetComponent<PlayerManagerController>();
        playerSpeed = playerManager.playerSpeed;

        initDist = Vector3.Project(cam.transform.position - transform.position, playerManager.baseOrientation).magnitude;

		audioSource = GetComponent<AudioSource> ();

		canStep = true;
		audioStep = playerManager.baseSpeed/20;

        ParticleSystem.EmissionModule mod = sys1.emission;
        mod.enabled = false;
        mod = sys2.emission;
        mod.enabled = false;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        HandleMovement();
    }

    private void Update()
    {
        playerSpeed = playerManager.playerSpeed;
        
        if (storedItem)
        {
            itemIcon.enabled = true;
            itemIcon.sprite = storedItem.icon;
            itemIcon.color = storedItem.IsActivated() ? activeColour : heldColour;
        }
        else
        {
            itemIcon.enabled = false;
        }

        HandleItem();
		if (footsteps.Length > 0) {
			audioStep = playerManager.baseSpeed/20;
			audioCounter += audioStep;
			if (audioCounter > 1) {
				audioCounter = 0;
				canStep = true;
			}
			audioSource.clip = footsteps [Mathf.RoundToInt (Random.value * 5)];
			if (canStep && !audioSource.isPlaying) {
				audioSource.Play ();
				canStep = false;
			}
		}

    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis(moveHorizontalAxisName);
        float moveVertical = Input.GetAxis(moveVerticalAxisName);

        Vector3 baseMove = playerManager.baseMovement + Vector3.forward * speedUpAmount;

        Vector3 inputMove = new Vector3(moveHorizontal, 0, moveVertical);

        if (log)
        {
            //Debug.Log(inputMove);
        }

        inputMove = inputMove * playerSpeed;
        Vector3 finalMove = inputMove + baseMove;

        Vector3 futurePosition = transform.position + finalMove * Time.fixedDeltaTime;
        
        float currDist = Vector3.Project(cam.transform.position - transform.position, playerManager.baseOrientation).magnitude;

        if (moveVertical < 0 && currDist < initDist + minDistOffset)
        {
            finalMove.z = baseMove.z;
            futurePosition = transform.position + finalMove * Time.fixedDeltaTime;
        }

        futurePosition.y = 0;

        transform.position = futurePosition;

        if (finalMove.magnitude > 0)
        {
            Vector3 toRotate = Vector3.RotateTowards(transform.forward, finalMove, 0.5f, 0.0f);

            transform.rotation = Quaternion.LookRotation(toRotate);
        }

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", finalMove.magnitude);

        lastFrameVelocity = finalMove;
        //if (log)
            //Debug.Log("Velocity: " + finalMove.ToString());
    }

    void HandleItem()
    {
        // We only need to manage the item if it exists!
        if (!storedItem)
        {
            return;
        }

        // Tick the item. If it returns true, that means the item has been used up!
        if (storedItem.Tick())
        {
            CleanUpItem();
            return;
        }

        // Compute the throw vector from input. Make sure that the vector has
        // AT MOST a magnitude of 1 (important for moving the analog stick diagonally).
        float throwHorizontal = Input.GetAxis(throwHorizontalAxisName);
        float throwVertical = Input.GetAxis(throwVerticalAxisName);
        Vector2 throwVector = Vector2.ClampMagnitude(new Vector2(throwHorizontal, throwVertical), 1);
        // If the item is inactive we need to check whether to activate it.
        if (!itemIsActive)
        {
            // Activate only if the analog stick is outside of the deadzone.
            if (throwVector.magnitude >= throwDeadzone)
            {
                if (!storedItem.Activated())
                {
                    itemIsActive = true;
                    previousThrowVector = throwVector;
                }
                else
                {
                    CleanUpItem();
                }
            }
        }
        else
        {
            // float absX = Mathf.Abs(previousThrowVector.x);
            // float absY = Mathf.Abs(previousThrowVector.y);
            bool FiredAndCompleted = false;
            if (previousThrowVector.magnitude >= throwDeadzone)
            {
                /* 4 Directions
                if (absX >= absY && previousThrowVector.x < 0)
                {
                    if (throwVector.magnitude < throwDeadzone) { FiredAndCompleted = storedItem.FireLeft(); } else { storedItem.AimLeft(); }
                }
                else if (absX >= absY && previousThrowVector.x > 0)
                {
                    if (throwVector.magnitude < throwDeadzone) { FiredAndCompleted = storedItem.FireRight(); } else { storedItem.AimRight(); }
                }
                else if (absX <= absY && previousThrowVector.y < 0)
                {
                    if (throwVector.magnitude < throwDeadzone) { FiredAndCompleted = storedItem.FireUp(); } else { storedItem.AimUp(); }
                }
                else if (absX <= absY && previousThrowVector.y > 0)
                {
                    if (throwVector.magnitude < throwDeadzone) { FiredAndCompleted = storedItem.FireDown(); } else { storedItem.AimDown(); }
                }
                */
                if (throwVector.magnitude < throwDeadzone) { FiredAndCompleted = storedItem.Fire(previousThrowVector.normalized); } else { storedItem.Aim(previousThrowVector.normalized); }
            }

            previousThrowVector = throwVector;

            if (FiredAndCompleted)
            {
                CleanUpItem();
            }
        }
    }

    public bool PickUpItem(Item item)
    {
        if (storedItem)
        {
            return false;
        }
        storedItem = item;
        item.PickedUp(this);
        return true;
    }

    private void CleanUpItem()
    {
        itemIsActive = false;
        previousThrowVector = Vector3.zero;
        storedItem.CleanUp();
        Destroy(storedItem.gameObject);
        storedItem = null;
    }

    public Vector3 GetVelocity()
    {
        return lastFrameVelocity;
    }
	
	void OnTriggerStay(Collider collider){
        HerdSpawner sp = collider.gameObject.GetComponent<HerdSpawner>();

        if (sp) {
			//Debug.Log (playerManager.baseSpeed);
			//Debug.Log ("TRIGGER TOUCHING HERD");
			playerManager.baseSpeed = sp.speed;
			//Debug.Log (playerManager.baseSpeed);
			playerManager.playerSpeed = 0;
		}
	}

	void OnTriggerExit(Collider collider){
		if (collider.gameObject.GetComponent<HerdSpawner>()) {
			//Debug.Log ("Left collider");
			playerManager.baseSpeed = GameController.theGame.baseSpeed;
			playerManager.playerSpeed = GameController.theGame.playerSpeed;
		}
	}

    public void BlackHoleTriggered()
    {
        StartCoroutine(HandleTeleportParticles());
    }

    IEnumerator HandleTeleportParticles()
    {
        ParticleSystem.EmissionModule mod = sys1.emission;
        mod.enabled = true;
        mod = sys2.emission;
        mod.enabled = true;

        yield return new WaitForSeconds(teleportParticlesPlayTime);

        mod = sys1.emission;
        mod.enabled = false;
        mod = sys2.emission;
        mod.enabled = false;
    }

    public void PlayCheer(float time)
    {
        StartCoroutine(playCheerSound(time));
    }

    IEnumerator playCheerSound(float Time)
    {
        cheer.Play();
        yield return new WaitForSeconds(Time);
        cheer.Stop();
    }
}
