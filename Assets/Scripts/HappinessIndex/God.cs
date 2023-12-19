using System.Collections;
using UnityEngine;

public class God : MonoBehaviour
{
    public static long budget=100000000;
    public static bool getUpdate=false;
    public static float cumulativeHappinessIndex=0;
    public static float houseWeightage=0.3f;
    public static float roadWeightage=0.1f;
    public static float neighbourhoodWeightage=0.1f;
    Bounds bounds;
    


    public static float hospitalWeightage=0.1f;
    // public static float hospitalPenalty=0f;
    // private int hospitalCapacity;
    // private int hospitalPatients;


    public static float schoolWeightage=0.1f;
    // private int schoolCapacity;
    // private int schoolStudents;
    // public static float schoolPenalty=0f;

    public static float marketWeightage=0.1f;


    public static float waterWeightage=0.1f;

    public static float powerWeightage=0.1f;
    private float time=0;

    public static GameObject[] houses;
    void Start()
    {
        StartCoroutine(GetUpdates());
        getUpdate=false;
        //gameObject.tag="God";
        //Debug.Log("God is here");
    }

    
    void Update()
    {
        if(getUpdate)
        {
            StartCoroutine(GetUpdates());
            getUpdate = false;
        }
        time += Time.deltaTime;
        if(time > 60){
            StartCoroutine(GetUpdates());
            time = 0;
        }
    }

    IEnumerator GetUpdates(){
        
      //  Debug.Log("Running Updates");
        houses = GameObject.FindGameObjectsWithTag("Houses");
        UpdateValues();
        foreach(GameObject house in houses){
            if(house.GetComponent<House>()!=null)
            house.GetComponent<House>().isUpdating=true;
        }

        yield return new WaitForSeconds(0.3f);
        GameObject[] waterTanks = GameObject.FindGameObjectsWithTag("WaterTank");
        foreach(GameObject waterTank in waterTanks){
            waterTank.GetComponent<WaterTank>().isUpdating=true;
        }
        yield return new WaitForSeconds(0.1f);
        GameObject[] electricalPowers = GameObject.FindGameObjectsWithTag("ElectricalPower");
        foreach(GameObject electricalPower in electricalPowers){
            electricalPower.GetComponent<ElectricalPower>().isUpdating=true;
        }
        yield return new WaitForSeconds(0.3f);
        getUpdate=false;
        yield return new WaitForSeconds(0.3f);
        cumulativeHappinessIndex=cumulativeHappinessIndex/houses.Length;
        Debug.Log("Cumulative Happiness Index: "+cumulativeHappinessIndex*100);
        yield return new WaitForSeconds(0.3f);
    }

    private void UpdateValues(){

        cumulativeHappinessIndex=0;
        // hospitalPatients=0;
        // schoolStudents=0;
        // foreach(GameObject house in houses){
        //     if(house.GetComponent<House>()!=null){
        //     hospitalPatients += house.GetComponent<House>().houseMembers;
        //     schoolStudents += house.GetComponent<House>().houseMembers;
        //     }
        // }

        //HospitalCapacityCheck
        GameObject[] hospitals = GameObject.FindGameObjectsWithTag("Hospital");
        if(hospitals.Length==0){
            Debug.Log("No hospital");
        }
        // foreach(GameObject hospital in hospitals){
        //     hospitalCapacity += hospital.GetComponent<Hospital>().capacity;
        // }
        
        // if(hospitalPatients > hospitalCapacity){
        //    // Debug.Log("Hospital Capacity Exceeded");
        //     cumulativeHappinessIndex-=(hospitalPatients - hospitalCapacity) * hospitalPenalty;
        // }



        //SchoolCapacityCheck
        GameObject[] schools = GameObject.FindGameObjectsWithTag("School");
        if(schools.Length==0){
            Debug.Log("No school");
        }
        // foreach(GameObject school in schools){
        //     schoolCapacity += school.GetComponent<School>().schoolCapacity;
        // }
        // if(schoolStudents > schoolCapacity){
        //     //Debug.Log("School Capacity Exceeded");
        //     cumulativeHappinessIndex -= (schoolStudents - schoolCapacity) * schoolPenalty;
        // }


        //MarketCheck
        GameObject[] markets = GameObject.FindGameObjectsWithTag("Market");
        if(markets.Length == 0){
           Debug.Log("No Market");
        }
    }

    public static Vector3 GetCentre(GameObject obj){
        if(obj.GetComponent<Collider>()!=null){
            return obj.GetComponent<Collider>().bounds.center;
        }
        else
        {
        Transform[] colliders = obj.GetComponentsInChildren<Transform>();
        Bounds bound = colliders[0].gameObject.GetComponent<Collider>().bounds;

        foreach(Transform collider in colliders){
            bound.Encapsulate(collider.gameObject.GetComponent<Collider>().bounds);
        }
        return bound.center;
        }
    }
}
