using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class NewSceneLoader : MonoBehaviour
{
    public void New()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "trackedObjectsData.json");
        File.Delete(path);
        SceneManager.LoadScene(1);
    }
}
