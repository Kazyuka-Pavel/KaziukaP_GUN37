using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{    
    public override void InstallBindings()
    {
        //Привязки
        var _sceneController = new SceneController();
        Container.BindInstance(_sceneController).AsSingle();

        var _gameController = new GameController();
        Container.BindInstance(_gameController).AsSingle();

        var _indicatorController = new IndicatorController();
        Container.BindInstance(_indicatorController).AsSingle();
    }
}
