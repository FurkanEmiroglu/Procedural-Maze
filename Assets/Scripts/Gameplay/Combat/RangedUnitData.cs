using UnityEngine;

namespace GameName.Gameplay.Combat
{
    [CreateAssetMenu(menuName = "Combat/Ranged Unit Data")]
    public class RangedUnitData : ScriptableObject
    {
        [field: SerializeField] public float AttackRange { get; set; }
        [field: SerializeField] public int AttackPerMinute { get; set; }
        [field: SerializeField] public float DamageAmount { get; set; }
        [field: SerializeField] public float ProjectileSpeed { get; set; }
        [field: SerializeField] public float HeightFactor { get; set; }
    } 
}