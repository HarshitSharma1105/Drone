using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building_Selector: MonoBehaviour
{
    public string building;
    public void OnClick()
    {
        InstantiateAtMouse.instance.prefabToInstantiate = Resources.Load<GameObject>("Prefabs/" + building);
        InstantiateAtMouse.instance.panel.SetActive(false);
    }
}
