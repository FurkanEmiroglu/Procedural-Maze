using UnityEngine;

namespace GameName.Gameplay.Combat
{
    [System.Serializable]
    public class RangedUnitData
    {
        [field: SerializeField] public float AttackRange { get; set; }
        [field: SerializeField] public int AttackPerMinute { get; set; }
        [field: SerializeField] public float DamageAmount { get; set; }
        [field: SerializeField] public float ProjectileSpeed { get; set; }
        [field: SerializeField] public float HeightFactor { get; set; }
    } 
}