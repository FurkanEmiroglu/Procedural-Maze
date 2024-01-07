using FE.ObjectPool;
using UnityEngine;

namespace GameName.Gameplay.Combat
{
    public sealed class RangedUnit : MonoBehaviour
    {
        [SerializeField]
        private LayerMask targetLayer;

        [SerializeField]
        private Projectile projectilePrefab;

        [SerializeField]
        private RangedUnitData unit;

        private Transform _transform;
        
        // attack
        private IDamageReceiver m_currentTarget;
        private readonly Collider[] m_output = new Collider[1];
        private ObjectPool<Projectile> m_projectilePool;

        private float _attackTimer;

        private void OnDrawGizmos()
        {
            if (unit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(((Component)this).transform.position, unit.AttackRange);   
            }
        }

        private void Awake()
        {
            _transform = ((Component)this).transform;
            _attackTimer = CalculateCooldown();
            m_projectilePool = new ObjectPool<Projectile>(projectilePrefab, 5, _transform);
        }

        private void Update()
        {
            float cooldown = CalculateCooldown();

            var sensor = GetComponent<Sensor>();

            if (sensor.HasTarget(out IDamageReceiver receiver)) SetTarget(receiver);
            else m_currentTarget = null;
            
            if (m_currentTarget != null && CheckDistance())
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
            m_currentTarget = target;
        }

        private void Attack()
        {
            ProjectileData data = new (_transform, m_currentTarget, unit);
            Projectile projectile = m_projectilePool.Get();
            projectile.Initialize(data, m_projectilePool);
        }
        
        private float CalculateCooldown()
        {
            return 60f / unit.AttackPerMinute;
        }

        private bool CheckDistance()
        {
            float distance = Vector3.Distance(_transform.position, m_currentTarget.Transform().position);
            
            if (distance < unit.AttackRange) return true;
            m_currentTarget = null;
            return false;
        }
    }
}