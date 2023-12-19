using System;
using UnityEngine;

public class House : MonoBehaviour
{
    public long budget;
    [HideInInspector] public bool isUpdating=false;
    public float happinessIndex;
    
    public int houseMembers=4;
    public float houseRadius=5f;
    public Vector2 houseValue=new Vector2(1,0);
    public float roadValue = 0;
    [SerializeField] private Vector2 neighbourhoodValue;

    [SerializeField] private float hospitalValue;
    public float hospitalDistanceScaling=0.1f; //scaling factor for hospital distance has to be changed to a more realistic one

    [SerializeField] private float schoolValue;
    public float schoolDistanceScaling=0.1f; //scaling factor for school distance has to be changed to a more realistic one

    public float waterValue;
    public bool hasWater=false;

    public float powerValue;
    public bool hasPower=false;

    [SerializeField] private float marketValue;
    public float marketDistanceScaling=0.1f; //scaling factor for market distance has to be changed to a more realistic one


    
    void Start(){
        God.getUpdate=true;
        isUpdating=true;
        gameObject.tag="Houses";
    }
    void Update()
    {   
    //Debug.Log(isUpdating);
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
        Collider[] neighbourhood = Physics.OverlapSphere(God.GetCentre(gameObject), 2.5f*houseRadius);
        int neighbourhoodCount = 0;
        foreach (Collider c in neighbourhood){
            if(c.gameObject.tag == "Houses"){
                if(c.gameObject.GetComponent<House>()!=null){
                neighbourhoodValue.x += c.gameObject.GetComponent<House>().houseValue.x;
                neighbourhoodCount++;
                }
            }
        }
        if(neighbourhoodCount!=0)
            neighbourhoodValue.y = neighbourhoodValue.x / neighbourhoodCount * God.neighbourhoodWeightage;
        happinessIndex += neighbourhoodValue.y;
        //Debug.Log("neighbourhoodValue" + neighbourhoodValue.y);




        //Road
        Collider[] road = Physics.OverlapSphere(God.GetCentre(gameObject), houseRadius);
        int roadCount = 0;
        foreach (Collider c in road){
            if(c.gameObject.tag == "Road"){
               // Debug.Log(c);
               roadCount++;         
            }
            roadValue += roadCount * God.roadWeightage;
           // Debug.Log("road value " + roadValue);
        }
        if(roadCount!=0)
       // Debug.Log("D " + roadCount);
        happinessIndex += roadValue;

        //Hospital
        GameObject[] hospitals = GameObject.FindGameObjectsWithTag("Hospital");
        foreach (GameObject hospital in hospitals){
            float distance = Vector3.Distance(God.GetCentre(gameObject), God.GetCentre(hospital));
            hospitalValue += hospitalDistanceScaling /(1+ distance) * God.hospitalWeightage;        //the scaling function has to be changed to a more realistic one
        }
        //Debug.Log("hospitalValue" + hospitalValue);
        happinessIndex += hospitalValue;

        //School
        GameObject[] schools = GameObject.FindGameObjectsWithTag("School");
        foreach (GameObject school in schools){
            float distance = Vector3.Distance(God.GetCentre(gameObject), God.GetCentre(school));
            schoolValue += schoolDistanceScaling /(1+ distance) * God.schoolWeightage;        //the scaling function has to be changed to a more realistic one
        }
        //Debug.Log("schoolValue" + schoolValue);
        happinessIndex += schoolValue;
        //Debug.Log("happinessIndex: " + happinessIndex);
        

        //water
        if(hasWater){
            waterValue=God.waterWeightage;
        }
        else{
            Debug.Log("No Water at: "+gameObject.GetComponentInParent<House>().name);
        }
        happinessIndex += waterValue;

        //power
        if(hasPower){
            powerValue=God.powerWeightage;
        }
        else{
            Debug.Log("No Power at: "+gameObject.GetComponentInParent<House>().name);
        }
        happinessIndex += powerValue;

        //market
        GameObject[] markets = GameObject.FindGameObjectsWithTag("Market");
        foreach (GameObject market in markets){
            float distance = Vector3.Distance(God.GetCentre(gameObject), God.GetCentre(market));
            if(distance < 20f)
            marketValue += marketDistanceScaling * Calc(distance) * God.marketWeightage;        //the scaling function has to be changed to a more realistic one
        }
        happinessIndex += marketValue;

        God.cumulativeHappinessIndex += happinessIndex;

        isUpdating=false;
    }

    private float Calc(float x){
        return -Mathf.Exp(-0.6f*(x-4)) + 2*Mathf.Exp(-0.4f*(x-4));
    }


    private void PreUpdateReset(){
        houseValue.y = 0;
        neighbourhoodValue.x = 0;
        neighbourhoodValue.y = 0;
        roadValue = 0;
        hospitalValue = 0;
        schoolValue = 0;
        happinessIndex = 0;
        marketValue = 0;
    }

    public void getBudget(){

    }
}
