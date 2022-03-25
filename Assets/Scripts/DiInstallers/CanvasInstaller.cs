using UnityEngine;
using Zenject;

namespace DiInstallers
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasInstaller : MonoInstaller
    {
        public string canvasId;
    
        public override void InstallBindings()
        {
            Container.BindInstance(GetComponent<Canvas>()).WithId(canvasId);
        }
    }
}