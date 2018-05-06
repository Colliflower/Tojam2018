using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public GameObject play;
	public GameObject credits;
	public GameObject exit;
	private Material basemat;
	public Material selectmat;
	private GameObject[] options;
	private int index;
	// Use this for initialization
	void Start () {
		basemat = play.GetComponent<Renderer> ().material;
		options = new GameObject[3];
		options [0] = play;
		options [1] = credits;
		options [2] = exit;
		index = 0;
		options [index % 3].GetComponent<Renderer> ().material = selectmat;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("d")) {
			options [index % 3].GetComponent<Renderer> ().material = basemat;
			index += 1;
			options [index % 3].GetComponent<Renderer> ().material = selectmat;
		}
		if (Input.GetKeyDown ("a")) {
			options [index % 3].GetComponent<Renderer> ().material = basemat;
			index -= 1;
			if (index < 0) {
				index = 2;
			}
			options [index % 3].GetComponent<Renderer> ().material = selectmat;

		}
		if (Input.GetKeyDown ("space")) {
			if (index % 3 == 0) {
				GetComponent<AudioSource> ().Stop ();
				SceneManager.LoadScene("testScene", LoadSceneMode.Additive);
			}
			else if (index % 3 == 2) {
				Application.Quit();
			}


		}
		transform.Rotate (Vector3.up * 50 * Time.deltaTime);
	}
}
