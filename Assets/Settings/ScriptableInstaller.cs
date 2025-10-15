using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableInstaller", menuName = "Settings/Installer")]

public class ScriptableInstaller : ScriptableObjectInstaller<ScriptableInstaller>
{
    [SerializeField]
    private Transform _prefabSample1;
    [SerializeField]
    private Transform _prefabSample2;
    [SerializeField]
    private Transform _prefabSample3;

    public override void InstallBindings()
    {
        // Здесть отдельно биндится
        Container.Bind<Transform>().WithId("1").FromInstance(_prefabSample1);
        Container.Bind<Transform>().WithId("2").FromInstance(_prefabSample2);
        Container.Bind<Transform>().WithId("3").FromInstance(_prefabSample3);

        // Здесть биндится через ИД, но ид зашит в константу.
        Container.BindInstance(_prefabSample1).WithId(InjectConstants.PlayerPrefab).AsSingle();

        // Здесть биндится сразу массив, и потом получить получится массив
        Container.BindInstances(_prefabSample1, _prefabSample2, _prefabSample3);

        
    }
}