using System.Globalization;
using GameName.Gameplay;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameName.UI
{
    public sealed class TimeView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI viewText;
        
        private Timer _timer;
        
        [Inject]
        private void Construct(Timer timer)
        {
            _timer = timer;
        }

        private void Update()
        {
            viewText.text = _timer.SecondsLeft.ToString("F1", CultureInfo.InvariantCulture);
        }
    }
}