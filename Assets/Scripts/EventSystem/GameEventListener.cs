using UnityEngine;
using UnityEngine.Events;

namespace GameName.SOInjection
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private ListeningType listeningType;
        [SerializeField] private GameEvent @event;
        [SerializeField] private UnityEvent response;

        private void Awake()
        {
            if (listeningType == ListeningType.AwakeDestroy)
            {
                @event.AddListener(InvokeResponse);
            }
        }

        private void OnEnable()
        {
            if (listeningType == ListeningType.EnableDisable)
            {
                @event.AddListener(InvokeResponse);
            }
        }

        private void OnDisable()
        {
            if (listeningType == ListeningType.EnableDisable)
            {
                @event.RemoveListener(InvokeResponse);
            }
        }

        private void OnDestroy()
        {
            if (listeningType == ListeningType.AwakeDestroy)
            {
                @event.RemoveListener(InvokeResponse);
            }
        }

        private void InvokeResponse()
        {
            response?.Invoke();
        }

        private enum ListeningType
        {
            AwakeDestroy,
            EnableDisable
        }
    }
}