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
        if (collision.attachedRigidbody && !creator.Equals(collision.attachedRigidbody.gameObject) && collision.attachedRigidbody.GetComponent<PlayerController>()) {

            PlayerController creatorController = creator.GetComponent<PlayerController>();
            Vector3 relativeCreatorCamDistance = creatorController.cam.transform.position - creator.transform.position;

            PlayerController otherController = collision.attachedRigidbody.GetComponent<PlayerController>();
            Vector3 relativeOtherCamDistance = otherController.cam.transform.position - otherController.transform.position;

            Vector3 otherPosition = collision.attachedRigidbody.transform.position;
            collision.attachedRigidbody.transform.position = creator.transform.position;
            creator.transform.position = otherPosition;

            creatorController.cam.transform.position = creator.transform.position + relativeCreatorCamDistance;

            otherController.cam.transform.position = otherController.transform.position + relativeOtherCamDistance;

            creatorController.BlackHoleTriggered();
            otherController.BlackHoleTriggered();

            Destroy(gameObject);
        }
    }
}
