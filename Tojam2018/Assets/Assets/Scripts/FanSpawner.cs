using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpawner : MonoBehaviour {

	public GameObject manager;
	public GameObject fan;
	public float duration;
	private Color[] colors;
	// Use this for initialization
	void Start () {
		colors = new Color[5];
		colors [0] = Color.blue;
		colors [1] = Color.green;
		colors [2] = Color.red;
		colors [3] = Color.yellow;
		colors [4] = Color.magenta;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("z")) {
			for (int i = 0; i < 5; i++) {
				GameObject fanInstance = Instantiate (fan, transform.position, Quaternion.Euler(0,90,0));
				fanInstance.GetComponent<FanController> ().idol = manager.GetComponent<GameController> ().lastPlayer;
				fanInstance.GetComponent<FanController> ().direction = i;
				fanInstance.GetComponent<FanController> ().baseSpeed = manager.GetComponent<GameController> ().baseSpeed;
				fanInstance.GetComponent<FanController> ().timer = Mathf.PI*i/5;
				fanInstance.GetComponent<FanController> ().step = 0.2f;
				fanInstance.GetComponent<FanController> ().duration = duration;
				foreach (Transform child in fanInstance.transform) {
					child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colors[i]);
				}

			}
			/*
			GameObject fanInstanceLeft = Instantiate (fan, transform.position, Quaternion.Euler(0,90,0));
			fanInstanceLeft.GetComponent<FanController> ().idol = manager.GetComponent<GameController> ().lastPlayer;
			fanInstanceLeft.GetComponent<FanController> ().direction = 1;
			fanInstanceLeft.GetComponent<FanController> ().baseSpeed = manager.GetComponent<GameController> ().baseSpeed;
			fanInstanceLeft.GetComponent<FanController> ().timer = Mathf.PI*2/3;
			fanInstanceLeft.GetComponent<FanController> ().step = 0.2f;
			//fanInstanceLeft.GetComponent<CapsuleCollider>().enabled = false;


			GameObject fanInstanceRight = Instantiate (fan, transform.position, Quaternion.Euler(0,90,0));
			fanInstanceRight.GetComponent<FanController> ().idol = manager.GetComponent<GameController> ().lastPlayer;
			fanInstanceRight.GetComponent<FanController> ().direction = 2;
			fanInstanceRight.GetComponent<FanController> ().baseSpeed = manager.GetComponent<GameController> ().baseSpeed;
			fanInstanceRight.GetComponent<FanController> ().timer = Mathf.PI/3;
			fanInstanceRight.GetComponent<FanController> ().step = 0.2f;
			//fanInstanceRight.GetComponent<CapsuleCollider>().enabled = false;

			GameObject fanInstanceDown = Instantiate (fan, transform.position, Quaternion.Euler(0,90,0));
			fanInstanceDown.GetComponent<FanController> ().idol = manager.GetComponent<GameController> ().lastPlayer;
			fanInstanceDown.GetComponent<FanController> ().direction = 3;
			fanInstanceDown.GetComponent<FanController> ().timer = 0;
			fanInstanceDown.GetComponent<FanController> ().step = 0.2f;
			fanInstanceDown.GetComponent<FanController> ().baseSpeed = manager.GetComponent<GameController> ().baseSpeed;
			//fanInstanceDown.GetComponent<CapsuleCollider>().enabled = false;

			GameObject fanInstanceDownRight = Instantiate (fan, transform.position, Quaternion.Euler(0,90,0));
			fanInstanceDownRight.GetComponent<FanController> ().idol = manager.GetComponent<GameController> ().lastPlayer;
			fanInstanceDownRight.GetComponent<FanController> ().direction = 4;
			fanInstanceDownRight.GetComponent<FanController> ().timer = 0;
			fanInstanceDownRight.GetComponent<FanController> ().step = 0.2f;
			fanInstanceDownRight.GetComponent<FanController> ().baseSpeed = manager.GetComponent<GameController> ().baseSpeed;

			GameObject fanInstanceDownLeft = Instantiate (fan, transform.position, Quaternion.Euler(0,90,0));
			fanInstanceDownLeft.GetComponent<FanController> ().idol = manager.GetComponent<GameController> ().lastPlayer;
			fanInstanceDownLeft.GetComponent<FanController> ().direction = 5;
			fanInstanceDownLeft.GetComponent<FanController> ().timer = 0;
			fanInstanceDownLeft.GetComponent<FanController> ().step = 0.2f;
			fanInstanceDownLeft.GetComponent<FanController> ().baseSpeed = manager.GetComponent<GameController> ().baseSpeed;*/

		}
	}
}
