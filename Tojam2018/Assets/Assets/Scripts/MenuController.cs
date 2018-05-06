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
	public Texture creditImage;
	private GameObject[] options;
	private int index;
	private bool showCredits;
	private AudioSource hornSource;
	public AudioClip horn;
	// Use this for initialization
	void Start () {
		basemat = play.GetComponent<Renderer> ().material;
		options = new GameObject[3];
		options [0] = play;
		options [1] = credits;
		options [2] = exit;
		index = 0;
		showCredits = false;
		options [index % 3].GetComponent<Renderer> ().material = selectmat;
		hornSource = gameObject.AddComponent<AudioSource> ();
		hornSource.volume = 0.8f;
		hornSource.clip = horn;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("d")) {
			options [index % 3].GetComponent<Renderer> ().material = basemat;
			index += 1;
			options [index % 3].GetComponent<Renderer> ().material = selectmat;
			showCredits = false;
			hornSource.Play ();
		}
		if (Input.GetKeyDown ("a")) {
			options [index % 3].GetComponent<Renderer> ().material = basemat;
			index -= 1;
			if (index < 0) {
				index = 2;
			}
			showCredits = false;
			options [index % 3].GetComponent<Renderer> ().material = selectmat;
			hornSource.Play();

		}
		if (Input.GetKeyDown ("space")) {
			showCredits = false;
			if (index % 3 == 0) {
				GetComponent<AudioSource> ().Stop ();
				SceneManager.LoadScene("testScene");
			}
			else if (index % 3 == 1) {
				showCredits = true;
			}
			else if (index % 3 == 2) {
				Application.Quit();
			}


		}
		transform.Rotate (Vector3.up * 50 * Time.deltaTime);
	}

	void OnGUI(){
		if (showCredits) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), creditImage); 
		}
	}
}
