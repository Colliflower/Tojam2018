using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public GameObject[] sourceBuildings;

    public float mapLength;

    public float spacing;

    public float width;

    public float offset;

    private List<GameObject> leftBuildings;

    private List<GameObject> rightBuildings;

    // Use this for initialization
    void Start () {
        float currLength = 0;

		if (sourceBuildings.Length > 0)
        {
            leftBuildings = new List<GameObject>();
            
            int buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

            Vector3 buildingScale = sourceBuildings[buildingIx].transform.localScale;

            Vector3 buildingPosition = new Vector3(-(width + buildingScale.x / 2), buildingScale.y / 2, currLength + buildingScale.z / 2 + offset);

            GameObject instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, Quaternion.identity);

            leftBuildings.Add(instance);

            currLength += buildingScale.z;
            currLength += spacing;

            while (currLength < mapLength)
            {
                buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

                buildingScale = sourceBuildings[buildingIx].transform.localScale;

                buildingPosition.x = -(width + buildingScale.x / 2);
                buildingPosition.y = buildingScale.y / 2;
                buildingPosition.z = currLength + buildingScale.z / 2 + offset;
                
                instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, Quaternion.identity);

                leftBuildings.Add(instance);

                currLength += buildingScale.z;
                currLength += spacing;

                //Debug.Log("Curr: " + currLength.ToString() + ", Max: " + mapLength.ToString());
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
