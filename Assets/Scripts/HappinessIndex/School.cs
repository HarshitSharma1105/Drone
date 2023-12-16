using UnityEngine;

public class School : MonoBehaviour
{
    public int schoolCapacity=20;

    void Start()
    {
        God.getUpdate=true;
        gameObject.tag="School";
    }

    void Update()
    {
        //UpdateValues();
    }
}
