using UnityEngine;

namespace GameName.SOInjection
{
    [CreateAssetMenu(menuName = "Injections/Variables/Float")]
    public class InjectedFloat : ScriptableObject
    {
        [SerializeField] 
        private float value;
        
        public OnValueChange<float> OnValueChange
        {
            get
            {
                if (_onValueChange == null) return _onValueChange = new OnValueChange<float>();
                return _onValueChange;
            }
        }

        private OnValueChange<float> _onValueChange;

        private void OnValidate()
        {
            OnValueChange.Raise(value);
        }

        public void Set(float val)
        {
            value = val;
            OnValueChange.Raise(value);
        }

        public float Get()
        {
            return value;
        }    
    }
}