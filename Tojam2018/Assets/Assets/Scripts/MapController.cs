using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public GameObject invisbleFence;

    public GameObject fence;

    public GameObject[] sourceBuildings;

    public float mapLength;

    public float buildingSpacing;

    public float buildingWidth;

    public float buildingOffset;

    public float fenceWidth;

    public float fenceSpacing;

    public float fenceOffset;

    private List<GameObject> leftBuildings;

    private List<GameObject> rightBuildings;

    private List<GameObject> fences;

    // Use this for initialization
    void Start () {
        float currLength = 0;

        Vector3 invisFenceScale = invisbleFence.GetComponent<Renderer>().bounds.size;

        Vector3 invisFencePosition = new Vector3(-(fenceWidth + invisFenceScale.x / 2), invisFenceScale.y / 2, mapLength / 2 + invisFenceScale.z / 2 + fenceOffset);

        Debug.Log(fenceOffset);
        Debug.Log(invisFencePosition);

        GameObject invisFenceInstance = Instantiate(invisbleFence, invisFencePosition, Quaternion.identity);

        Vector3 scale = invisFenceInstance.transform.localScale;

        invisFenceInstance.transform.localScale = new Vector3(scale.x, scale.y, scale.z * mapLength);

        invisFencePosition.x = fenceWidth + invisFenceScale.x / 2;

        invisFenceInstance = Instantiate(invisbleFence, invisFencePosition, Quaternion.identity);

        scale = invisFenceInstance.transform.localScale;

        invisFenceInstance.transform.localScale = new Vector3(scale.x, scale.y, scale.z * mapLength);

        if (sourceBuildings.Length > 0)
        {
            // Left

            leftBuildings = new List<GameObject>();
            
            int buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

            Vector3 buildingScale = sourceBuildings[buildingIx].transform.localScale;

            Vector3 buildingPosition = new Vector3(-(buildingWidth + buildingScale.x / 2), buildingScale.y / 2, currLength + buildingScale.z / 2 + buildingOffset);

            GameObject instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, Quaternion.identity);

            leftBuildings.Add(instance);

            currLength += buildingScale.z;
            currLength += buildingSpacing;

            while (currLength < mapLength)
            {
                buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

                buildingScale = sourceBuildings[buildingIx].transform.localScale;

                buildingPosition.x = -(buildingWidth + buildingScale.x / 2);
                buildingPosition.y = buildingScale.y / 2;
                buildingPosition.z = currLength + buildingScale.z / 2 + buildingOffset;
                
                instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, Quaternion.identity);

                leftBuildings.Add(instance);

                currLength += buildingScale.z;
                currLength += buildingSpacing;
            }

            // Right

            currLength = 0;

            rightBuildings = new List<GameObject>();

            buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

            buildingScale = sourceBuildings[buildingIx].transform.localScale;

            buildingPosition.x = buildingWidth + buildingScale.x / 2;
            buildingPosition.y = buildingScale.y / 2;
            buildingPosition.z = currLength + buildingScale.z / 2 + buildingOffset;
            
            Quaternion orientation = Quaternion.Euler(new Vector3(0, 180, 0));

            instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, orientation);

            rightBuildings.Add(instance);

            currLength += buildingScale.z;
            currLength += buildingSpacing;


            while (currLength < mapLength)
            {
                buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

                buildingScale = sourceBuildings[buildingIx].transform.localScale;

                buildingPosition.x = buildingWidth + buildingScale.x / 2;
                buildingPosition.y = buildingScale.y / 2;
                buildingPosition.z = currLength + buildingScale.z / 2 + buildingOffset;

                instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, orientation);

                rightBuildings.Add(instance);

                currLength += buildingScale.z;
                currLength += buildingSpacing;
            }
        }

        currLength = 0;

        fences = new List<GameObject>();

        Vector3 fenceScale = fence.GetComponent<Renderer>().bounds.size;

        Vector3 fencePosition = new Vector3(-(fenceWidth + fenceScale.x / 2), 0, currLength + fenceScale.z / 2 + fenceOffset);

        Quaternion fenceOrientation = Quaternion.Euler(new Vector3(-90, 0, 90));

        GameObject fenceInstance = Instantiate(fence, fencePosition, fenceOrientation);

        fences.Add(fenceInstance);
        
        fencePosition.x = fenceWidth + fenceScale.x / 2;

        fenceInstance = Instantiate(fence, fencePosition, fenceOrientation);

        fences.Add(fenceInstance);

        currLength += fenceScale.z;
        currLength += fenceSpacing;

        while (currLength < mapLength)
        {
            fencePosition.x = -(fenceWidth + fenceScale.x / 2);
            fencePosition.y = 0;
            fencePosition.z = currLength + fenceScale.z / 2 + fenceOffset;

            fenceInstance = Instantiate(fence, fencePosition, fenceOrientation);

            fences.Add(fenceInstance);

            fencePosition.x = fenceWidth + fenceScale.x / 2;

            fenceInstance = Instantiate(fence, fencePosition, fenceOrientation);

            fences.Add(fenceInstance);

            currLength += fenceScale.z;
            currLength += fenceSpacing;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
