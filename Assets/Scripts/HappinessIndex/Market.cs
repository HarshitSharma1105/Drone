using UnityEngine;

public class Market : MonoBehaviour
{

    public int marketLevel=1;
    // Start is called before the first frame update
    void Start()
    {
        God.getUpdate=true;
        gameObject.tag="Market";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
