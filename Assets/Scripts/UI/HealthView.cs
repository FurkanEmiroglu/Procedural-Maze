using System.Collections;
using GameName.SOInjection;
using UnityEngine;
using UnityEngine.UI;

namespace GameName.UI
{
    // just to show an example of my preference to approach UI
    public class HealthView : MonoBehaviour
    {
        [SerializeField] 
        private Image fillImage;

        [SerializeField] 
        private InjectedInt health;

        private void OnEnable()
        {
            health.OnValueChange.AddListener(UpdateFill);
        }

        private void OnDisable()
        {
            health.OnValueChange.RemoveListener(UpdateFill);
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            UpdateFill(health.Get());
        }

        private void UpdateFill(int value)
        {
            fillImage.fillAmount = value / 100f;
        }
    }
}