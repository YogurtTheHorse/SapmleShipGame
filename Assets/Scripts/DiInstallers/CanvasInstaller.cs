using UnityEngine;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class CanvasInstaller : MonoInstaller
{
    public string canvasId;
    
    public override void InstallBindings()
    {
        Container.BindInstance(GetComponent<Canvas>()).WithId(canvasId);
    }
}