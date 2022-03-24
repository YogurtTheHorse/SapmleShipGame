using UnityEngine;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class CanvasInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInstance(GetComponent<Canvas>()).WithId("canvas");
    }
}