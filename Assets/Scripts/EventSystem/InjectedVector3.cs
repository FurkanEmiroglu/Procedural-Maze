using UnityEngine;

namespace GameName.SOInjection
{
    [CreateAssetMenu(menuName = "Injections/Variables/Vector3")]
    public class InjectedVector3 : ScriptableObject
    {
        [SerializeField] 
        private Vector3 value;
    
        public OnValueChange<Vector3> OnValueChange
        {
            get
            {
                if (_onValueChange == null) return _onValueChange = new OnValueChange<Vector3>();
                return _onValueChange;
            }
        }

        private OnValueChange<Vector3> _onValueChange;

        private void OnValidate()
        {
            OnValueChange.Raise(value);
        }

        public void Set(Vector3 val)
        {
            value = val;
            OnValueChange.Raise(value);
        }

        public Vector3 Get()
        {
            return value;
        }
    }
}