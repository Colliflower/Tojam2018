using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    public GameObject p1Win;
    public GameObject p2Win;
    int playersFinished = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other){
        PlayerManagerController pmc;
        PlayerController pc;
        if (other.rigidbody && other.rigidbody.transform.parent && (pmc = other.rigidbody.transform.parent.GetComponent<PlayerManagerController>()) != null && (pc = other.rigidbody.GetComponent<PlayerController>()) != null)
        {
            Debug.Log(pmc.name + " Finished!");
            pmc.baseSpeed = 0;
            pmc.playerSpeed = 0;
            playersFinished++;
            if (playersFinished == GameController.theGame.playerCount - 1)
            {
                if (GameController.theGame.lastPlayer.GetComponent<PlayerController>().playerId == 1)
                {
                    p1Win.SetActive(true);
                }
                else
                {
                    p2Win.SetActive(true);
                }
            }
        }
	}
}
