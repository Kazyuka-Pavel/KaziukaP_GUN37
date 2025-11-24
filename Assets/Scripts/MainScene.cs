using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainScene : MonoBehaviour
{
    private InputAction enterAction;
    private @Controls   controls;
    private SceneController sceneController;

    void Awake()
    {
        sceneController = new SceneController();
        controls = new @Controls();
        controls.Menu.Enable();
        controls.Game.Disable();
        controls.Menu.StrartGame.performed += context => StrartGame();
    }

    void StrartGame()
    {
        sceneController.OpenGameScene();        
    }

    private void OnEnable()
    {
        controls.Menu.Enable();
    }

    private void OnDisable()
    {
        controls.Menu.Disable();
    }
}
