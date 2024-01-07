using GameName.SOInjection;
using TMPro;
using UnityEngine;

namespace GameName.UI
{
    public class SeedView : MonoBehaviour
    {
        [SerializeField] 
        private InjectedInt seed;
        
        [SerializeField] 
        private TextMeshProUGUI tmp;

        private void Awake()
        {
            seed.OnValueChange.AddListener(UpdateValue);
        }

        private void OnDestroy()
        {
            seed.OnValueChange.RemoveListener(UpdateValue);
        }

        private void UpdateValue(int value)
        {
            tmp.text = value.ToString();
        }
    }
}