using System;
using GameName.Gameplay;
using GameName.LoggingSystem;
using UnityEngine;
using Zenject;

namespace GameName.HealthSystem
{
    public class Health
    {
        public int Value => _value;
        public bool IsDead { get; private set; }

        public event Action OnDeath;
        public event Action<int> OnValueChange;

        [Inject]
        private LevelEndSignal _signal;

        private int _value;
        private int _maxValue;
        
        public Health(int value)
        {
            _value = value;
            _maxValue = int.MaxValue;
        }

        public Health(int value, int maxValue)
        {
            _value = value;
            _maxValue = maxValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Amount to add, must be a positive value</param>
        public void Add(int amount)
        {
            if (amount < 0)
            {
                CLogger.LogWarning("You can't 'ADD' negative values");
                return;
            }

            _value += amount;
            _value = Mathf.Clamp(_value, -1, _maxValue);
            OnValueChange?.Invoke(_value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Amount to remove, must be a positive value</param>
        public void Remove(int amount)
        {
            if (IsDead)
            {
                CLogger.LogWarning("Health is already depleted"); // need to fix multiple remove calls
                return;
            }
            
            if (amount < 0)
            {
                CLogger.LogWarning("You can't 'REMOVE' negative values");
                return;
            }
            
            _value -= amount;

            if (_value <= 0)
            {
                IsDead = true;
                OnDeath?.Invoke();
                _signal.Raise(false);
            }
            else
            {
                OnValueChange?.Invoke(_value);
            }
        }
    }
}