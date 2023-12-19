using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building_Selector: MonoBehaviour
{
    public GameObject building;

    public void OnClick()
    {
        Camera.main.GetComponent<OutlineSelection>().enabled = false;
        InstantiateAtMouse.instance.prefabToInstantiate = Resources.Load<GameObject>("Prefabs/Instantiating Objects/" + building.name);
        Instantiate(building, InstantiateAtMouse.instance.Hover.transform);
        Debug.Log("done");
        InstantiateAtMouse.instance.isSelected = true;
        InstantiateAtMouse.instance.panel.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
}
