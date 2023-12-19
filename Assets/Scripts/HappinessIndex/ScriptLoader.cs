using UnityEngine;

public class ScriptLoader : MonoBehaviour
{
    Transform[] allChildren;
    void Start()
    {
        allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.name.StartsWith("CityEngine"))
           {
            child.gameObject.AddComponent<MeshCollider>().providesContacts = true;
            child.gameObject.AddComponent<Road>();
            child.gameObject.tag="Road";
           }
        }

    }
}
