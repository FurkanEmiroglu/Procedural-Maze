using UnityEngine;

namespace GameName.SOInjection
{
    [CreateAssetMenu(menuName = "Injections/Variables/Bool")]
    public class InjectedBool : ScriptableObject
    {
        [SerializeField] 
        private bool value;
        
        public OnValueChange<bool> OnValueChange
        {
            get
            {
                if (_onValueChange == null) return _onValueChange = new OnValueChange<bool>();
                return _onValueChange;
            }
        }

        private OnValueChange<bool> _onValueChange;

        private void OnValidate()
        {
            OnValueChange.Raise(value);
        }

        public void Set(bool val)
        {
            value = val;
            OnValueChange.Raise(value);
        }

        public bool Get()
        {
            return value;
        }
    }
}