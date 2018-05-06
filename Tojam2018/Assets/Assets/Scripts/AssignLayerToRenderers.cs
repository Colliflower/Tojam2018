using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignLayerToRenderers : MonoBehaviour
{
    public void AssignLayer(int layer)
    {
        AssignLayerToTransform(transform, layer);
    }

    private void AssignLayerToTransform(Transform t, int layer)
    {
        if(t.GetComponent<Renderer>())
        {
            t.gameObject.layer = layer;
        }

        for (int i = 0; i < t.childCount; i++)
        {
            AssignLayerToTransform(t.GetChild(i), layer);
        }
    }
}
