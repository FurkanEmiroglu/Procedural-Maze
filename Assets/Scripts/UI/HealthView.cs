using System.Collections;
using GameName.HealthSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameName.UI
{
    // just to show an example of my preference to approach UI
    public class HealthView : MonoBehaviour
    {
        [SerializeField] 
        private Image fillImage;

        private Health _health;

        [Inject]
        private void Construct(Health health)
        {
            _health = health;
        }

        private void OnEnable()
        {
            _health.OnValueChange += UpdateFill;
        }

        private void OnDisable()
        {
            _health.OnValueChange -= UpdateFill;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            UpdateFill(_health.Value);
        }

        private void UpdateFill(int value)
        {
            fillImage.fillAmount = value / 100f;
        }
    }
}