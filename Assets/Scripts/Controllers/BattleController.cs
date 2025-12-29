using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class BattleController : MonoBehaviour
{
    private IGameplayCommand _command;          //injected
    private ISharedData _data;                  //injected
    private SignalBus _signal;                  //injected
    private Controls.GameActions _controls;     //injected
    private Battlefield _battlefield;           //injected

    [Inject]
    private void Construct(IGameplayCommand command, ISharedData data, SignalBus signal,Battlefield battlefield, Controls.GameActions controls)
    {
        (_command, _data, _signal, _battlefield, _controls) = (command, data, signal, battlefield, controls);
        _battlefield.OnCellClicked += _command.Interact;
        _controls.Cancel.performed += OnCancel;
        _controls.Confirm.performed += OnConfirm;
        _signal.Subscribe<GameEvent>(Callback); //Подпись на событие GameEvent
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (_data.Event is GameEvent.Play) return;
        if (_data.Target == null)
        {
            Debug.Log("Non selected cell");
            return;
        }
        _data.Event = GameEvent.Confirm;
        _signal.Fire(GameEvent.Confirm);
    }

    // Обработка инпута. Стандартный экшен.
    private void OnCancel(InputAction.CallbackContext obj)
    {
        if (_data.Event is GameEvent.Play) return;
        _signal.Fire(GameEvent.Cancel);
    }

    //Переход в следующий режим
    private void Callback(GameEvent arg)
    {
        switch (arg)
        {
            case GameEvent.NewTurn:
                _data.Target = null;
                _data.Cells.Clear();
                _data.Destination = null; 
                _command.CellsDictionary.Clear();
                _battlefield.ResetSelect();
                _data.Event = GameEvent.SelectUnit;
                _signal.Fire(GameEvent.SelectUnit);
                break;
            case GameEvent.Cancel:
                _data.Target = null;
                _data.Destination = null;
                _data.Cells.Clear();
                _command.CellsDictionary.Clear();
                _data.Event = GameEvent.SelectUnit;
                _signal.Fire(GameEvent.SelectUnit);
                break;
        }
    }   
}
