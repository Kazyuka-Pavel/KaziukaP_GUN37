using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : ScriptableObject
{    
    public void RestartGameScene()
    {
        //SceneManager.UnloadSceneAsync(0);
        //SceneManager.LoadSceneAsync(0);
        SceneManager.LoadScene(0);
    }
}
