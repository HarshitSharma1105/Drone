using UnityEngine;

public class SphereOverlapDetector : MonoBehaviour
{
    public float sphereRadius = 5f; // Adjust the radius as needed

    public int houseCount = 0;

    void CountHouse()
    {
        // Check for overlaps in the sphere
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);

        // Count the number of houses inside the sphere

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Houses"))
            {
                houseCount++;
            }
        }

        // Print the result or use it as needed
        //Debug.Log("Number of houses in the sphere: " + houseCount);
    }

    // You can also visualize the sphere in the scene view using OnDrawGizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
