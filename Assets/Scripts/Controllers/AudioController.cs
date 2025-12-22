using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private AudioClip _selectSource;
    [SerializeField]
    private AudioClip _cancelSource;
    [SerializeField]
    private AudioClip _confirmSource;

    [Inject]
    private void Construct(SignalBus signal)
    {        
        signal.Subscribe<GameEvent>(Callback); //Подпись на событие GameEvent
    }
    private void Callback(GameEvent gameEvent) 
    {
        var clip = default(AudioClip);
        switch (gameEvent) 
        { 
            case GameEvent.Select:
                clip = _selectSource;
                break;
            case GameEvent.Cancel:
                clip = _cancelSource;
                break;
            case GameEvent.Confirm:
                clip = _confirmSource;
                break;
        }
        _source.clip = clip;
        _source.Play();
    }
}
