using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    private Controls _controls;

    [SerializeField]    private GameObject              _image;
    [SerializeField]    private UnityEngine.UI.Image    _imageEm;
    [SerializeField]    private float                   _speed = 1;
                        private SceneController         _sceneController;

    private void Awake()
    {
        _controls = new Controls();
        _controls.Game.Enable();        

        _controls.Game.Restart.started += Restart_started;
        _controls.Game.Restart.performed += Restart_performed;
        _controls.Game.Restart.canceled += Restart_canceled;

        _sceneController = new SceneController();
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
        IEnumerator Loop()
        {
            for (var i = _imageEm.fillAmount; i < 1f; i += Time.deltaTime * _speed)
            {
                _imageEm.fillAmount = i;
                yield return null;
            }
            StopCoroutine(Loop());
            //Debug.Log("End");
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
