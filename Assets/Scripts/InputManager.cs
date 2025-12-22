using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;

public class InputManager : MonoBehaviour
{
    [SerializeField]    private GameObject              _image;
    [SerializeField]    private UnityEngine.UI.Image    _imageEm;
    [SerializeField]    private float                   _speed = 1;
    [Inject]            private SceneController         _sceneController;
    [Inject]            private Controls                _controls;

    private void Awake()
    {        
        _controls.Game.Restart.started += Restart_started;
        _controls.Game.Restart.performed += Restart_performed;
        _controls.Game.Restart.canceled += Restart_canceled;        
    }

    private void Restart_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_image.IsDestroyed() == false)
        {
            _image.SetActive(false);
            _imageEm.fillAmount = 0;
        }        
    }

    private void Restart_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {        
        StartCoroutine(Loop());                
    }

    IEnumerator Loop()
    {
        var i = 0f;
        _imageEm.fillAmount = 0.1f;
        for (i = _imageEm.fillAmount; i < 1f; i += Time.deltaTime * _speed)
        {
            if (_imageEm.fillAmount == 0)
            {
                break;
            }
            _imageEm.fillAmount = i;
            yield return null;            
        }
        StopCoroutine(Loop());
        //Debug.Log("End");
        if (i >= 1f) {
            _controls.Game.Restart.started -= Restart_started;
            _controls.Game.Restart.performed -= Restart_performed;
            _controls.Game.Restart.canceled -= Restart_canceled;
            _sceneController.RestartGameScene(); 
        }
    }

    private void Restart_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(_image.IsDestroyed() == false)
        {
            _image.SetActive(true);
            _imageEm.fillAmount = 0;
        }        
    }
}
