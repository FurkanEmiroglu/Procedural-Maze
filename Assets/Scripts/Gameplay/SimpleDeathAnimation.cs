using DG.Tweening;
using UnityEngine;
using Zenject;

namespace GameName.Gameplay
{
    // play some smoke particles here too.
    public class SimpleDeathAnimation : MonoBehaviour
    {
        [SerializeField] 
        private float scaleDuration;

        [SerializeField] 
        private Ease ease;

        private LevelEndSignal _levelEndSignal;

        [Inject]
        private void Construct(LevelEndSignal levelEndSignal)
        {
            _levelEndSignal = levelEndSignal;
            _levelEndSignal.AddListener(Animate);
        }

        private void OnDestroy()
        {
            _levelEndSignal.RemoveListener(Animate);
        }

        private void Animate(bool isSuccess)
        {
            if (isSuccess) return;
            transform.DOScale(0, scaleDuration).SetEase(ease).onComplete += DisableObject;
        }
        
        private void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}