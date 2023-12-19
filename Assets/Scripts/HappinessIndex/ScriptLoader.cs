using UnityEngine;

public class ScriptLoader : MonoBehaviour
{
    // Start is called before the first frame update
   // Stack <GameObject> checker = new Stack<GameObject>();
    Transform[] allChildren;
    void Start()
    {
        
        allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.name.StartsWith("CityEngine"))
           {
           // Debug.Log(child.gameObject.name);
            child.gameObject.AddComponent<MeshCollider>().providesContacts = true;
            child.gameObject.AddComponent<Road>();
            child.gameObject.tag="Road";

           }
        }

    }

    // Update is called once per frame

}
