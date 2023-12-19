using Unity.VisualScripting;
using UnityEngine;

public class ScriptLoader2 : MonoBehaviour
{
    Transform[] allChildren;
    void Start(){
        allChildren = GetComponentsInChildren<Transform>();
        Transform child1=allChildren[0];
        child1.gameObject.tag="Houses";
        child1.AddComponent<MeshCollider>().providesContacts = true;
        child1.AddComponent<House>();
    }
}
