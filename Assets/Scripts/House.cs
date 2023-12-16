using UnityEngine;

public class House : MonoBehaviour
{

    [HideInInspector] public bool isUpdating=false;
    public float happinessIndex;
    
    public int houseMembers;
    public float houseRadius=2f;
    public Vector2 houseValue;
    private float roadValue;
    private Vector2 neighbourhoodValue;

    private float hospitalValue;
    public float hospitalDistanceScaling=0.1f; //scaling factor for hospital distance has to be changed to a more realistic one

    private float schoolValue;
    public float schoolDistanceScaling=0.1f; //scaling factor for school distance has to be changed to a more realistic one

    public float waterValue;
    [HideInInspector] public int membersWithoutWater;
    public bool hasWater=false;

    public float powerValue;
    [HideInInspector] public int membersWithoutPower;
    public bool hasPower=false;


    
    void Start(){
        isUpdating=true;
        gameObject.tag="House";
    }
    void Update()
    {
        if(isUpdating){
            UpdateValues();
        }
    }
    private void UpdateValues()
    {
        PreUpdateReset();        
           //Own House
        houseValue.y = houseValue.x * God.houseWeightage;
        happinessIndex += houseValue.y;
        //Debug.Log(happinessIndex);



        //neighbourhood
        Collider[] neighbourhood = Physics.OverlapSphere(transform.position, 2.5f*houseRadius);
        int neighbourhoodCount = 0;
        foreach (Collider c in neighbourhood){
            if(c.gameObject.tag == "House"){
                neighbourhoodValue.x += c.gameObject.GetComponent<House>().houseValue.x;
                neighbourhoodCount++;
            }
        }
        if(neighbourhoodCount!=0)
            neighbourhoodValue.y = neighbourhoodValue.x / neighbourhoodCount * God.neighbourhoodWeightage;
        happinessIndex += neighbourhoodValue.y;
        //Debug.Log("neighbourhoodValue" + neighbourhoodValue.y);




        //Road
        Collider[] road = Physics.OverlapSphere(transform.position, houseRadius/(2*Mathf.Sqrt(2))+0.5f);
        foreach (Collider c in road){
            if(c.gameObject.tag == "Road"){
                roadValue += c.gameObject.GetComponent<Road>().roadLevel * God.roadWeightage;
            }
        }
        happinessIndex += roadValue;

        //Hospital
        GameObject[] hospitals = GameObject.FindGameObjectsWithTag("Hospital");
        foreach (GameObject hospital in hospitals){
            float distance = Vector3.Distance(transform.position, hospital.transform.position);
            hospitalValue += hospitalDistanceScaling / distance * God.hospitalWeightage;        //the scaling function has to be changed to a more realistic one
        }
        //Debug.Log("hospitalValue" + hospitalValue);
        happinessIndex += hospitalValue;

        //School
        GameObject[] schools = GameObject.FindGameObjectsWithTag("School");
        foreach (GameObject school in schools){
            float distance = Vector3.Distance(transform.position, school.transform.position);
            schoolValue += schoolDistanceScaling / distance * God.schoolWeightage;        //the scaling function has to be changed to a more realistic one
        }
        //Debug.Log("schoolValue" + schoolValue);
        happinessIndex += schoolValue;
        //Debug.Log("happinessIndex: " + happinessIndex);

        //water
        if(!hasWater){
            Debug.Log("Water Scarcity at: "+gameObject.name + " with " + membersWithoutWater);
        }

        //power
        if(!hasPower){
            Debug.Log("No Power at: "+gameObject.name + " with " + membersWithoutPower);
        }

        God.cumulativeHappinessIndex += happinessIndex;

        

        isUpdating=false;
    }


    private void PreUpdateReset(){
        houseValue.y = 0;
        neighbourhoodValue.x = 0;
        neighbourhoodValue.y = 0;
        roadValue = 0;
        hospitalValue = 0;
        schoolValue = 0;
        happinessIndex = 0;
        waterValue = 0;
        membersWithoutWater = houseMembers;
        powerValue = 0;
        membersWithoutPower = houseMembers;
    }
}
