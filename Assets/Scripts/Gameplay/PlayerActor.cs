using GameName.Gameplay.Combat;
using GameName.Gameplay.Movement;
using GameName.HealthSystem;
using UnityEngine;
using Zenject;

namespace GameName.Gameplay
{
    public class PlayerActor : MonoBehaviour, IDamageReceiver
    {
        [System.Serializable]
        public class Stats
        {
            [field: SerializeField] public int Health { get; private set; }
            [field: SerializeField] public float MovementSpeed { get; private set; }
        }

        [SerializeField] 
        private ObjectMover objectMover;

        private Stats _stats;
        private Health _actorHealth;
        private PlayerInput _playerInput;
        private PositionData _playerPositionData;

        private Transform _transform;

        [Inject]
        private void Construct(Stats stat, PlayerInput input, Health health, PositionData pos)
        {
            _stats = stat;
            _playerInput = input;
            _actorHealth = health;
            _transform = transform;
            _playerPositionData = pos;
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            HandleMovement(delta);
            UpdatePositionData();
        }
        
        public void TakeDamage(int amount)
        {
            _actorHealth.Remove(amount);
        }

        public Transform Transform()
        {
            return _transform;
        }

        private void UpdatePositionData()
        {
            _playerPositionData.Set(_transform.position);
        }

        private void HandleMovement(float delta)
        {
            objectMover.AddPosition(_playerInput.Value * (_stats.MovementSpeed * delta));
        }
    }
}