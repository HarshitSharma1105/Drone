using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnandOff : MonoBehaviour
{
    int a=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show(){
        gameObject.SetActive(a%2==0);
        a++;
    }
}
