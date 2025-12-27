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
        if (_data.Target == null)
        {
            Debug.Log("Non selected cell");
            return;
        }

        _data.Status = GameStatus.Confirm;
        _data.Event = GameEvent.Confirm;
        //_signal.Fire(GameStatus.Confirm);
        _signal.Fire(GameEvent.Confirm);
    }

    // Обработка инпута. Стандартный экшен.
    private void OnCancel(InputAction.CallbackContext obj)
    {
        _signal.Fire(GameEvent.Cancel);
    }

    //Переход в следующий режим
    private void Callback(GameEvent arg)
    {
        switch (arg)
        {
            case GameEvent.NewTurn:
                _data.Status = GameStatus.Select ;
                _data.Target = null;
                _data.Cells.Clear();
                _data.Destination = null; 
                _command.CellsDictionary.Clear();
                break;
            case GameEvent.Cancel:
                _data.Status = GameStatus.Select;
                _data.Target = null;
                _data.Destination = null;
                _data.Cells.Clear();
                _command.CellsDictionary.Clear();
                break;
        }
        
        //if (arg is not GameEvent.Select) return;

        //switch (_data.Status)
        //{
        //    case GameStatus.Select:
        //        _data.Status = GameStatus.Move;                
        //        break;
        //    case GameStatus.Move:
        //        _signal.Fire(GameStatus.Confirm);
        //        break;
        //    case GameStatus.Attack:
        //        _signal.Fire(GameStatus.Confirm);
        //        break;
        //    case GameStatus.Confirm:
        //        Debug.LogError("Incorrect value");
        //        break;
        //}
    }   
}
