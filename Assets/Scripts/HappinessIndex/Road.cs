using UnityEngine;

public class Road : MonoBehaviour
{
    public int roadLevel; //0: Kacha, 1: Brick, 2: PaverBlock, 3: Concrete/Cement
    void Start(){
        God.getUpdate=true;
        gameObject.tag="Road";
    }
    void Update(){
        //UpdateValues();   
    }
}
