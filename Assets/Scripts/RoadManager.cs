using System.Diagnostics.Tracing;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;

    public GameObject arrow;

    GameObject arrowstart;
    GameObject arrowend;

    public float UnitValueForRoad = 100f;

    public float pricePerUnit = 10f;

    private float costOfRoad;

    private float n = 1f;
    
    public GameObject Create;
    
    public GameObject Cancel;

    private float distance;

    public CostManager costManager;

    GameObject road;

    public int isStart = 1;

    Vector3 startPoint = Vector3.zero;
    Vector3 endPoint = Vector3.zero;
    public GameObject none;

    void Awake()
    {
        isStart = 1;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && InstantiateAtMouse.instance.prefabToInstantiate == roadPrefab)
        {
          //  Debug.Log(isStart);
            if(isStart == 1)
            {
                startPoint = GetMouseClickPosition();
              //  Debug.Log(startPoint);
                isStart = 2;
            }
            else if(isStart == 2)
            {
                endPoint = GetMouseClickPosition();
             //   Debug.Log(endPoint);

                if (IsPathClear(startPoint, endPoint))
                {   
                    arrowstart = Instantiate(arrow, startPoint, Quaternion.identity);
                    arrowend = Instantiate(arrow, endPoint, Quaternion.Euler(0, 180, 0));
                    CreateRoad(startPoint, endPoint);
                  //  Debug.Log(isStart);
                    isStart = 3;
                }
                else
                {
                    ShowErrorMessage();
                }
            }    
        }
    }

    Vector3 GetMouseClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.CompareTag("Terrain"))
                return hit.point;
        }

        return Vector3.zero;
    }

    bool IsPathClear(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = endPoint - startPoint;
        float distance = Vector3.Distance(startPoint, endPoint);

        Ray ray = new Ray(startPoint, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(!hit.transform.gameObject.CompareTag("Terrain"))
                return false;
        }

        return true;
    }

    void CreateRoad(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = endPoint - startPoint;
        distance = Vector3.Distance(startPoint, endPoint);

        // Instantiate the road prefab

        
        
        
        road = Instantiate(roadPrefab, startPoint, Quaternion.LookRotation(direction));

        // Adjust the scale of the road to fit the distance
        road.transform.localScale = new Vector3(1f, 1f, distance);

        // Adjust the position of the road to the midpoint between start and end points
        road.transform.position = startPoint + 0.5f * direction;

        Create.SetActive(true);
        Cancel.SetActive(true);
    }

    public void createPressed(){
        Destroy(arrowstart);
        Destroy(arrowend);
        Create.SetActive(false);
        Cancel.SetActive(false);

        //cost logic always to be used
        int cost = calculateCost();
        if (costManager != null)
        {
            costManager.deductCost(cost);
        }


        isStart = 1;
       // Debug.Log(isStart);
        InstantiateAtMouse.instance.isSelected = false ;
        Debug.Log(isStart);
    }

    public int calculateCost(){
        n = distance/UnitValueForRoad;
        costOfRoad = n*pricePerUnit;
        return (int)costOfRoad;
    }

    public void cancelPressed(){
        Destroy(arrowstart);
        Destroy(arrowend);
        Destroy(road);
        Create.SetActive(false);
        Cancel.SetActive(false);

       // Debug.Log(isStart);
        InstantiateAtMouse.instance.isSelected = false;
        Debug.Log(isStart);
        isStart = 1;
    }

    void ShowErrorMessage()
    {
       // Debug.Log("Path is not clear. Cannot create road.");
    }
}


