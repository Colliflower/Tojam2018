using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
    
    public GameObject invisbleFence;

    public GameObject[] sourceFences;

    public GameObject[] sourceBuildings;

    public GameObject[] sourceGrounds;

    public GameObject itemBoxPrefab;

    public GameObject[] items;

    public float renderDistance;

    public float mapLength;

    public float buildingSpacing;
    public float buildingWidth;
    public float buildingOffset;

    public float fenceWidth;
    public float fenceSpacing;
    public float fenceOffset;
    
    public float groundOffset;
    
    public float trackRadius = 4.5f;
    public float itemBoxSeparation = 5;
    public float itemBoxGap = 2;
    public float itemBoxChance = .25f;

    private List<GameObject> leftBuildings;
    private List<GameObject> rightBuildings;

    private List<GameObject> leftFences;
    private List<GameObject> rightFences;
    
    private List<GameObject> grounds;

    private List<GameObject[]> itemBoxes;

    private float lastItemAttempt;

    // Use this for initialization
    void Start () {
        Vector3 invisFenceScale = invisbleFence.GetComponent<Renderer>().bounds.size;

        Vector3 invisFencePosition = new Vector3(-(fenceWidth + invisFenceScale.x / 2), invisFenceScale.y / 2, mapLength / 2 + invisFenceScale.z / 2 + fenceOffset);

        GameObject invisFenceInstance = Instantiate(invisbleFence, invisFencePosition, Quaternion.identity);

        Vector3 scale = invisFenceInstance.transform.localScale;

        invisFenceInstance.transform.localScale = new Vector3(scale.x, scale.y, scale.z * mapLength);

        invisFencePosition.x = fenceWidth + invisFenceScale.x / 2;

        invisFenceInstance = Instantiate(invisbleFence, invisFencePosition, Quaternion.identity);

        scale = invisFenceInstance.transform.localScale;

        invisFenceInstance.transform.localScale = new Vector3(scale.x, scale.y, scale.z * mapLength);

        leftBuildings = new List<GameObject>();
        
        rightBuildings = new List<GameObject>();

        leftFences = new List<GameObject>();

        rightFences = new List<GameObject>();

        grounds = new List<GameObject>();

        itemBoxes = new List<GameObject[]>();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject frontMostPlayer = GameController.theGame.firstPlayer;

        float currentFrontier = frontMostPlayer.transform.position.z;

        SpawnObjects(sourceBuildings, leftBuildings, currentFrontier, buildingSpacing, buildingOffset, -buildingWidth, Quaternion.identity);
        SpawnObjects(sourceBuildings, rightBuildings, currentFrontier, buildingSpacing, buildingOffset, buildingWidth, Quaternion.identity);
        SpawnObjects(sourceFences, leftFences, currentFrontier, fenceSpacing, fenceOffset, -fenceWidth, Quaternion.identity);
        SpawnObjects(sourceFences, rightFences, currentFrontier, fenceSpacing, fenceOffset, fenceWidth, Quaternion.identity);

        SpawnObjects(sourceGrounds, grounds, currentFrontier, 0, groundOffset, 0, Quaternion.identity, false);

        // SpawnItemBox(currentFrontier);

        GameObject backMostPlayer = GameController.theGame.lastPlayer;

        currentFrontier = backMostPlayer.transform.position.z;

        DeSpawnObjects(leftBuildings, currentFrontier);
        DeSpawnObjects(rightBuildings, currentFrontier);
        DeSpawnObjects(leftFences, currentFrontier);
        DeSpawnObjects(rightFences, currentFrontier);
        DeSpawnObjects(grounds, currentFrontier);

        //float currLength;

        //if (sourceBuildings.Length > 0)
        //{
        //    // Left
        //    if (leftBuildings.Count > 0)
        //    {
        //        GameObject frontMostBuilding = leftBuildings[leftBuildings.Count - 1];

        //        currLength = frontMostBuilding.transform.position.z + frontMostBuilding.GetComponent<Renderer>().bounds.size.z * 3 / 2 + buildingSpacing;

        //    }
        //    else
        //    {
        //        currLength = 0;
        //    }

        //    if (currLength < mapLength && currLength < currentFrontier + renderDistance)
        //    {
        //        int buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

        //        Vector3 buildingScale = sourceBuildings[buildingIx].GetComponent<Renderer>().bounds.size;

        //        Vector3 buildingPosition = new Vector3(-(buildingWidth + buildingScale.x / 2), buildingScale.y / 2, currLength + buildingScale.z / 2 + buildingOffset);

        //        GameObject instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, Quaternion.identity);

        //        leftBuildings.Add(instance);

        //        currLength += buildingScale.z;
        //        currLength += buildingSpacing;

        //        while (currLength < mapLength && currLength < currentFrontier + renderDistance)
        //        {
        //            buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

        //            buildingScale = sourceBuildings[buildingIx].GetComponent<Renderer>().bounds.size;

        //            buildingPosition.x = -(buildingWidth + buildingScale.x / 2);
        //            buildingPosition.y = buildingScale.y / 2;
        //            buildingPosition.z = currLength + buildingScale.z / 2 + buildingOffset;

        //            instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, Quaternion.identity);

        //            leftBuildings.Add(instance);

        //            currLength += buildingScale.z;
        //            currLength += buildingSpacing;
        //        }
        //    }


        //    // Right

        //    if (rightBuildings.Count > 0)
        //    {
        //        GameObject frontMostBuilding = rightBuildings[rightBuildings.Count - 1];

        //        currLength = frontMostBuilding.transform.position.z + frontMostBuilding.GetComponent<Renderer>().bounds.size.z * 3 / 2 + buildingSpacing;
        //    }
        //    else
        //    {
        //        currLength = 0;
        //    }

        //    if (currLength < mapLength && currLength < currentFrontier + renderDistance)
        //    {
        //        int buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

        //        Vector3 buildingScale = sourceBuildings[buildingIx].GetComponent<Renderer>().bounds.size;

        //        //Debug.Log("Bounds: " + buildingScale.ToString() + ", Local: " + sourceBuildings[buildingIx].transform.localScale.ToString());

        //        Vector3 buildingPosition = new Vector3((buildingWidth + buildingScale.x / 2), buildingScale.y / 2, currLength + buildingScale.z / 2 + buildingOffset);

        //        Quaternion orientation = Quaternion.Euler(new Vector3(0, 180, 0));

        //        GameObject instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, orientation);

        //        rightBuildings.Add(instance);

        //        currLength += buildingScale.z;
        //        currLength += buildingSpacing;

        //        while (currLength < mapLength && currLength < currentFrontier + renderDistance)
        //        {
        //            buildingIx = Mathf.RoundToInt(Random.value * (sourceBuildings.Length - 1));

        //            buildingScale = sourceBuildings[buildingIx].transform.localScale;

        //            buildingPosition.x = buildingWidth + buildingScale.x / 2;
        //            buildingPosition.y = buildingScale.y / 2;
        //            buildingPosition.z = currLength + buildingScale.z / 2 + buildingOffset;

        //            instance = Instantiate(sourceBuildings[buildingIx], buildingPosition, orientation);

        //            rightBuildings.Add(instance);

        //            currLength += buildingScale.z;
        //            currLength += buildingSpacing;
        //        }
        //    }
        //}

        //currLength = 0;

        //fences = new List<GameObject>();

        //Vector3 fenceScale = fence.GetComponent<Renderer>().bounds.size;

        //Vector3 fencePosition = new Vector3(-(fenceWidth + fenceScale.x / 2), 0, currLength + fenceScale.z / 2 + fenceOffset);

        //Quaternion fenceOrientation = Quaternion.Euler(new Vector3(-90, 0, 90));

        //GameObject fenceInstance = Instantiate(fence, fencePosition, fenceOrientation);

        //fences.Add(fenceInstance);

        //fencePosition.x = fenceWidth + fenceScale.x / 2;

        //fenceInstance = Instantiate(fence, fencePosition, fenceOrientation);

        //fences.Add(fenceInstance);

        //currLength += fenceScale.z;
        //currLength += fenceSpacing;

        //while (currLength < mapLength)
        //{
        //    fencePosition.x = -(fenceWidth + fenceScale.x / 2);
        //    fencePosition.y = 0;
        //    fencePosition.z = currLength + fenceScale.z / 2 + fenceOffset;

        //    fenceInstance = Instantiate(fence, fencePosition, fenceOrientation);

        //    fences.Add(fenceInstance);

        //    fencePosition.x = fenceWidth + fenceScale.x / 2;

        //    fenceInstance = Instantiate(fence, fencePosition, fenceOrientation);

        //    fences.Add(fenceInstance);

        //    currLength += fenceScale.z;
        //    currLength += fenceSpacing;
        //}
    }

    private void SpawnItemBox(float currentFrontier)
    {
        int boxLineCount = Mathf.FloorToInt((currentFrontier - lastItemAttempt) / itemBoxSeparation);
        if (boxLineCount > 0)
        {
            for (int i = 0; i < boxLineCount; i++)
            {
                float rand = Random.value;
                GameObject[] obj = new GameObject[GameController.theGame.playerCount / 2];
                for (int j = 0; j < GameController.theGame.playerCount / 2; j++)
                {
                    Vector3 pos = new Vector3(rand * (trackRadius * 2 - (GameController.theGame.playerCount / 2 - 1) * itemBoxGap) + j * itemBoxGap - trackRadius, 0, lastItemAttempt + (i + 1) * itemBoxSeparation);
                    ItemBox box = Instantiate(itemBoxPrefab, pos, Quaternion.identity).GetComponent<ItemBox>();
                    int itemRand = Mathf.RoundToInt(Random.value * (items.Length - 1));
                    box.itemPrefab = items[itemRand];
                    obj[j] = box.gameObject;
                }
                itemBoxes.Add(obj);
            }

            lastItemAttempt += boxLineCount * itemBoxSeparation;
        }
    }

    private void DeSpawnItemBox(float currentFrontier)
    {
        if(itemBoxes.Count > 0)
        {
            if(itemBoxes[0][0].transform.position.z + renderDistance < currentFrontier)
            {
                for(int i = 0; i < itemBoxes[0].Length; i++)
                {
                    Destroy(itemBoxes[0][i]);
                }
                itemBoxes.RemoveAt(0);
            }
        }
    }

    private void SpawnObjects(GameObject[] sourceObjects, List<GameObject> currentObjects, float currentFrontier, float spacing, float offset, float width, Quaternion orientation, bool shouldCenter = true)
    {
        if (sourceObjects.Length > 0)
        {
            float currLength;

            if (currentObjects.Count > 0)
            {
                GameObject frontMostObject = currentObjects[currentObjects.Count - 1];

                currLength = frontMostObject.transform.position.z + frontMostObject.GetComponent<BoxCollider>().size.z / 2+ spacing;

                //if (currLength < currentFrontier + renderDistance)
                //{
                //    Debug.Log(frontMostObject.name);

                //    Debug.Log(currLength);

                //    Debug.Log("Pos: " + frontMostObject.transform.position.z.ToString() + ", Size: " + frontMostObject.GetComponent<BoxCollider>().size.z.ToString() + ", Space: " + spacing.ToString());
                //}

                offset = 0;
            }
            else
            {
                currLength = 0;
            }

            if (currLength < mapLength && currLength < currentFrontier + renderDistance)
            {
                int objectIx = Mathf.RoundToInt(Random.value * (sourceObjects.Length - 1));

                Vector3 objectScale = sourceObjects[objectIx].GetComponent<BoxCollider>().size;
                
                
                //Debug.Log(objectScale);

                Vector3 objectPosition;

                if (shouldCenter)
                {
                    if (width < 0)
                    {
                        objectPosition = new Vector3(width - objectScale.x / 2, 0, currLength + objectScale.z / 2 + offset);
                    }
                    else
                    {
                        objectPosition = new Vector3(width + objectScale.x / 2, 0, currLength + objectScale.z / 2 + offset);
                    }
                }
                else
                {
                    objectPosition = new Vector3(width, 0, currLength + objectScale.z / 2 + offset);
                }

                GameObject instance = Instantiate(sourceObjects[objectIx], objectPosition, orientation);

                currentObjects.Add(instance);

                currLength += objectScale.z;
                currLength += spacing;

                while (currLength < mapLength && currLength < currentFrontier + renderDistance)
                {
                    objectIx = Mathf.RoundToInt(Random.value * (sourceObjects.Length - 1));

                    objectScale = sourceObjects[objectIx].GetComponent<BoxCollider>().size;

                    if (shouldCenter)
                    {
                        if (width < 0)
                        {
                            objectPosition.x = width - objectScale.x / 2;
                        }
                        else
                        {
                            objectPosition.x = width + objectScale.x / 2;
                        }
                    }
                    else
                    {
                        objectPosition.x = width;
                    }

                    objectPosition.y = 0;
                    objectPosition.z = currLength + objectScale.z / 2 + offset;

                    instance = Instantiate(sourceObjects[objectIx], objectPosition, orientation);

                    currentObjects.Add(instance);

                    currLength += objectScale.z;
                    currLength += spacing;
                }
            }
        }
    }

    private void DeSpawnObjects(List<GameObject> currentObjects, float currentFrontier)
    {
        if (currentObjects.Count > 0)
        {
            GameObject backMostObject = currentObjects[0];

            float currentPosition = backMostObject.transform.position.z + backMostObject.GetComponent<BoxCollider>().size.z / 2;

            while (currentObjects.Count > 0 && currentPosition + renderDistance < currentFrontier)
            {
                if (currentPosition + renderDistance < currentFrontier)
                {
                    Destroy(backMostObject);

                    currentObjects.RemoveAt(0);

                    if (currentObjects.Count > 0)
                    {
                        backMostObject = currentObjects[0];

                        currentPosition = backMostObject.transform.position.z + backMostObject.GetComponent<BoxCollider>().size.z / 2;
                    }
                }
            }
        }
    }
}
