using System;
using UnityEngine;

namespace GameName.SOInjection
{
    [CreateAssetMenu(menuName = "Injections/Events/Simple")]
    public class GameEvent : ScriptableObject
    {
        private event Action @event;

        public void AddListener(Action response)
        {
            @event += response;
        }

        public void RemoveListener(Action response)
        {
            @event -= response;
        }

        public void Raise()
        {
            @event?.Invoke();
        }
    }
}