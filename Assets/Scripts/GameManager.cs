using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start(){
        SceneManager.LoadScene(1);
    }

    public void Instructions(){
        SceneManager.LoadScene(1);
    }
    
    public void Settings(){
        SceneManager.LoadScene(1);
    }

    public void Quit(){
        Application.Quit();
    }
}
