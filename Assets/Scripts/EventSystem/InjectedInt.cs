using UnityEngine;

namespace GameName.SOInjection
{
    [CreateAssetMenu(menuName = "Injections/Variables/Int")]
    public class InjectedInt : ScriptableObject
    {
        [SerializeField]
        private int value;

        public OnValueChange<int> OnValueChange
        {
            get
            {
                if (_onValueChange == null) return _onValueChange = new OnValueChange<int>();
                return _onValueChange;
            }
        }

        private OnValueChange<int> _onValueChange;

        private void OnValidate()
        {
            OnValueChange.Raise(value);
        }

        public void Set(int val)
        {
            value = val;
            OnValueChange.Raise(value);
        }

        public int Get()
        {
            return value;
        }
    }
}