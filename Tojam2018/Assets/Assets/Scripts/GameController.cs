using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController theGame;

    public float playerSpeed;
    public float baseSpeed;
    private float initPlayerSpeed;
    private float initBaseSpeed;
	public GameObject lastPlayer = null;
	public GameObject lastManager = null;
	public GameObject firstPlayer = null;
	public GameObject firstManager = null;
    public int playerCount = 2;
	private GameObject[] playerManagers;
	float minZ;
	float maxZ;
    private int counter;
    public float step;
    private float stepCounter;

    public GameObject[] startCounter;

    public AudioClip[] startSounds;
    private AudioSource gameSounds;

    void Start()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        counter = 3;
        stepCounter = 0;
        gameSounds = GetComponent<AudioSource>();
        initBaseSpeed = baseSpeed;
        initPlayerSpeed = playerSpeed;
        baseSpeed = 0;
        playerSpeed = 0;
    }
    // Use this for initialization
    void Awake () {
		minZ = Mathf.Infinity;
		maxZ = Mathf.NegativeInfinity;
        theGame = this;
    }

    void Update()
    {
        if (counter >= 0)
        {
            stepCounter += step;
            if (stepCounter > 10)
            {
                startCounter[counter].SetActive(false);
                counter -= 1;
                stepCounter = 0;
                if (counter >= 0)
                {
                    gameSounds.clip = startSounds[counter];
                    gameSounds.Play();
                    startCounter[counter].SetActive(true);
                }
                if (counter == 0)
                {
                    baseSpeed = initBaseSpeed;
                    playerSpeed = initPlayerSpeed;
                    foreach (GameObject playerManager in playerManagers)
                    {
                        playerManager.GetComponent<PlayerManagerController>().baseSpeed = baseSpeed;
                        playerManager.GetComponent<PlayerManagerController>().playerSpeed = playerSpeed;
                    }
                }
            }
        }
        if (Input.GetButtonDown("menu_Start"))
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate () {
		Transform currPlayer;
		minZ = Mathf.Infinity;
		maxZ = Mathf.NegativeInfinity;
		playerManagers = GameObject.FindGameObjectsWithTag("PMTag");
		foreach (GameObject playerManager in playerManagers) {
			currPlayer = playerManager.transform.Find ("Player");
			if (currPlayer.position.z < minZ){
				minZ = currPlayer.position.z;
				lastPlayer = currPlayer.gameObject;
				lastManager = playerManager;
			}
			if (currPlayer.position.z > maxZ) {
				maxZ = currPlayer.position.z;
				firstPlayer = currPlayer.gameObject;
				firstManager = playerManager;
			}
		}
		//Debug.Log ("Last place is " + lastManager.name);
	}
}
