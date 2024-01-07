using UnityEngine;

namespace GameName.Gameplay.Combat
{
    public interface IDamageReceiver
    {
        void TakeDamage(int amount);
        Transform Transform();
    }
}