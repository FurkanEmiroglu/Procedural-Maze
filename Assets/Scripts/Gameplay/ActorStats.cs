using UnityEngine;

namespace GameName.Gameplay
{
    [CreateAssetMenu(menuName = "Player/Actor Stats")]
    public class ActorStats : ScriptableObject
    {
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
    }
}