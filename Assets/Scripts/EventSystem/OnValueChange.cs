using System;

namespace GameName.SOInjection
{
    public class OnValueChange<T>
    {
        private event Action<T> OnChange;

        public void AddListener(Action<T> response)
        {
            OnChange += response;
        }
            
        public void RemoveListener(Action<T> response)
        {
            OnChange -= response;
        }

        public void Raise(T param)
        {
            OnChange?.Invoke(param);
        }
    }
}