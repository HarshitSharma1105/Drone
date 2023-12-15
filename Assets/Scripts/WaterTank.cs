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
        GameObject[] houses=GameObject.FindGameObjectsWithTag("House");
        foreach(GameObject house in houses){
            house.GetComponent<House>().hasWater=false;
        }
        Collider[] housesInRange=Physics.OverlapSphere(transform.position, waterRadius);

        foreach(Collider house in housesInRange){
            if(house.gameObject.tag != "House")
                continue;
        
            House housee=house.gameObject.GetComponent<House>();

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
                Debug.Log("Scarcity at: "+housee.name + " with " + housee.membersWithoutWater);
                housee.hasWater=false;
            }
            /*while(housee.membersWithoutWater > 0 && capacityLeft > 0){
                housee.membersWithoutWater--;
                capacityLeft--;
                housee.waterValue += God.waterWeightage;
                housee.hasWater=true;
            }*/

        }
        isUpdating=false;
}
}
