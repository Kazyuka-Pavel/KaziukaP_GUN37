using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : ScriptableObject
{    
    public void RestartGameScene()
    {
        SceneManager.LoadScene(0);
    }
}
