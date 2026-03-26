using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : ScriptableObject
{    
    public void OpenMainScene()
    {
        SceneManager.LoadScene(0);
    }
}
