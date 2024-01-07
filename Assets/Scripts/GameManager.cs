using System;
using GameName.MazeSystem;
using GameName.SOInjection;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] 
        private LevelGenerator levelGenerator;
        
        [SerializeField] 
        private InjectedInt playerHealth;

        [SerializeField] 
        private GameEvent levelOver;

        [SerializeField] 
        private GameEvent levelSuccess;

        private void Awake()
        {
            playerHealth.OnValueChange.AddListener(CheckLevelOver);
        }

        private void OnDestroy()
        {
            playerHealth.OnValueChange.RemoveListener(CheckLevelOver);
        }

        private void Start()
        {
            Instantiate(levelGenerator, transform);
        }

        private void CheckLevelOver(int health)
        {
            if (health <= 0)
            {
                GameOver();
            }
        }
        
        private void GameSuccess()
        {
            levelSuccess.Raise();
        }
        
        private void GameOver()
        {
            levelOver.Raise();
        }
    }
}