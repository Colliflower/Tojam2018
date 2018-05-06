using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdSpawner : MonoBehaviour {
	public GameObject goat;
	public int numGoats;
	private GameObject[] goats;
	private int counter;
	public float xOffset;
	public float zOffset;
	public float speed;
	private float timer;
	public float step;
    public float variance = .2f;
	private bool toDestroy;
    public ParticleSystem particles;
	// Use this for initialization
	void Start () {
		timer = 0;
		toDestroy = false;
		goats = new GameObject[numGoats];
		for (int i = 0; i < 2; i++) {
			goats[counter] = Instantiate(goat,new Vector3(transform.position.x-(i - 0.5f)*xOffset + (Random.value * 2 - 1) * variance,0,transform.position.z + (Random.value * 2 - 1) * variance), Quaternion.identity);
			goats[counter].GetComponent<GoatController> ().bounceScale = Random.Range (1, 4);
			goats[counter].GetComponent<GoatController> ().step = Random.Range (0.1f, 0.3f);
			goats[counter].GetComponent<GoatController> ().timer = Random.Range (0.0f, Mathf.PI);
			goats[counter].GetComponent<GoatController> ().speed = speed;
			counter++;

		}
		for (int i = 0; i < 3; i++) {
			goats[counter] = Instantiate(goat,new Vector3(transform.position.x - (i - 1) * xOffset / 3 * 2 + (Random.value * 2 - 1) * variance, 0, transform.position.z - zOffset + (Random.value * 2 - 1) * variance), Quaternion.identity);
			goats[counter].GetComponent<GoatController> ().bounceScale = Random.Range (1, 4);
			goats[counter].GetComponent<GoatController> ().step = Random.Range (0.1f, 0.3f);
			goats[counter].GetComponent<GoatController> ().timer = Random.Range (0.0f, Mathf.PI);
			goats[counter].GetComponent<GoatController> ().speed = speed;
			counter++;
		}
		for (int i = 0; i < 2; i++) {
			goats[counter] = Instantiate(goat,new Vector3(transform.position.x - (i - 0.5f) * xOffset + (Random.value * 2 - 1) * variance, 0, transform.position.z - zOffset* 2 + (Random.value * 2 - 1) * variance), Quaternion.identity);
			goats[counter].GetComponent<GoatController> ().bounceScale = Random.Range (1, 4);
			goats[counter].GetComponent<GoatController> ().step = Random.Range (0.1f, 0.3f);
			goats[counter].GetComponent<GoatController> ().timer = Random.Range (0.0f, Mathf.PI);
			goats[counter].GetComponent<GoatController> ().speed = speed;
			counter++;
		}
	}
	void FixedUpdate(){
		if (!toDestroy) {
			timer += step;
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z + speed * Time.fixedDeltaTime);
			if (timer >= 100) {
				for (int i = 0; i < numGoats; i++) {
					goats [i].GetComponent<GoatController> ().bounceScale = 20;
					//goats [i].GetComponent<GoatController> ().step = 0.01f;
				}
				toDestroy = true;
                particles.Stop();
			}
		} else {
			timer += step;
			if (timer >= 200) {
				Destroy (gameObject);
			}
		}
	}
}
