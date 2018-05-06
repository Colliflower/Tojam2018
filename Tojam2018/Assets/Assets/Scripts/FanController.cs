using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour {

	// Use this for initialization
	public int direction;
	public GameObject idol;
	public float baseSpeed;
	public float timer;
	public float step;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (direction == 1) {
			transform.position = Vector3.MoveTowards(transform.position, idol.transform.position-idol.transform.right*2+new Vector3(0,transform.localScale.y,0), 0.5f);
		}
		else if (direction == 2) {
			transform.position = Vector3.MoveTowards(transform.position, idol.transform.position+idol.transform.right*2+new Vector3(0,transform.localScale.y,0), 0.5f);
		}
		if (direction == 3) {
			transform.position = Vector3.MoveTowards(transform.position, idol.transform.position-idol.transform.forward*2+new Vector3(0,transform.localScale.y,0), 0.5f);
		}
		if (Vector3.Distance (transform.position, idol.transform.position) < 5) {
			timer += step;
			idol.GetComponent<PlayerController> ().playerManager.baseSpeed = baseSpeed*2;
			transform.position = new Vector3 (transform.position.x, Mathf.Abs (Mathf.Sin (timer)), transform.position.z);

		}
	}
}
