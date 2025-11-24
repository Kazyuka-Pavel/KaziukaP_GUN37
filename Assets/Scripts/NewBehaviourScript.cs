using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Controls _controls;

    [SerializeField]
    private SceneController _sceneController;

    private void Update()
    {
        //_controls.Game.Table.IsPressed
    }

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        _controls.Menu.Enable();
    }

    private void Start()
    {
        _controls = new Controls();
        _controls.Menu.StrartGame.started += StrartGame_performed;
        
    }

  
    private void StrartGame_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _sceneController.OpenGameScene();
    }
    

    private void OnDisable()
    {
        _controls.Menu.Disable();
    }
}
