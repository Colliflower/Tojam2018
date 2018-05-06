using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlackHoleController : MonoBehaviour {
    public GameObject creator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Rigidbody>() && !creator.Equals(collision.GetComponent<Rigidbody>().gameObject) && collision.GetComponent<Rigidbody>().GetComponent<PlayerController>()) {
            Vector3 playerPos = collision.GetComponent<Rigidbody>().transform.position;
            collision.GetComponent<Rigidbody>().transform.position = creator.transform.position;
            creator.transform.position = playerPos;

            creator.GetComponent<PlayerController>().BlackHoleTriggered();
            collision.GetComponent<Rigidbody>().GetComponent<PlayerController>().BlackHoleTriggered();
            Debug.Log("test");

            Destroy(gameObject);
        }
    }
}
