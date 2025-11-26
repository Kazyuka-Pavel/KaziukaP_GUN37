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
    private PlayerController _battlefield;      //injected

    private void OnCancel(InputAction.CallbackContext obj)
    {
        _data.Event = GameEvent.Cancel;
        _data.Status = GameStatus.Select;
    }

    private void Callback(GameEvent arg)
    {
        if (arg is not GameEvent.Select) return;

        switch (_data.Status)
        {
            case GameStatus.Select:
                _signal.Fire(GameStatus.Move);
                break;
            case GameStatus.Move:
                _signal.Fire(GameStatus.Confirm);
                break;
            case GameStatus.Attack:
                _signal.Fire(GameStatus.Confirm);
                break;
            case GameStatus.Confirm:
                Debug.LogError("Incorrect value");
                break;
        }
    }
}
