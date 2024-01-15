using UnityEngine;
using Zenject;

namespace GameName.Gameplay.Combat
{
    public class Sensor : MonoBehaviour
    {
        [System.Serializable]
        public class SensorSettings
        {
            public LayerMask targetLayer;
            public LayerMask sightBlocker;
            public float distance;
        }

        public enum SensorType
        {
            RangeUnitDetection,
            InstantKill
        }

        [field: SerializeField] public SensorType SelectedType { get; private set; }

        private Collider[] _cache;
        private Transform _transform;
        private SensorSettings _settings;
        private IDamageReceiver _targetReceiver;
    
        private bool _hasTarget;

        [Inject]
        private void Construct(SensorSettings set)
        {
            _settings = set;
        }

        private void Awake()
        {
            _transform = transform;
            _cache = new Collider[1];
        }

        private void FixedUpdate()
        {
            UpdateSensor();
        }
        
        private void UpdateSensor()
        {
            if (CheckDistanceDetection(out IDamageReceiver receiver))
            {
                _hasTarget = true;
                _targetReceiver = receiver;
            }
            else
            {
                _hasTarget = false;
                _targetReceiver = null;
            }
        }

        public bool HasTarget(out IDamageReceiver target)
        {
            if (_hasTarget)
            {
                target = _targetReceiver;
                return true;
            }

            target = null;
            return false;
        }

        private bool CheckDistanceDetection(out IDamageReceiver detected)
        {
            int count = Physics.OverlapSphereNonAlloc(_transform.position, _settings.distance, _cache, 
                _settings.targetLayer);

            if (count > 0 && _cache[0].TryGetComponent(out IDamageReceiver receiver))
            {
                if (HasLineOfSight(receiver.Transform().position))
                {
                    detected = receiver;
                    return true;
                }
            }

            detected = null;
            return false;
        }

        private bool HasLineOfSight(Vector3 point)
        {
            Vector3 position = _transform.position;
            Ray ray = new Ray(position, point - position);
            return !Physics.Raycast(ray, _settings.distance, _settings.sightBlocker);
        }
    }
}