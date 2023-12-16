using UnityEngine;

public class ElectricalPower : MonoBehaviour
{
    [HideInInspector] public bool isUpdating=false;
    public int capacity=20;

    public int capacityLeft;
    public float powerRadius=10f;
    void Start()
    {
        God.getUpdate=true;
        gameObject.tag="ElectricalPower";
    }

    void Update()
    {
        if(isUpdating){
            UpdateValues();
        }
    }
    private void UpdateValues(){
        capacityLeft=capacity;
        GameObject[] houses=GameObject.FindGameObjectsWithTag("Houses");
        foreach(GameObject house in houses){
            if(house.GetComponent<House>()!=null)
            house.GetComponent<House>().hasPower=false;
        }
        Collider[] housesInRange=Physics.OverlapSphere(God.GetCentre(gameObject), powerRadius);

        foreach(Collider house in housesInRange){
            if(house.gameObject.tag != "Houses")
                continue;
            if(house.GetComponent<House>()==null)
            continue;

            House housee=house.gameObject.GetComponent<House>();
            housee.powerValue=0;
            if(housee.membersWithoutPower<=capacity){
                capacityLeft-=housee.membersWithoutPower;
                housee.powerValue+=housee.membersWithoutPower*God.powerWeightage;
                housee.membersWithoutPower=0;
                housee.hasPower=true;
            }
            else{
                housee.membersWithoutPower-=capacityLeft;
                housee.powerValue+=capacityLeft*God.powerWeightage;
                capacityLeft=0;
                Debug.Log("No Power at: "+housee.name + " with " + housee.membersWithoutPower);
                housee.hasPower=true;
            }
        }
        isUpdating=false;
    }
}
