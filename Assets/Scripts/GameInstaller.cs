using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private Controls _controls;
    
    [SerializeField] private CellManager _cellManager;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private CellPaletteSettings _cellPaletteSettings;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<GameEvent>();
        Container.DeclareSignal<GameStatus>();

        _controls = new Controls();
        _controls.Menu.Disable();
        _controls.Game.Enable();
        Container.BindInstance(_controls.Game).AsSingle();

        Container.BindInstance(FindAnyObjectByType<Cell>());

        _cellManager.OnCellClicked += CellManagerOnCellClicked;
    }

    private void CellManagerOnCellClicked(Cell obj)
    {
        obj.SetSelect(_cellPaletteSettings.SelectCell);
    }
}
