using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourScript : MonoBehaviour
{
    public Color colour;

	// Use this for initialization
	void Start ()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colour);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
