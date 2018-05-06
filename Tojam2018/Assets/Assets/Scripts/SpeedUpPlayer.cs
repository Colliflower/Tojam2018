using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPlayer : MonoBehaviour
{
    public float speedUpLength;
    public float speedUpAmount;
    public float bottleAngle;
    public float bottleSpinSpeed;
    public float bottleHeight;
    public Throwable throwScript;
    public SpinObject spinScript;
    public Transform bottleTransform;

    private PlayerController targetPC;
    private float startTime = -1;

    void Update()
    {
        if(startTime >= 0 && Time.time - startTime >= speedUpLength)
        {
            targetPC.speedUpAmount -= speedUpAmount;
            startTime = -1;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(startTime >= 0 || !other.attachedRigidbody)
        {
            return;
        }

        targetPC = other.attachedRigidbody.GetComponent<PlayerController>();
        if(targetPC)
        {
            targetPC.speedUpAmount += speedUpAmount;
            spinScript.transform.localRotation = Quaternion.identity;
            transform.parent = other.attachedRigidbody.transform;
            transform.localPosition = Vector3.up * bottleHeight;
            transform.forward = transform.parent.forward;
            bottleTransform.up = new Vector3(0, Mathf.Cos(Mathf.Deg2Rad * bottleAngle), Mathf.Sin(Mathf.Deg2Rad * bottleAngle));
            transform.localScale *= 0.75f;
            throwScript.velocity = Vector3.zero;
            throwScript.lifeTime = -1;
            spinScript.eulerSpin = new Vector3(0, bottleSpinSpeed, 0);
            startTime = Time.time;
        }
    }
}
