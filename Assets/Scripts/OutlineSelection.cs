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
    Outline outline;
    private void Start()
    {
        outline = gameObject.AddComponent<Outline>();
        gameObject.GetComponent<Outline>().OutlineColor = Color.red;
        gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
        outline.enabled = false;
        gg = transform.Find("FloatStuff");
    }

    void Update()
    {
        /* Debug.Log("high");
        Debug.Log(highlight);
        Debug.Log("sele");  
        Debug.Log(selection); */
        // Highlight

        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            if (raycastHit.transform == gameObject.transform)
            {
                outline.enabled = true;
            }
            else if (selected == true) { outline.enabled = true; }
            else { outline.enabled = false; }
        }
        // Selection

        if (Input.GetMouseButtonDown(0))
        {
            /*
             if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = raycastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                }
            }
            */
            if (Physics.Raycast(ray, out raycastHit))
            {
                Debug.Log("clickkarra");
                testt = raycastHit.transform;
                if ((testt != gameObject.transform))
                {
                    selected = false;
                }
                else if ((testt == gameObject.transform))
                {
                    selected = true;
                }
            }
        }
        
        //laga.gameObject.SetActive(false);
        //if(highlight != null) laga.gameObject.SetActive(true);
        //if(selection != null) laga.gameObject.SetActive(true);
        
        if(selected) gg.gameObject.SetActive(true);
        else gg.gameObject.SetActive(false);

    }

}
