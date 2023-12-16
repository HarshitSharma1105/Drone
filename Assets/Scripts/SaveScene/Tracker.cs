using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public static Tracker instance;
    private List<GameObject> instantiatedObjects = new List<GameObject>();

    private void Awake()
    {
        if(instance == null)
        instance = this;
    }

    public void TrackInstantiatedObject(GameObject instantiatedObject)
    {
        instantiatedObjects.Add(instantiatedObject);
    }

    public void DeleteTracked(GameObject deletedObject)
    {
        instantiatedObjects.Remove(deletedObject);
    }

    public List<GameObject> GetTrackedObjects()
    {
        return instantiatedObjects;
    }
}
