using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FildOnView : MonoBehaviour
{
    // Радиус
    [SerializeField] private float _radius;
    // Угол обзора
    [SerializeField, Range(0, 360)] private float _angle;
    // Игрок
    [SerializeField] private Player _player;
    // Слой на котором игроки
    [SerializeField] private LayerMask _targetMask;
    // Слой на котором препятствия
    [SerializeField] private LayerMask _obstacleMask;

    //Можем ли видеть игрока
    private bool _cenSeePlayer;

    public bool CenSeePlayer => _cenSeePlayer;
    public float Radius => _radius;
    public float Angle => _angle;
    public Player Player => _player;

    private void Start()
    {
        StartCoroutine(FovRountine());
    }

    //Корутина контроля видимости
    private IEnumerator FovRountine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FildOfViewCheck();
        }
    }

    private void FildOfViewCheck()
    {
        // Рисуем сферу
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length > 0)
        {
            // Если кто-то попал, то сохраняем трансформ
            Transform target = rangeChecks[0].transform;
            // Рисуем вектор, который нправлен к этому объекту
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //Если вектор меньше, чем угол / 2
            if(Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                // дистанция между целью
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //Если позиция доступна
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstacleMask))
                {
                    _cenSeePlayer = true;
                }
                else
                {
                    _cenSeePlayer = false;
                }
            }
            else
            {
                _cenSeePlayer = false;
            }
        }
        else if (_cenSeePlayer)
        {
            _cenSeePlayer = false;
        }
    }
}
