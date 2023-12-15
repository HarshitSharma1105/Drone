using UnityEngine;

public class School : MonoBehaviour
{
    public int schoolCapacity=20;

    private float time=0;
    void Start()
    {
        gameObject.tag="School";
    }

    void Update()
    {
        /*time += Time.deltaTime;
        if(time > 5){
            RealTimeUpdate();
            time = 0;
        }
        */
    }
}
