using Explosions;
using Rockets;
using Zenject;

namespace DiInstallers
{
    public class GameInstaller : MonoInstaller
    {
        public Rocket rocketPrefab;

        public Explosion explosionPrefab;
    
        public override void InstallBindings()
        {
            Container.BindFactory<Rocket, Rocket.Factory>().FromComponentInNewPrefab(rocketPrefab);
            Container.BindFactory<Explosion, ExplosionSpawner.ExplosionFactory>().FromComponentInNewPrefab(explosionPrefab);
            Container.Bind<RocketPool>().AsSingle();
            Container.Bind<ExplosionSpawner>().AsSingle();
        }
    }
}
