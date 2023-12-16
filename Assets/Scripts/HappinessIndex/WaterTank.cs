using UnityEngine;

public class WaterTank : MonoBehaviour
{
    [HideInInspector] public bool isUpdating=false;
    public int capacity=20;

    public int capacityLeft;
    public float waterRadius=10f;

    void Start(){
        God.getUpdate=true;
        gameObject.tag="WaterTank";
    }

    void Update(){
        if(isUpdating){
            UpdateValues();
        }
    }

    private void UpdateValues(){
        capacityLeft=capacity;
        GameObject[] houses=GameObject.FindGameObjectsWithTag("Houses");
        foreach(GameObject house in houses){
            house.GetComponent<House>().hasWater=false;
        }
        Collider[] housesInRange=Physics.OverlapSphere(transform.position, waterRadius);

        foreach(Collider house in housesInRange){
            if(house.gameObject.tag != "Houses")
                continue;
            
            House housee=house.gameObject.GetComponent<House>();
            housee.waterValue=0;
            if(housee.membersWithoutWater<=capacity){
                capacityLeft-=housee.membersWithoutWater;
                housee.waterValue+=housee.membersWithoutWater*God.waterWeightage;
                housee.membersWithoutWater=0;
                housee.hasWater=true;
            }
            else{
                housee.membersWithoutWater-=capacityLeft;
                housee.waterValue+=capacityLeft*God.waterWeightage;
                capacityLeft=0;
                Debug.Log("Water Scarcity at: "+housee.name + " with " + housee.membersWithoutWater);
                housee.hasWater=true;
            }
        }
        isUpdating=false;
}
}
