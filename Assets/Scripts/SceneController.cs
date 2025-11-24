using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void OpenMainScene()
    {
        
    }
    
    public void OpenGameScene()
    {
        SceneManager.LoadScene(1);        
    }

    public void RestartGameScene()
    {
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadSceneAsync(1);
    }
}
