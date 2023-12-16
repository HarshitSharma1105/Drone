using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingUIStuff : MonoBehaviour
{
    private Camera _cam;
    private float dist, initial_dist;
    void Start()
    {
        //initial_dist = Vector3.Distance(gameObject.transform.position, _cam.transform.position);
        _cam = Camera.main;
    }

    void Update()
    {
        //gameObject.transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        gameObject.transform.rotation = _cam.transform.rotation;
        //dist = Vector3.Distance(transform.position, _cam.transform.position);
        //transform.localScale = Vector3.one * dist / initial_dist;
    }
}
