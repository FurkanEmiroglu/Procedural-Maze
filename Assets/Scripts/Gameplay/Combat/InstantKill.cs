using UnityEngine;

namespace GameName.Gameplay.Combat
{
    [RequireComponent(typeof(Sensor))]
    public class InstantKill : MonoBehaviour
    {
        [SerializeField]
        private Sensor sensor;

        private void FixedUpdate()
        {
            if (sensor.HasTarget(out IDamageReceiver receiver))
                receiver.TakeDamage(int.MaxValue);
        }
    }
}