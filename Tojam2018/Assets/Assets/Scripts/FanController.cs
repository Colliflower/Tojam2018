using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]
    public int direction;
    [HideInInspector]
    public GameObject idol;
    [HideInInspector]
    public float baseSpeed;
    [HideInInspector]
    public float timer;
    [HideInInspector]
    public float animOffset;
    [HideInInspector]
    public float duration;
    public float hopSpeed;
    public float moveSpeedScale;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (direction == 0) {
			transform.position = Vector3.MoveTowards(transform.position, idol.transform.position-idol.transform.right*2+new Vector3(0,transform.localScale.y,0), baseSpeed*moveSpeedScale);
		}
		else if (direction == 1) {
			transform.position = Vector3.MoveTowards(transform.position, idol.transform.position+idol.transform.right*2+new Vector3(0,transform.localScale.y,0), baseSpeed * moveSpeedScale);
		}
		else if (direction == 2) {
			transform.position = Vector3.MoveTowards(transform.position, idol.transform.position-idol.transform.forward*1.5f+new Vector3(0,transform.localScale.y,0), baseSpeed * moveSpeedScale);
		}
		else if (direction == 3) {
			transform.position = Vector3.MoveTowards(transform.position, idol.transform.position-idol.transform.forward+idol.transform.right*1.5f+new Vector3(0,transform.localScale.y,0), baseSpeed * moveSpeedScale);
		}
		else if (direction == 4) {
			transform.position = Vector3.MoveTowards(transform.position, idol.transform.position-idol.transform.forward-idol.transform.right*1.5f+new Vector3(0,transform.localScale.y,0), baseSpeed * moveSpeedScale);
		}
		if (Vector3.Distance (transform.position, idol.transform.position) < 5) {
			timer += Time.deltaTime;
			idol.GetComponent<PlayerController> ().playerManager.baseSpeed = baseSpeed*2;
			transform.position = new Vector3 (transform.position.x, Mathf.Abs (Mathf.Sin (timer * hopSpeed + animOffset)), transform.position.z);

		}
		if (timer >= duration) {
			idol.GetComponent<PlayerController> ().playerManager.baseSpeed = baseSpeed;
			Destroy (gameObject);
		}
	}
}
