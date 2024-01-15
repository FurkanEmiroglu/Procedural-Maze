using GameName.MazeSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameName.UI
{
    public class SeedView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI tmp;

        [Inject]
        private void Construct(LevelGenerator.LevelGeneratorSettings set)
        {
            tmp.text = set.seed.ToString();
        }
    }
}