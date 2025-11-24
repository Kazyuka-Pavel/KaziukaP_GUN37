using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    private Controls _controls;

    [SerializeField]
    private CellManager _cellManager;
    [SerializeField]
    private SceneController _sceneController;
    [SerializeField, Space(15f)]
    private CellPaletteSettings _cellPaletteSettings;

    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Menu.Enable();
    }
}
