using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform testt;
    private RaycastHit raycastHit;
    private Transform gg;
    private bool selected;
    private float timer = 0.0f;
    Outline outline;
    private GameObject prev = null;
    private GameObject selec = null;
    bool donotdestroy = false;
    GameObject curr;
    private void Start()
    {

    }

    void Update()
    {
        // Debug.Log(timer);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
        
             
            if(!donotdestroy)
            {   curr = raycastHit.transform.gameObject;
            if(curr != null ) 
            {
                if(curr != prev )
                {
                    if (prev != null && prev.GetComponent<Outline>() != null )
                    {
                        
                        prev.GetComponent<Outline>().enabled = false;
                        Destroy(prev.GetComponent<Outline>());
                    }

                    if(curr.GetComponent<Outline>() == null)
                    {
                       
                        curr.AddComponent<Outline>();
                        curr.GetComponent<Outline>().OutlineColor = Color.blue;
                        curr.GetComponent<Outline>().OutlineWidth = 10f;
                        curr.GetComponent<Outline>().enabled = true;
                        prev = curr;
                    }
                }
            }
            else
            {
                if (prev != null && prev.GetComponent<Outline>() != null)
                {
                    
                    prev.GetComponent<Outline>().enabled = false;
                    Destroy(prev.GetComponent<Outline>());
                    prev = null;
                }
            }
         
        }
        
        // Selection

        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(ray, out raycastHit))
            {
                Debug.Log("clickkarra");
                selec = raycastHit.transform.gameObject;
                if(selec != null)
                {   
                    Debug.Log(selec);
                    donotdestroy = true;
                    if(selec.gameObject.GetComponent<Outline>() != null)
                    {
                        selec.gameObject.GetComponent<Outline>().enabled = true;
                        selec.gameObject.GetComponent<Outline>().OutlineColor = Color.green;
                    }
                }
                if(curr != selec)
                 {
                    curr.gameObject.GetComponent<Outline>().enabled = false;
                    donotdestroy = false;

                 }
                
            }
        }
        }
        
        //laga.gameObject.SetActive(false);
        //if(highlight != null) laga.gameObject.SetActive(true);
        //if(selection != null) laga.gameObject.SetActive(true);
        
        // if(selected) gg.gameObject.SetActive(true);
        // else gg.gameObject.SetActive(false);

    }

}
