using UnityEngine;
using Zenject;

namespace GameName.Gameplay.Movement
{
    public class SimpleCameraController : MonoBehaviour 
    {
        [System.Serializable]
        public class CameraSettings
        {
            public Vector3 followOffset;
        }
        
        private CameraSettings _settings;
        private PositionData _positionData;
        private Transform _transform;
        
        [Inject]
        private void Construct(PositionData positionData, CameraSettings set)
        {
            _settings = set;
            _positionData = positionData;
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            Follow(_positionData.Get());
        }

        private void Follow(Vector3 p)
        {
            _transform.position = p + _settings.followOffset;
        }
    }
}