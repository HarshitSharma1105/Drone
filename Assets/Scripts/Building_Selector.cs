using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building_Selector: MonoBehaviour
{
    public GameObject building;
    public void OnClick()
    {
        InstantiateAtMouse.instance.prefabToInstantiate = Resources.Load<GameObject>("Prefabs/Instantiating Objects/" + building.name);
        Debug.Log("done");
        InstantiateAtMouse.instance.isSelected = true;
        InstantiateAtMouse.instance.panel.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
}
