using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{

    private void Start()
    {
        LoadTrackedObjectsData();
    }
    public void SaveTrackedObjectsData()
    {
        List<GameObject> trackedObjects = Tracker.instance.GetTrackedObjects();
        SerializedGameObjectList serializedObjects = new SerializedGameObjectList();

        foreach (GameObject obj in trackedObjects)
        {
            SerializedGameObject serializedObj = new SerializedGameObject(GetPrefabPathForObject(obj), new SerializableTransform(obj.transform));
            // Add other data you want to save...
            serializedObjects.list.Add(serializedObj);
        }
        string json = JsonUtility.ToJson(serializedObjects);
        string savePath = Path.Combine(Application.streamingAssetsPath, "trackedObjectsData.json");
        File.WriteAllText(savePath, json);
    }

    public void LoadTrackedObjectsData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "trackedObjectsData.json"); ;
        if (System.IO.File.Exists(path))
        {
            Debug.Log(this);
            string json = System.IO.File.ReadAllText(path);

            SerializedGameObjectList serializedObjects = JsonUtility.FromJson<SerializedGameObjectList>(json);

            foreach (SerializedGameObject serializedObj in serializedObjects.list)
            {
                InstantiateObjectFromSerializedData(serializedObj);
            }
        }
        else
        {
            Debug.Log("No tracked objects data found.");
        }
    }

    private void InstantiateObjectFromSerializedData(SerializedGameObject serializedObj)
    {
        GameObject prefab = Resources.Load<GameObject>(serializedObj.prefabPath);

        if (prefab != null)
        {
            GameObject newObj = Instantiate(prefab);
            Transform objTransform = newObj.transform;
            SetTransformFromSerializedData(objTransform, serializedObj.transformData);
            // Apply other saved data...
            Tracker.instance.TrackInstantiatedObject(newObj);
        }
        else
        {
            Debug.LogError("Prefab not found for: " + serializedObj.prefabPath);
        }
    }

    private void SetTransformFromSerializedData(Transform transform, SerializableTransform serializedTransform)
    {
        transform.position = new Vector3(serializedTransform.posX, serializedTransform.posY, serializedTransform.posZ);
        transform.rotation = Quaternion.Euler(serializedTransform.rotX, serializedTransform.rotY, serializedTransform.rotZ);
        transform.localScale = new Vector3(serializedTransform.scaleX, serializedTransform.scaleY, serializedTransform.scaleZ);
    }

    private string GetPrefabPathForObject(GameObject obj)
    {
        string originalName = obj.name.Replace("(Clone)", "");
        return "Prefabs/Instantiating Objects/" + originalName;
    }

    private void OnApplicationQuit()
    {
        SaveTrackedObjectsData();
    }
}
