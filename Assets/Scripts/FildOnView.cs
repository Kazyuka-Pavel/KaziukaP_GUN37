using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FildOnView : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField, Range(0, 360)] private float _angle;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    private bool _cenSeePlayer;

    public bool CenSeePlayer => _cenSeePlayer;
    public float Radius => _radius;
    public float Angle => _angle;
    public Player Player => _player;

    private void Start()
    {
        
    }

    private IEnumerator FovRountine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (!_player)
        {
            yield return wait;
            FildOfViewCheck();
        }
    }

    private void FildOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstacleMask))
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
