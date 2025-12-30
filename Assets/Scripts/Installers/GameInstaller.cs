using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private Controls            _controls;
    private SceneController     _sceneController;

    [SerializeField]    private Camera              _camera;    
    [SerializeField]    private UnitGameSettings    _queenSettings;
    [SerializeField]    private TurnPanelSettings   _turnPanelSettings;
    [SerializeField]    private UnitsSettings       _unitsSettings;
    [SerializeField]    private CellPaletteSettings _cellPaletteSettings;

    public override void InstallBindings()
    {
        // Паттерн 
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<GameEvent>();   // регистрация

        _controls = new Controls();
        _controls.Game.Enable();
        Container.BindInstance(_controls.Game).AsSingle();

        _sceneController = ScriptableObject.CreateInstance<SceneController>();
        Container.BindInstance(_sceneController).AsSingle();

        Container.BindInstances(FindAnyObjectByType<Cell>());
        Container.BindInterfacesAndSelfTo<Battlefield>().AsSingle();
        
        Container.BindInstance(_camera).AsSingle();
        Container.BindInstance(_queenSettings).AsSingle();
        Container.BindInstance(_turnPanelSettings).AsSingle();
        Container.BindInstance(_unitsSettings).AsSingle();
        Container.BindInstance(_cellPaletteSettings).AsSingle();

        Container.BindInstance(FindAnyObjectByType<TurnIndicator>()).AsSingle();        

        var units = FindObjectsOfType<Unit>().ToList<Unit>();
        Container.BindInstance(units).AsSingle();

        // не получилось
        //var teams = units.Select(t => t.Team).Distinct().ToList();
        //teams.Sort();
        var teams = new List<Team>() { Team.Black, Team.White };
        Container.Bind<ITurn>().To<OneByOneTurn>().AsSingle().WithArguments(teams);
        Container.Bind<ISharedData>().To<SingleSharedData>().AsSingle();
        Container.Bind<IGameplayCommand>().To<CheckerCommand>().AsSingle();
    }

    private void OnDestroy()
    {
        _controls.Dispose();
    }
}
