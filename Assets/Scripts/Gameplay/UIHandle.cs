using UnityEngine;
using Zenject;

namespace GameName.Gameplay
{
    public sealed class UIHandle : MonoBehaviour
    {
        [SerializeField]
        private RectTransform winScreen;

        [SerializeField]
        private RectTransform loseScreen;

        private CustomSignal<bool> _levelEndSignal;

        [Inject]
        private void Construct(LevelEndSignal signal)
        {
            _levelEndSignal = signal;
        }

        private void Awake()
        {
            _levelEndSignal.AddListener(OnLevelEnd);
        }

        private void OnDestroy()
        {
            _levelEndSignal.RemoveListener(OnLevelEnd);
        }

        private void OnLevelEnd(bool isSuccess)
        {
            winScreen.gameObject.SetActive(isSuccess);
            loseScreen.gameObject.SetActive(!isSuccess);
        }
    }
}