using UnityEngine;

[System.Serializable]
public class SerializableTransform
{
    public float posX, posY, posZ;
    public float rotX, rotY, rotZ;
    public float scaleX, scaleY, scaleZ;

    // Constructor (if needed)
    public SerializableTransform(Transform transform)
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

        rotX = transform.rotation.eulerAngles.x;
        rotY = transform.rotation.eulerAngles.y;
        rotZ = transform.rotation.eulerAngles.z;

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }
}
