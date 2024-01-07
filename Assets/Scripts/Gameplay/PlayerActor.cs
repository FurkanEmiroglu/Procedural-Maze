using GameName.Gameplay.Combat;
using GameName.Gameplay.Movement;
using GameName.HealthSystem;
using GameName.SOInjection;
using UnityEngine;

namespace GameName.Gameplay
{
    public class PlayerActor : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] 
        private ActorStats actorStats;

        [SerializeField] 
        private InjectedInt health;

        [SerializeField] 
        private InjectedBool rippleRequest;

        [SerializeField] 
        private ObjectMover objectMover;

        private Health _actorHealth;
        private InputReceiver _input;

        private Transform _transform;

        private void OnValidate()
        {
            objectMover = GetComponent<ObjectMover>();
        }

        private void Awake()
        {
            _actorHealth = new Health(actorStats.Health);
            health.Set(_actorHealth.Value);
            _input = new InputReceiver();
            _transform = transform;
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            HandleMovement(delta);
        }
        
        public void TakeDamage(int amount)
        {
            _actorHealth.Remove(amount);
            health.Set(_actorHealth.Value);
            rippleRequest.Set(true);
        }

        public Transform Transform()
        {
            return _transform;
        }

        private void HandleMovement(float delta)
        {
            objectMover.AddPosition(_input.value * (actorStats.MovementSpeed * delta));
        }
    }
}