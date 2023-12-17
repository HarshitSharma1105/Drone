using UnityEngine;

public class InstantiateAtMouse : MonoBehaviour
{
    public static InstantiateAtMouse instance;
    public GameObject prefabToInstantiate;
    public float areaWidth = 2.0f; // Width of the square area
    public float areaLength = 2.0f;
    public GameObject panel;
    public GameObject Road;
    public GameObject[] disallowedObjects; // Array of tags that prevent instantiation

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !panel.activeSelf) // Check for left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from the center of the screen
            Vector3 cameraAngles = Camera.main.transform.eulerAngles;
            Quaternion spawnRotation = Quaternion.Euler(0f, cameraAngles.y, cameraAngles.z); // Same Y and Z angles as the camera

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (!CheckdisallowedObjectsWithinArea(hit.point,spawnRotation) && CheckRoadWithinArea(hit.point,spawnRotation)) // Check for disallowed objects within the area
                {
                    Tracker.instance.TrackInstantiatedObject(Instantiate(prefabToInstantiate, hit.point, spawnRotation));
                    // Instantiate the prefab at the hit point of the raycast (center of the screen in this case)
                }
                else
                {
                    Debug.Log("Cannot instantiate due to disallowed objects in the area.");
                    // Handle or log the situation where disallowed objects are found in the area
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

    bool CheckRoadWithinArea(Vector3 center, Quaternion rotation)
    {
        Vector3 halfExtents = new Vector3((areaWidth + 1) / 2f, 0.5f, (areaWidth + 1) / 2f); // Half extents for the square area
        Collider[] colliders = Physics.OverlapBox(center, halfExtents, rotation); // Get colliders within the box area

        foreach (var collider in colliders)
        {
            if (collider.CompareTag(Road.tag))
            {
                return true;
            }
        }
        return false;
    }
}
