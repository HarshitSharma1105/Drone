using UnityEngine;

public class Hospital : MonoBehaviour
{

    public int capacity=20;

    private float time=0;
    void Start()
    {

        gameObject.tag="Hospital";
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > 5){
            //RealTimeUpdate();
            time = 0;
        }
        
    }
}
