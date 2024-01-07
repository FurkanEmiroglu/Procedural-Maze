using UnityEngine;

namespace GameName.Gameplay.Combat
{
    public struct ProjectileData
    {
        public readonly IDamageReceiver target;
        
        public readonly Transform OriginTransform;
        public readonly Transform TargetTransform;
        
        public readonly float TravelSpeed;
        public readonly float DamageAmount;
        public readonly float HeightFactor;

        public ProjectileData(Transform originTransform, IDamageReceiver target, RangedUnitData unit)
        {
            OriginTransform = originTransform;
            TargetTransform = target.Transform();
            this.target = target;

            TravelSpeed = unit.ProjectileSpeed;
            DamageAmount = unit.DamageAmount;
            HeightFactor = unit.HeightFactor;
        }
    } 
}