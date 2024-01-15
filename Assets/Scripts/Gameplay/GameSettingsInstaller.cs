using GameName.Gameplay.Combat;
using GameName.HealthSystem;
using GameName.MazeSystem;
using UnityEngine;
using Zenject;

namespace GameName.Gameplay
{
    [CreateAssetMenu(menuName = "Installers/Game Settings")]
    public sealed class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        private PlayerActor.Stats playerStats;

        [SerializeField]
        private FactoryPrefabs factoryPrefabs;

        [SerializeField]
        private RangedUnitData rangedUnitData;

        [SerializeField]
        private RipplePostProcessor.RippleSettings rippleSettings;
        
        [SerializeField]
        private LevelGenerator.LevelGeneratorSettings levelGeneratorSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(levelGeneratorSettings).IfNotBound();
            Container.BindInstance(playerStats).IfNotBound();
            Container.BindInstance(rangedUnitData).IfNotBound();
            Container.BindInstance(rippleSettings).IfNotBound();
            
            Container.BindInstance(GetPlayerHealthInstance()).AsSingle().NonLazy();
            Container.BindInstance(levelGeneratorSettings.levelSuccessTime).WhenInjectedInto<Timer>();
            
            Container.BindFactory<EnemyActor, EnemyActor.Factory>()
                     .FromComponentInNewPrefab(factoryPrefabs.enemyPrefab);
        }

        private Health GetPlayerHealthInstance()
        {
            Health instance = new (playerStats.Health);
            return instance;
        }

        [System.Serializable]
        private class FactoryPrefabs
        {
            public EnemyActor enemyPrefab;
        }
    }
}