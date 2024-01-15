using System;
using Zenject;

namespace GameName.Gameplay
{
    public sealed class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelEndSignal>().AsSingle().NonLazy();
        }
    }

    public class CustomSignal
    {
        private event Action @event;
        
        public void AddListener(Action a)
        {
            @event += a;
        }

        public void RemoveListener(Action a)
        {
            @event -= a;
        }

        public void Raise()
        {
            @event?.Invoke();
        }
    }
    
    public class CustomSignal<T>
    {
        private event Action<T> @event;
        
        public void AddListener(Action<T> a)
        {
            @event += a;
        }

        public void RemoveListener(Action<T> a)
        {
            @event -= a;
        }

        public void Raise(T parameter)
        {
            @event?.Invoke(parameter);
        }
    }

    public class LevelEndSignal : CustomSignal<bool>
    {
        
    }
}