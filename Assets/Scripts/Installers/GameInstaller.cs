using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private Controls _controls;
    
    [SerializeField]    private CellManager         _cellManager;
    [SerializeField]    private SceneController     _sceneController;
                        private CellPaletteSettings _cellPaletteSettings;

    public override void InstallBindings()
    {
        // Паттерн 
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<GameEvent>();   // регистрация
        Container.DeclareSignal<GameStatus>();  // регистрация

        _cellPaletteSettings = new CellPaletteSettings();

        _controls = new Controls();
        _controls.Game.Enable();
        Container.BindInstance(_controls).AsSingle();
        Container.BindInstance(_sceneController).AsSingle();
        Container.BindInstance(_cellPaletteSettings).AsSingle();        
        Container.BindInstance(FindAnyObjectByType<Cell>());

        //_cellManager.OnCellClicked += CellManagerOnCellClicked;

        var units = FindObjectOfType<Unit>();
        Container.BindInstance(units).AsSingle();

        //var teams = units.Select(t => t.Team).Distinct().ToList();
        //teams.Sort();
        //Container.Bind<ITurn>().To<OneByOneTurn>().AsSingle().WithArguments(teams);

        Container.Bind<ISharedData>().To<SingleSharedData>().AsSingle();
        Container.Bind<IGameplayCommand>().To<CheckerCommand>().AsSingle();
    }

    private void CellManagerOnCellClicked(Cell obj)
    {
        obj.SetSelect(_cellPaletteSettings.SelectCell);
    }
}
