using UnityEngine;

namespace GameName.Gameplay.Combat
{
    public class Sensor : MonoBehaviour
    {
        [SerializeField] 
        private LayerMask targetLayer;

        [SerializeField] 
        private LayerMask sightBlocker;
        
        [SerializeField]
        private float distance;

        private Collider[] _cache;
        private IDamageReceiver _targetReceiver;
        private bool _hasTarget;
        
        private Transform _transform;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distance);
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
            int count = Physics.OverlapSphereNonAlloc(_transform.position, distance, _cache, targetLayer);

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
            return !Physics.Raycast(ray, distance, sightBlocker);
        }
    }
}