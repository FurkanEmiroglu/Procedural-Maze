using UnityEngine;

namespace GameName.Gameplay
{
    public class PositionData
    {
        private Vector3 _position;

        public void Set(Vector3 position)
        {
            _position = position;
        }

        public Vector3 Get()
        {
            return _position;
        }
    }
}