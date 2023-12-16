using UnityEngine;

[System.Serializable]
public class SerializedGameObject
{
    public string prefabPath;
    public SerializableTransform transformData;
    // Add other data you want to save...

    // Constructor (if needed)
    public SerializedGameObject(string prefabPath, SerializableTransform transformData)
    {
        this.prefabPath = prefabPath;
        this.transformData = transformData;
    }
}
