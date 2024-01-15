using GameName.Gameplay.Combat;
using GameName.Gameplay.Movement;
using GameName.MazeSystem;
using Zenject;

namespace GameName.Gameplay
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInitializable>().To<LevelGenerator>().AsSingle();
            Container.Bind<PositionData>().AsSingle();
            Container.Bind<RippleEffectOnDamage>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<Timer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RipplePostProcessor>().AsSingle().NonLazy();
        }
    }
}