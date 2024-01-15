using GameName.HealthSystem;
using UnityEngine;
using Zenject;

namespace GameName.Gameplay.Combat
{
    public sealed class RippleEffectOnDamage
    {
        private RipplePostProcessor _rippleProcessor;
        private Health _playerHealth;
        
        [Inject]
        private void Construct(Health playerHealth, RipplePostProcessor ripplePostProcessor)
        {
            _rippleProcessor = ripplePostProcessor;
            _playerHealth = playerHealth;
            _playerHealth.OnValueChange += Request;
        }

        ~RippleEffectOnDamage()
        {
            _playerHealth.OnValueChange -= Request;
        }

        private void Request(int _)
        {
            _rippleProcessor.RequestRipple();
        }
        
        public void Initialize()
        {
            
        }
    }
}