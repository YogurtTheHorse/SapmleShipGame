using UnityEngine;
using Zenject;

[RequireComponent(typeof(SpaceShip))]
public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInstance(gameObject).WithId("player");
    }
}