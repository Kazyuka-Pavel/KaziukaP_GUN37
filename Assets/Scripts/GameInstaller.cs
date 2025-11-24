using System.Collections;
using System.Collections.Generic;
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
        _controls = new Controls();
        _controls.Menu.Disable();
        _controls.Game.Enable();

        Container.BindInstance(_controls.Game).AsSingle();
        Container.BindInstance(_cellManager).AsSingle();
        Container.BindInstance(_sceneController).AsSingle();
        //Container.BindInstance(_unit).AsSingle();

        //Test
        var test = _controls.Game.Restart.ReadValue<float>();

        _cellManager.OnCellClicked += CellManagerOnCellClicked;
    }

    private void CellManagerOnCellClicked(Cell obj)
    {
        obj.SetSelect(_cellPaletteSettings.SelectCell);
    }
}
