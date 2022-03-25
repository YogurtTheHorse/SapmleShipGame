using Ships;
using UnityEngine;
using Zenject;

namespace DiInstallers
{
    [RequireComponent(typeof(SpaceShip))]
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance(gameObject).WithId("player");
        }
    }
}