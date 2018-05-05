using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject itemPrefab;

    void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc)
        {
            GameObject itemObject = (GameObject)Instantiate(itemPrefab);
            Item i = itemObject ? itemObject.GetComponent<Item>() : null;
            if(i && pc.PickUpItem(i))
            {
                Destroy(gameObject);
            }
        }
    }
}
