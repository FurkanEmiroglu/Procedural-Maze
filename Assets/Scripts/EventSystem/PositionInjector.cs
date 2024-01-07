using UnityEngine;

namespace GameName.SOInjection
{
    public class PositionInjector : MonoBehaviour
    {
        [SerializeField] 
        private InjectedVector3 target;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            target.Set(_transform.position);
        }
    }
}