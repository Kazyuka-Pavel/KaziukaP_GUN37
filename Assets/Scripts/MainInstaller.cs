using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private Transform _sphere;
    public override void InstallBindings()
    {
        Container.BindInstance<Transform>(_sphere).AsSingle();    
    }
}
