using GameName.HealthSystem;
using UnityEngine;
using Zenject;

namespace GameName.Gameplay
{
    public sealed class Timer : ITickable
    {
        public float SecondsLeft => _secondsLeft;
        
        private LevelEndSignal _levelEndSignal;
        private Health _playerHealth;
        
        private float _secondsLeft;
        private bool _timerEnd;

        [Inject]
        private void Construct(LevelEndSignal levelEndSig, Health playerHealth, float successTime)
        {
            _levelEndSignal = levelEndSig;
            _secondsLeft = successTime;
            _playerHealth = playerHealth;
        }
        
        public void Tick()
        {
            if (_timerEnd) return;
            
            _secondsLeft -= Time.deltaTime;

            if (_secondsLeft <= 0 && !_playerHealth.IsDead)
            {
                _levelEndSignal.Raise(true);
                Time.timeScale = 0f;
                _timerEnd = true;
            }
        }
    }
}