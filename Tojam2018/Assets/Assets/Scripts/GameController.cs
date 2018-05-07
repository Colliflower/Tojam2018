using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController theGame;

    public float playerSpeed;
    public float baseSpeed;
	public GameObject lastPlayer = null;
	public GameObject lastManager = null;
	public GameObject firstPlayer = null;
	public GameObject firstManager = null;
    public int playerCount = 2;
	private GameObject[] playerManagers;
	float minZ;
	float maxZ;

    void Start()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    }
    // Use this for initialization
    void Awake () {
		minZ = Mathf.Infinity;
		maxZ = Mathf.NegativeInfinity;
        theGame = this;
    }

    void Update()
    {
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
