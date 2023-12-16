using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Scene has finished loading
        Debug.Log("Scene loaded: " );

        // Add your logic here for when the scene has loaded
    }

    void Start()
    {
        StartCoroutine(LoadScene());
    }
}
