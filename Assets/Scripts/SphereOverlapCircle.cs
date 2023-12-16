using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSphereOveralpCircle : MonoBehaviour
{
    public float sphereRadius = 5f; // Set the overall sphere radius here
    public int circleSegments = 64; // Number of segments in the circle

    private LineRenderer lineRenderer;

    void Start()
    {
        // Add LineRenderer component to the GameObject
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Set LineRenderer properties
        lineRenderer.positionCount = circleSegments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;

        // Update the circle
        UpdateCircle();
    }

    void Update()
    {
        // You can update the circle during runtime if needed
        // For example, if the sphere radius changes dynamically.
        UpdateCircle();
    }

    void UpdateCircle()
    {
        float deltaTheta = 2 * Mathf.PI / circleSegments;
        float theta = 0f;

        // Calculate circle points
        for (int i = 0; i < circleSegments + 1; i++)
        {
            float x = sphereRadius * Mathf.Cos(theta);
            float z = sphereRadius * Mathf.Sin(theta);

            lineRenderer.SetPosition(i, new Vector3(x, 0f, z));

            theta += deltaTheta;
        }
    }
}
