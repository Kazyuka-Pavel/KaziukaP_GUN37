using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private SignalBus   _signal;    //injected
    private ISharedData _data;      //injected
    private float _speed = 1f;

    [Inject]
    private void Construct(SignalBus signal, ISharedData data)
    {
        //Подписка на сигнал типа GameEvent
        (_signal, _data) = (signal, data);
        _signal.Subscribe<GameEvent>(StartPlay);
    }

    private void StartPlay(GameEvent arg)
    {
        if (arg is not GameEvent.Confirm) return; //Если не подтверждено, то продолжить
        if (_data.Status is not GameStatus.Confirm) return; //Проверяем, что находимся в игровом статусе Confirm

        _data.Status = GameStatus.Lock;
        var destination = _data.Destination;

        //Движение к точке
        //if (_data.Target.IsEmpty)
        //{
        //    destination.OnMoveEndCallback += OnEndPlay;
        //    destination.Move(_data.Target);
        //}
        ////Атака по цели
        //else
        //{
        //    var target = _data.Target.Unit;
        //    target.Health -= destination.Settings.Stats.Damage;
        //    if (target.Health <= 0)
        //    {
        //        _data.Target.Unit = null;
        //        Destroy(target.gameObject);
        //    }
        //    _data.Target = null;
        //    _data.Status = GameStatus.Unlock;
        //}
        
    }

    public void Move(Cell cell)
    {
        StartCoroutine(OnMove(cell));
    }

    private IEnumerator OnMove(Cell cell) 
    {
        var source = transform;
        var start = source.position;
        var end = cell.transform.position;
        var time = Vector3.Distance(start, end) / _speed;
        var delta = 0f;
        while (delta < time) 
        {
            source.position = Vector3.Lerp(start, end, delta / time);
            delta += Time.deltaTime;
            yield return null;
        }

        //Cell = cell;
        //OnMoveEndCallback?.Invoke();
    }

    private void OnEndPlay() 
    {
        //_data.Status = GameStatus.Unlock;
        //_data.Destination.OnMoveEndCallbalck -= OnEndPlay;
        //if (_data.Destination.Settings.Mobility.MoveAndAttackInTurn)
        //{
        //    _data.Target = null;
        //    _data.Status = GameStatus.Attack;
        //}
    }
}
