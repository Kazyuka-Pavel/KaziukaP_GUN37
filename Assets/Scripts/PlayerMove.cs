using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _horizontalTurnSensotivity = 10;
    [SerializeField] private float _verticalTurnSensotivity = 10;
    [SerializeField] private float _verticalMinAngle = -89;
    [SerializeField] private float _verticalMaxAngle = 89;
    [SerializeField] private float _jumpSpeed = 7;

    [SerializeField] private CharacterController _characterController;

    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _cameraTransform;

    private Vector3 _verticalVelocity;
    private float _cameraAngle = 0;

    private void Awake()
    {
        //_transform = transform;
        //_characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_characterController != null)
        {
            //{{EXTCODE 
            //OLD
            //Vector3 playerSpeed = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));
            ////Вектор напрвления умножаем на скорость
            //playerSpeed *= Time.deltaTime * _speed;
            //NEW
            Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
            Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

            _cameraAngle -= Input.GetAxis("Mouse Y") * _verticalTurnSensotivity;
            _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
            _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;

            _transform.Rotate(Vector3.up * _horizontalTurnSensotivity, Input.GetAxis("Mouse X"));

            Vector3 playerSpeed = forward * Input.GetAxis("Vertical") * _speed + right * Input.GetAxis("Horizontal") * _speed;            
            //EXTCODE}}            

            //Если на плоскости
            if (_characterController.isGrounded)
            {
                //{{EXTCODE 
                //OLD
                ////Будет притягиваться к плоскости
                //_characterController.Move(playerSpeed + Physics.gravity);
                //NEW
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _verticalVelocity = Vector3.up * _jumpSpeed;
                }
                else
                {
                    _verticalVelocity = Vector3.down;
                }
                _characterController.Move((playerSpeed + _verticalVelocity) * Time.deltaTime);
                //EXTCODE}}            
            }
            else
            {
                //{{EXTCODE 
                //OLD
                //// Смесим на плоскость
                //_characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);
                //NEW
                Vector3 horizontalVelicity = _characterController.velocity;
                horizontalVelicity.y = 0;
                _verticalVelocity += Physics.gravity * Time.deltaTime;
                _characterController.Move((_characterController.velocity + _verticalVelocity) * Time.deltaTime);
                //EXTCODE}}                            
            }
        }
    }

    //Событие столкновения
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Если есть ригитбади, т.к. отрабатывал столконение с землей
        if (hit.gameObject.GetComponent<Rigidbody>())
        {
            hit.rigidbody.velocity = Vector3.up * 10;
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_transform.position, Vector3.right + Vector3.forward + Vector3.up * _characterController.height);
    }
}
