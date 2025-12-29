using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TurnIndicator : MonoBehaviour
{
    private ITurn _turn; //injected    

    [SerializeField]    private TextMeshPro _leftIcon;
    [SerializeField]    private Image       _leftArrow;

    [SerializeField, Space(15f)]                    private TextMeshPro _rightIcon;
    [SerializeField]                                private Image       _rightArrow;

    private (Team team, TextMeshPro icon, Image arrow) _left;
    private (Team team, TextMeshPro icon, Image arrow) _right;

    private void CallBack(GameEvent status)
    {
        switch (status)
        {
            default:
                return;
            case GameEvent.NewTurn:
                _turn.Next();                
                break;
        }

        
        var (enable, disable) = _left.team == _turn.Current
            ? (_left, _right)
            : (_right, _left);

        enable.arrow.enabled = true;
        //enable.icon.transform.localScale = Vector3.one;
        //enable.icon.color = new Color(1f, 1f, 1f, 1f);

        disable.arrow.enabled = false;
        //disable.icon.transform.localScale = Vector3.one * _disableScale;
        //disable.icon.color = new Color(1f, 1f, 1f, _disableAlpha);
    }

    [Inject]
    private void Construct(SignalBus signal, ITurn turn, TurnPanelSettings settings)
    {
        signal.Subscribe<GameEvent>(CallBack);

        _turn = turn;

        var (left, right) = (settings[Team.White], settings[Team.Black]);
        _left = (left.Team, _leftIcon, _leftArrow);
        _right = (right.Team, _rightIcon, _rightArrow);
        //(_leftIcon.text, _rightIcon.text) = (left.Text, right.Text);
       // (_leftIcon.color, _rightIcon.color) = (left.Color, right.Color);
    }
}
