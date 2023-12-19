using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BudgetSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    long budget;

    void Start(){
        slider = GetComponent<Slider>();
        slider.value=0;
    }
    void Update(){
        budget=500000+(long)((Mathf.Floor(slider.value)*500000));
        text.text =(budget).ToString("G8");
        text.text="Rs. "+text.text;
        God.budget=budget;
        //Debug.Log("Budget: "+God.budget);
    }
}
