using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController theGame;

    public float playerSpeed;
    public float baseSpeed;
	public GameObject lastPlayer = null;
	public GameObject lastManager = null;
	public GameObject firstPlayer = null;
	public GameObject firstManager = null;
	private GameObject[] playerManagers;
	float minZ;
	float maxZ;
    // Use this for initialization
    void Start () {
		minZ = Mathf.Infinity;
		maxZ = Mathf.NegativeInfinity;
        theGame = this;
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
