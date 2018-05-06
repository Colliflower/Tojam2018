using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject itemPrefab;

    private List<GameObject> itemBoxes;

    public void AssignItemBoxes(List<GameObject> itemBoxes)
    {
        this.itemBoxes = itemBoxes;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc)
        {
            GameObject itemObject = Instantiate(itemPrefab);
            Item i = itemObject ? itemObject.GetComponent<Item>() : null;
            if(i && pc.PickUpItem(i))
            {
                itemBoxes.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
