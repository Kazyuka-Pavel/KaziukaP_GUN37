using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoBehaviour
{
    [Inject]
    private Transform _sphere;
    
    private void Awake()
    {        
        Vector3 position = _sphere.position;
    }
}
