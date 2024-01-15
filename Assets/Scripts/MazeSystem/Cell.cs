using System.Linq;
using UnityEngine;
using static GameName.LoggingSystem.CLogger;

namespace GameName.MazeSystem
{
    public sealed class Cell : MonoBehaviour
    {
        [SerializeField]
        private Wall[] cellWalls;
        
        private CellState _currentState = CellState.NotVisited;

        public void OnValidate()
        {
            cellWalls = GetComponentsInChildren<Wall>();
            bool containsAllTypes = false;

            containsAllTypes = cellWalls.Any(w => w.WallType == WallType.PositiveX);
            containsAllTypes = containsAllTypes && cellWalls.Any(w => w.WallType == WallType.NegativeX);
            containsAllTypes = containsAllTypes && cellWalls.Any(w => w.WallType == WallType.PositiveZ);
            containsAllTypes = containsAllTypes && cellWalls.Any(w => w.WallType == WallType.NegativeZ);

            if (!containsAllTypes)
            {
                LogWarning("Check cell wall types, make sure cellWalls contains all directions");
                return;
            } 

            bool noDuplicates = true;
            
            noDuplicates = cellWalls.Count(w => w.WallType == WallType.PositiveX) == 1;
            noDuplicates &= cellWalls.Count(w => w.WallType == WallType.NegativeX) == 1;
            noDuplicates &= cellWalls.Count(w => w.WallType == WallType.PositiveZ) == 1;
            noDuplicates &= cellWalls.Count(w => w.WallType == WallType.NegativeZ) == 1;
            
            if (!noDuplicates)
            {
                LogWarning("Check wall types, make sure there is no duplicates");
            } 
        }

        public void RemoveWall(WallType wallTypeToRemove)
        {
            Wall wallToRemove = cellWalls.First(w => w.WallType == wallTypeToRemove);
            wallToRemove.gameObject.SetActive(false);
        }
        
        public void UpdateState(CellState newState)
        {
            _currentState = newState;
        }
    }
}