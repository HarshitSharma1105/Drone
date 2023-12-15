using UnityEngine;

public class Hospital : MonoBehaviour
{

    public int capacity=20;

    void Start()
    {
        God.getUpdate=true;
        gameObject.tag="Hospital";
    }

    void Update()
    {
        if(God.getUpdate){
            //UpdateValues();
        }
        
    }
}
