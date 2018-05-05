using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatController : MonoBehaviour {

	public float timer;
	private Vector3 moveVector;
	public float speed;
	public float step;
	private Vector3 initScale;
	public float upperThresh;
	public float lowerThresh;
	public float bounceScale;
	// Use this for initialization
	void Start () {
		initScale = transform.localScale;
		timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += step;
		if (Mathf.Abs (Mathf.Sin (timer)) < upperThresh) {
			if (Mathf.Abs (Mathf.Sin (timer)) > lowerThresh) {
				transform.localScale = new Vector3 (transform.localScale.x, Mathf.Abs (Mathf.Sin (timer))/upperThresh*initScale.y, transform.localScale.z);	
			}
			moveVector = new Vector3 (transform.position.x, 0, transform.position.z + speed*Time.fixedDeltaTime);
		} else {
			transform.localScale = initScale;
			moveVector = new Vector3 (transform.position.x, Mathf.Abs (Mathf.Sin (timer))*bounceScale, transform.position.z + speed*Time.fixedDeltaTime);
		}


		transform.position = moveVector;
		if (transform.position.y > 10) {
			Destroy (gameObject);
		}
	}
}
