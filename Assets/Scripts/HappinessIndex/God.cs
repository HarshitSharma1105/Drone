using System.Collections;
using UnityEngine;

public class God : MonoBehaviour
{
    public static bool getUpdate=false;
    public static float cumulativeHappinessIndex=0;
    public static float houseWeightage=0.3f;
    public static float roadWeightage=0.1f;
    public static float neighbourhoodWeightage=0.1f;
    


    public static float hospitalWeightage=0.1f;
    public static float hospitalPenalty=0.1f;
    private int hospitalCapacity;
    private int hospitalPatients;


    public static float schoolWeightage=0.1f;
    private int schoolCapacity;
    private int schoolStudents;
    public static float schoolPenalty=0.1f;

    public static float marketWeightage=0.1f;


    public static float waterWeightage=0.1f;

    public static float powerWeightage=0.1f;
    private float time=0;
    void Start()
    {
        StartCoroutine(GetUpdates());
        getUpdate=false;
        //gameObject.tag="God";
        Debug.Log("God is here");
    }

    
    void Update()
    {
        if(getUpdate)
        {
            StartCoroutine(GetUpdates());
            getUpdate = false;
        }
        time += Time.deltaTime;
        if(time > 5){
            StartCoroutine(GetUpdates());
            time = 0;
        }
    }

    IEnumerator GetUpdates(){
        UpdateValues();
        Debug.Log("Running Updates");
        GameObject[] houses = GameObject.FindGameObjectsWithTag("Houses");
        foreach(GameObject house in houses){
            if(house.GetComponent<House>()!=null)
            house.GetComponent<House>().isUpdating=true;
            //Debug.Log("Updating House: "+house.name);
        }


        yield return new WaitForSeconds(0.3f);
        GameObject[] waterTanks = GameObject.FindGameObjectsWithTag("WaterTank");
        foreach(GameObject waterTank in waterTanks){
            waterTank.GetComponent<WaterTank>().isUpdating=true;
        }

        GameObject[] electricalPowers = GameObject.FindGameObjectsWithTag("ElectricalPower");
        foreach(GameObject electricalPower in electricalPowers){
            electricalPower.GetComponent<ElectricalPower>().isUpdating=true;
        }
        yield return new WaitForSeconds(0.3f);
        getUpdate=false;
        yield return new WaitForSeconds(0.3f);
        cumulativeHappinessIndex=cumulativeHappinessIndex/houses.Length;
        Debug.Log("Cumulative Happiness Index: "+cumulativeHappinessIndex);
        yield return new WaitForSeconds(0.3f);
    }

    private void UpdateValues(){

        cumulativeHappinessIndex=0;
        GameObject[] houses = GameObject.FindGameObjectsWithTag("Houses");
        hospitalPatients=0;
        schoolStudents=0;
        foreach(GameObject house in houses){
            if(house.GetComponent<House>()!=null){
            hospitalPatients += house.GetComponent<House>().houseMembers;
            schoolStudents += house.GetComponent<House>().houseMembers;
            }
        }

        //HospitalCapacityCheck
        hospitalCapacity=0;
        GameObject[] hospitals = GameObject.FindGameObjectsWithTag("Hospital");
        foreach(GameObject hospital in hospitals){
            hospitalCapacity += hospital.GetComponent<Hospital>().capacity;
        }
        
        if(hospitalPatients > hospitalCapacity){
            Debug.Log("Hospital Capacity Exceeded");
            cumulativeHappinessIndex-=(hospitalPatients - hospitalCapacity) * hospitalPenalty;
        }



        //SchoolCapacityCheck
        schoolCapacity=0;
        GameObject[] schools = GameObject.FindGameObjectsWithTag("School");
        foreach(GameObject school in schools){
            schoolCapacity += school.GetComponent<School>().schoolCapacity;
        }
        if(schoolStudents > schoolCapacity){
            Debug.Log("School Capacity Exceeded");
            cumulativeHappinessIndex -= (schoolStudents - schoolCapacity) * schoolPenalty;
        }


        //MarketCheck
        GameObject[] markets = GameObject.FindGameObjectsWithTag("Market");
        if(markets.Length == 0){
            Debug.Log("No Market");
        }
    }

    public static Vector3 GetCentre(GameObject obj){
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Bounds bound=renderers[0].bounds;
        foreach(Renderer renderer in renderers){
            bound.Encapsulate(renderer.bounds);
        }
        //bound.Encapsulate(obj.GetComponent<Renderer>().bounds);
        return bound.center;
    }
}
