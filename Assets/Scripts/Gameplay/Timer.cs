using System;
using UnityEngine;
using Zenject;

namespace GameName.Gameplay
{
    public sealed class Timer : ITickable
    {
        public float SecondsLeft => _secondsLeft;
        
        private LevelEndSignal _levelEndSignal;
        
        private float _secondsLeft;
        private bool _timerEnd;

        [Inject]
        private void Construct(LevelEndSignal levelEndSig, float successTime)
        {
            _levelEndSignal = levelEndSig;
            _secondsLeft = successTime;
        }
        
        public void Tick()
        {
            if (_timerEnd) return;
            
            _secondsLeft -= Time.deltaTime;

            if (_secondsLeft <= 0)
            {
                _levelEndSignal.Raise(true);
                Time.timeScale = 0f;
                _timerEnd = true;
            }
        }
    }
}