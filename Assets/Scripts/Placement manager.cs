using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placementmanager : MonoBehaviour
{
    public GameObject Roads,Buildings,Others,HouseDetails;
    public void showRoads(){
    Roads.SetActive(!Roads.activeSelf);
    Buildings.SetActive(false);
    Others.SetActive(false);
    HouseDetails.SetActive(false);
    }
    public void showBuildings(){
    Buildings.SetActive(!Buildings.activeSelf);
    Roads.SetActive(false);
    Others.SetActive(false);
    HouseDetails.SetActive(false);
    }
    public void showOthers(){   
    Others.SetActive(!Others.activeSelf);
    Buildings.SetActive(false);
    Roads.SetActive(false);
    HouseDetails.SetActive(false);
    }   
}
