using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour
{
    public float totalMoney;

    public float currentMoney;

    
    public void deductCost(int cost)
    {
        currentMoney -= cost;
    }
}
