using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnDestroy()
    {
        Tracker.instance.DeleteTracked(gameObject);
    }
}
