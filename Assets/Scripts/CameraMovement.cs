using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float scrollSpeed = 50f;
    public float altitudeSpeed=20f;
    public float rotationSpeed = 5.0f;
    public float movementScale = 1.0f;
    public float scrollScale = 1.0f;
    private bool isRotating = false;
    public float minY = 5.0f;
    private Vector3 lastMousePosition;
    private Transform cameraTransform;


    void Start(){
        cameraTransform = Camera.main.transform;
    }



    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float altitudeInput = Input.GetAxis("Altitude");
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        float distanceFromGround=transform.position.y;
        movementSpeed = distanceFromGround * movementScale;
        scrollSpeed = distanceFromGround * scrollScale;
        Vector3 newPosition = transform.position;
        newPosition.y = Mathf.Max(newPosition.y, minY);
        transform.position = newPosition;

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed * Time.deltaTime
                            + new Vector3(0f, altitudeInput, 0f) * altitudeSpeed * Time.deltaTime;
        Vector3 cameraForwardXZ = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRightXZ = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        

        if (scrollInput != 0)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;

            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3 moveDirection = (worldMousePosition - transform.position).normalized;

            transform.position += moveDirection * scrollInput * scrollSpeed;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationX = -delta.y * rotationSpeed;
            float rotationY = delta.x * rotationSpeed;

            transform.RotateAround(transform.position, transform.right, rotationX);
            transform.RotateAround(transform.position, Vector3.up, rotationY);

            lastMousePosition = Input.mousePosition;
        }

        transform.position += cameraForwardXZ * movement.z + cameraRightXZ * movement.x +movement.y*Vector3.up;
  
    }

}