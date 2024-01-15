using UnityEngine;
using Zenject;

namespace GameName.Gameplay.Movement
{
    public class PlayerInput : ITickable
    {
        public Vector3 Value { get; private set; }

        public void Tick()
        {
            Value = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Value.Normalize();
        }
    }
}