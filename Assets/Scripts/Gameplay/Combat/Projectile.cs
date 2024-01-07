using FE.ObjectPool;
using UnityEngine;

namespace GameName.Gameplay.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Projectile : MonoBehaviour
    {
        private Rigidbody _rb;

        private ProjectileData _data;
        private QuadraticCurveGenerator _curveGen;
        
        private float _airTimer;
        private bool _isInitialized;

        private ObjectPool<Projectile> _pool;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _rb = GetComponent<Rigidbody>();
            _curveGen = new QuadraticCurveGenerator();
        }

        public void Initialize(ProjectileData data, ObjectPool<Projectile> pool)
        {
            _pool = pool;
            _data = data;
            _airTimer = 0f;
            _isInitialized = true;

            transform.SetParent(null);
            gameObject.SetActive(true);

            _rb.MovePosition(data.OriginTransform.position);
            _curveGen.UpdateGenerator(_data.OriginTransform, _data.TargetTransform, _data.HeightFactor);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageReceiver damageable))
            {
                damageable.TakeDamage((int)_data.DamageAmount);
                ReturnTrigger();
            }
        }
        
        private void FixedUpdate()
        {
            if (_isInitialized)
            {
                float distance = Vector3.Distance(_transform.position, _data.TargetTransform.position);
                
                _airTimer += Time.fixedDeltaTime * _data.TravelSpeed / distance; 
                
                _transform.position = _curveGen.Evaluate(_airTimer);
                _transform.forward = _curveGen.Evaluate(_airTimer + 0.001f) - transform.position;
            }
        }

        private void ReturnTrigger()
        {
            _isInitialized = false;
            gameObject.SetActive(false);
            _pool.Return(this);
        } 
    } 
}