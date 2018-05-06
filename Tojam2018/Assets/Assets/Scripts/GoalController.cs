using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other){
		Debug.Log (other.transform.parent.gameObject.name + " Finished!");
		other.transform.parent.gameObject.GetComponent<PlayerManagerController> ().baseSpeed = 0;
	}
}
