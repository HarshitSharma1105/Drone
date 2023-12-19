using UnityEngine;

public class InstantiateAtMouse : MonoBehaviour
{
    public static InstantiateAtMouse instance;
    public GameObject Hover;
    public GameObject prefabToInstantiate;
    public float areaWidth = 2.0f; // Width of the square area
    public float areaLength = 2.0f;
    public float height = 1;
    public GameObject panel;
    public bool isSelected;
    public GameObject[] disallowedObjects;
    Vector3 position;
    // Array of tags that prevent instantiation
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        isSelected = false;
    }
    void Update()
    {
        if (!Camera.main.GetComponent<OutlineSelection>().enabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from the center of the screen
            Vector3 cameraAngles = Camera.main.transform.eulerAngles;
            Quaternion spawnRotation = Quaternion.Euler(0f, cameraAngles.y, cameraAngles.z); // Same Y and Z angles as the camera
            Hover.transform.rotation = spawnRotation;
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                position = new Vector3(hit.point.x, height, hit.point.z);
                Hover.transform.position = position;

                if (Input.GetKeyDown(KeyCode.Mouse0) && !panel.activeSelf && isSelected && prefabToInstantiate.tag != "Road") // Check for left mouse button click
                {
                    foreach (Transform child in Hover.transform)
                    {
                        Destroy(child.gameObject);
                    }

                    //if (!CheckdisallowedObjectsWithinArea(hit.point, spawnRotation))//&& CheckRoadWithinArea(hit.point,spawnRotation)) // Check for disallowed objects within the area
                    {
                        isSelected = false;
                        Tracker.instance.TrackInstantiatedObject(Instantiate(prefabToInstantiate, position, spawnRotation));
                        Camera.main.GetComponent<OutlineSelection>().enabled = true;
                        // Instantiate the prefab at the hit point of the raycast (center of the screen in this case)
                    }
                }
            }
        }
    }

    bool CheckdisallowedObjectsWithinArea(Vector3 center,Quaternion rotation)
    {
        Vector3 halfExtents = new Vector3(areaLength / 2f, 0.5f, areaWidth / 2f); // Half extents for the square area
        Collider[] colliders = Physics.OverlapBox(center, halfExtents,rotation); // Get colliders within the box area

        foreach (var collider in colliders)
        {
            foreach (var Object in disallowedObjects)
            {
                if (collider.CompareTag(Object.tag))
                {
                    return true; // Disallowed object found within the area
                }
            }
        }
        return false; // No disallowed objects found within the area
    }
}
