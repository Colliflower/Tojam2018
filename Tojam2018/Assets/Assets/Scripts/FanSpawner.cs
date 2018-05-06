using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpawner : MonoBehaviour {

	public GameObject manager;
	public GameObject fan;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("z")) {
			GameObject fanInstanceLeft = Instantiate (fan, transform.position, Quaternion.identity);
			fanInstanceLeft.GetComponent<FanController> ().idol = manager.GetComponent<GameController> ().lastPlayer;
			fanInstanceLeft.GetComponent<FanController> ().direction = 1;
			fanInstanceLeft.GetComponent<FanController> ().baseSpeed = manager.GetComponent<GameController> ().baseSpeed;
			fanInstanceLeft.GetComponent<FanController> ().timer = Mathf.PI*2/3;
			fanInstanceLeft.GetComponent<FanController> ().step = 0.2f;
			fanInstanceLeft.GetComponent<CapsuleCollider>().enabled = false;


			GameObject fanInstanceRight = Instantiate (fan, transform.position, Quaternion.identity);
			fanInstanceRight.GetComponent<FanController> ().idol = manager.GetComponent<GameController> ().lastPlayer;
			fanInstanceRight.GetComponent<FanController> ().direction = 2;
			fanInstanceRight.GetComponent<FanController> ().baseSpeed = manager.GetComponent<GameController> ().baseSpeed;
			fanInstanceRight.GetComponent<FanController> ().timer = Mathf.PI/3;
			fanInstanceRight.GetComponent<FanController> ().step = 0.2f;
			fanInstanceRight.GetComponent<CapsuleCollider>().enabled = false;

			GameObject fanInstanceDown = Instantiate (fan, transform.position, Quaternion.identity);
			fanInstanceDown.GetComponent<FanController> ().idol = manager.GetComponent<GameController> ().lastPlayer;
			fanInstanceDown.GetComponent<FanController> ().direction = 3;
			fanInstanceDown.GetComponent<FanController> ().timer = 0;
			fanInstanceDown.GetComponent<FanController> ().step = 0.2f;
			fanInstanceDown.GetComponent<FanController> ().baseSpeed = manager.GetComponent<GameController> ().baseSpeed;
			fanInstanceDown.GetComponent<CapsuleCollider>().enabled = false;
		}
	}
}
