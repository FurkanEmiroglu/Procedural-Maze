using GameName.Gameplay.Combat;
using GameName.Gameplay.Movement;
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
        private Sensor.SensorSettings enemySensors;

        [SerializeField]
        private Sensor.SensorSettings instantKillSensor;
        
        [SerializeField]
        private LevelGenerator.LevelGeneratorSettings levelGeneratorSettings;

        [SerializeField]
        private RipplePostProcessor.RippleSettings rippleSettings;
        
        [SerializeField]
        private SimpleCameraController.CameraSettings cameraSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(levelGeneratorSettings).IfNotBound();
            Container.BindInstance(playerStats).IfNotBound();
            Container.BindInstance(rangedUnitData).IfNotBound();
            Container.BindInstance(rippleSettings).IfNotBound();
            Container.BindInstance(cameraSettings).IfNotBound();
            Container.BindInstance(enemySensors)
                     .When(ctx => ((Sensor)ctx.ObjectInstance).SelectedType == Sensor.SensorType.RangeUnitDetection);

            Container.BindInstance(instantKillSensor)
                     .When(ctx => ((Sensor)ctx.ObjectInstance).SelectedType == Sensor.SensorType.InstantKill);

            Container.Bind<Health>().AsSingle().WithArguments(playerStats.Health).NonLazy();
            Container.BindInstance(levelGeneratorSettings.levelSuccessTime).WhenInjectedInto<Timer>();
            
            Container.BindFactory<EnemyActor, EnemyActor.Factory>()
                     .FromComponentInNewPrefab(factoryPrefabs.enemyPrefab);
        }

        [System.Serializable]
        private class FactoryPrefabs
        {
            public EnemyActor enemyPrefab;
        }
    }
}