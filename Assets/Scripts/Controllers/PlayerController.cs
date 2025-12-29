using System;
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
        _signal.Subscribe<GameEvent>(Callback);
    }

    private void Callback(GameEvent arg)
    {
        if (arg is not GameEvent.Confirm) return; //Если не подтверждено, то продолжить
        if (_data.Event is not GameEvent.Confirm) return;

        var destination = _data.Destination;

        //Движение к точке
        if (_data.Target.Unit == null)
        {
            _data.Event = GameEvent.Play;
            destination.OnMoveEndCallback += OnEndPlay;
            destination.Move(_data.Target);
        }
    }

    private void OnEndPlay() 
    {        
        _data.Destination.OnMoveEndCallback -= OnEndPlay;
        foreach (var cell in _data.Cells)
        {
            if ((!cell.IsEmpty) && (cell != _data.Target))
                cell.Unit.DestroyGameObject();
        }
        _signal.Fire(GameEvent.End); 
    }
}
