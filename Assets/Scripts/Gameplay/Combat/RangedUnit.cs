using FE.ObjectPool;
using UnityEngine;
using Zenject;

namespace GameName.Gameplay.Combat
{
    public sealed class RangedUnit : MonoBehaviour
    {
        [SerializeField]
        private Projectile projectilePrefab;

        private Transform _transform;
        
        // attack
        private IDamageReceiver _currentTarget;
        private RangedUnitData _unit;
        private ObjectPool<Projectile> _projectilePool;

        private float _attackTimer;

        private void OnDrawGizmos()
        {
            if (_unit != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(((Component)this).transform.position, _unit.AttackRange);   
            }
        }

        [Inject]
        private void Construct(RangedUnitData unit)
        {
            _unit = unit;
        }

        private void Awake()
        {
            _transform = (this).transform;
            _attackTimer = CalculateCooldown();
            _projectilePool = new ObjectPool<Projectile>(projectilePrefab, 5, _transform);
        }

        private void Update()
        {
            float cooldown = CalculateCooldown();

            var sensor = GetComponent<Sensor>();

            if (sensor.HasTarget(out IDamageReceiver receiver)) SetTarget(receiver);
            else _currentTarget = null;
            
            if (_currentTarget != null && CheckDistance())
            {
                if (_attackTimer > cooldown)
                {
                    Attack();
                    _attackTimer = 0;
                }
                
                _attackTimer += Time.deltaTime;
            }
        }
        
        private void SetTarget(IDamageReceiver target)
        {
            _currentTarget = target;
        }

        private void Attack()
        {
            ProjectileData data = new (_transform, _currentTarget, _unit);
            Projectile projectile = _projectilePool.Get();
            projectile.Initialize(data, _projectilePool);
        }
        
        private float CalculateCooldown()
        {
            return 60f / _unit.AttackPerMinute;
        }

        private bool CheckDistance()
        {
            float distance = Vector3.Distance(_transform.position, _currentTarget.Transform().position);
            
            if (distance < _unit.AttackRange) return true;
            _currentTarget = null;
            return false;
        }
    }
}