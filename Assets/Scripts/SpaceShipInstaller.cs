using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpaceShipInstaller : MonoInstaller
{
    public Rocket rocketPrefab;
    
    public override void InstallBindings()
    {
        Container.BindFactory<Rocket, Rocket.Factory>().FromComponentInNewPrefab(rocketPrefab);
        Container.Bind<RocketPool>().AsSingle();
    }
}
