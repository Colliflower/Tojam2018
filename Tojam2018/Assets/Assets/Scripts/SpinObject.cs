using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public Vector3 eulerSpin;

	void Update ()
    {
        transform.Rotate(eulerSpin * Time.deltaTime);
	}
}
