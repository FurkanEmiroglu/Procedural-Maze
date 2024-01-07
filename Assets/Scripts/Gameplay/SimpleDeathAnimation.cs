using System;
using DG.Tweening;
using GameName.SOInjection;
using UnityEngine;

namespace GameName.Gameplay
{
    // play some smoke particles here too.
    public class SimpleDeathAnimation : MonoBehaviour
    {
        [SerializeField] 
        private GameEvent gameLose;
        
        [SerializeField] 
        private float scaleDuration;

        [SerializeField] 
        private Ease ease;

        private void OnEnable()
        {
            gameLose.AddListener(Animate);
        }

        private void OnDestroy()
        {
            gameLose.RemoveListener(Animate);
        }

        private void Animate()
        {
            transform.DOScale(0, scaleDuration).SetEase(ease).onComplete += DisableObject;
        }
        
        private void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}