using GameName.SOInjection;
using UnityEngine;

namespace GameName.Gameplay.Movement
{
    public class SimpleCameraController : MonoBehaviour 
    {
        [SerializeField] 
        private InjectedVector3 playerPosition;

        [SerializeField] 
        private Vector3 followOffset;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            playerPosition.OnValueChange.AddListener(Follow);
        }

        private void OnDisable()
        {
            playerPosition.OnValueChange.RemoveListener(Follow);
        }

        private void Follow(Vector3 p)
        {
            _transform.position = p + followOffset;
        }
    }
}