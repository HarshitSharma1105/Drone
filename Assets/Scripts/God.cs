using System.Collections;
using UnityEngine;

public class God : MonoBehaviour
{
    public static bool getUpdate;
    public static float cumulativeHappinessIndex=0;
    public static float houseWeightage=0.3f;
    public static float roadWeightage=0.1f;
    public static float neighbourhoodWeightage=0.1f;
    public static float schoolWeightage=0.1f;
    public static float hospitalWeightage=0.1f;
    public static float hospitalPenalty=0.1f;



    private int hospitalCapacity;
    private int hospitalPatients;

    private int schoolCapacity;
    private int schoolStudents;
    private float time=0;
    void Start()
    {
        getUpdate=true;
        gameObject.tag="God";
        Debug.Log("God is here");
    }

    
    void Update()
    {
        if(God.getUpdate){
            UpdateValues();
        }
        time += Time.deltaTime;
        if(time > 5){
            StartCoroutine(GetUpdates());
            time = 0;
        }
    }

    IEnumerator GetUpdates(){
        getUpdate=true;
        UpdateValues();
        Debug.Log("Running Updates");
        yield return new WaitForSeconds(1f);
        getUpdate=false;
        yield return new WaitForSeconds(1f);
        Debug.Log("Cumulative Happiness Index: "+cumulativeHappinessIndex);
    }

    private void UpdateValues(){

        cumulativeHappinessIndex=0;
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");



        //HospitalCapacityCheck
        hospitalCapacity=0;
        GameObject[] hospitals = GameObject.FindGameObjectsWithTag("Hospital");
        foreach(GameObject hospital in hospitals){
            hospitalCapacity += hospital.GetComponent<Hospital>().capacity;
        }
        hospitalPatients=0;
        
        foreach(GameObject house in houses){
            hospitalPatients += house.GetComponent<House>().houseMembers;
        }
        if(hospitalPatients > hospitalCapacity){
            Debug.Log("Hospital Capacity Exceeded");
            float hospitalPenalty = (hospitalPatients - hospitalCapacity) * 0.1f;
            cumulativeHappinessIndex-=hospitalPenalty;
        }



        //SchoolCapacityCheck
        schoolCapacity=0;
        GameObject[] schools = GameObject.FindGameObjectsWithTag("School");
        foreach(GameObject school in schools){
            schoolCapacity += school.GetComponent<School>().schoolCapacity;
        }
        schoolStudents=0;
        foreach(GameObject house in houses){
            schoolStudents += house.GetComponent<House>().houseMembers;
        }
        if(schoolStudents > schoolCapacity){
            Debug.Log("School Capacity Exceeded");
            float schoolPenalty = (schoolStudents - schoolCapacity) * 0.1f;
            cumulativeHappinessIndex-=schoolPenalty;
        }

        //Debug.Log("Cumulative Happiness Index: "+cumulativeHappinessIndex);
    }
}
