using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public void ResetStatus(ref WorldObject worldObject)
    {
        worldObject.myName = "";
        worldObject.dead = false;
        worldObject.transform.position = Vector3.zero;
        worldObject.transform.localPosition = Vector3.zero;
    }

    public void ResetStatus(ref WorldObject worldObject, ref Transform parent)
    {
        ResetStatus(ref worldObject);
        worldObject.transform.parent = parent;
    }
}
