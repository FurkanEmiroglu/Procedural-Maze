using System.Collections;
using System.Collections.Generic;
using GameName.Gameplay;
using GameName.Gameplay.Combat;
using GameName.SOInjection;

namespace GameName.MazeSystem
{
    using UnityEngine;

    public sealed class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Vector3 mazeScale;
        
        [SerializeField]
        private Cell cellPrefab;

        [SerializeField] 
        private Waypoint endPointPrefab;

        [SerializeField]
        private Vector2Int mazeGridSize;

        [SerializeField]
        private Vector2 cellPrefabScale;
        
        // level generatior/manager should spawn spawn manager and spawn manager itself should handle player/enemies
        // also consider marking these prefabs as addressable
        [SerializeField] 
        private PlayerActor playerPrefab;  

        [SerializeField] 
        private EnemyActor enemyPrefab;
        
        [SerializeField] 
        private InjectedInt seed;

        [SerializeField]
        private bool useRandomSeed;

        [SerializeField] [Tooltip("The minimum distance between start and end points.")] 
        private float minDistanceThreshold;

        private int enemyCount = 5;
        
        private System.Random _rng;
        
#if UNITY_EDITOR
        public bool debugMode;
#endif

        private Transform _transform;

        private List<Cell> _cells;

        private void Awake()
        {
            _transform = transform;
            _cells = new List<Cell>(mazeGridSize.x * mazeGridSize.y); // prevent memory fragmentation
        }

        private void Start()
        {
            if (useRandomSeed) seed.Set(Random.Range(0,1000));
            else seed.OnValueChange.Raise(seed.Get());
            
            // YOU MAY NOT LIKE HASH-IF CHECKS THIS OFTEN FOR UNITY EDITOR.
            // its just my preference.
#if UNITY_EDITOR
            if (debugMode)
            {
                StartCoroutine(GenerateMazeEDITOR(mazeGridSize));
            }
            else
            {
                GenerateMaze(mazeGridSize);
                SpawnEnemies();
            }
#else
            GenerateMaze(mazeGridSize);
            SpawnEnemies();
#endif
        }

        private void SetEndPoints()
        {
            Vector3 startPointPosition;
            Vector3 endPointPosition;
            do
            {
                startPointPosition = _cells[GetRandom(0, _cells.Count)].transform.position;
                endPointPosition = _cells[GetRandom(0, _cells.Count)].transform.position;
                
            } while (Vector3.Distance(startPointPosition, endPointPosition) < minDistanceThreshold);

            startPointPosition.y = 100f;

            Instantiate(playerPrefab).transform.position = startPointPosition;
            Instantiate(endPointPrefab).transform.position = endPointPosition;
        }

        // this method will only be called at start, so i don't worry that much about the GC.
        // i also hate lists and generally prefer hashsets, stacks and queues. but currently i'm on a timer.
        private void GenerateMaze(Vector2Int size)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    Vector3 cellPosition = new (x * cellPrefabScale.x - (size.x / 2f), 0, y * cellPrefabScale.y - (size.y / 2f));
                    Cell cellInstance = Instantiate(cellPrefab, cellPosition, Quaternion.identity, _transform);
                    _cells.Add(cellInstance);
                    
#if UNITY_EDITOR 
                    cellInstance.levelGenerator = this; 
#endif
                }
            }

            List<Cell> activeCells = new List<Cell>();
            List<Cell> visitedCells = new List<Cell>();
            
            // choose starting cell
            //activeCells.Add(_cells[Random.Range(0, _cells.Count)]);
            activeCells.Add(_cells[GetRandom(0, _cells.Count)]);
            activeCells[0].UpdateState(CellState.Active);


            while (visitedCells.Count < _cells.Count)
            {
                List<int> possibleNextCells = new List<int>();
                List<WallType> possibleDirections = new List<WallType>();

                int activeNodeIndex = _cells.IndexOf(activeCells[^1]);
                int activeNodeX = activeNodeIndex / size.y;
                int activeNodeY = activeNodeIndex % size.x;

                if (activeNodeX < size.x - 1)
                {
                    if (!visitedCells.Contains(_cells[activeNodeIndex + size.y]) && !activeCells.Contains(_cells[activeNodeIndex + size.y]))
                    {
                        possibleDirections.Add(WallType.PositiveX);
                        possibleNextCells.Add(activeNodeIndex + size.y);
                    }
                }

                if (activeNodeX > 0)
                {
                    if (!visitedCells.Contains(_cells[activeNodeIndex - size.y]) && !activeCells.Contains(_cells[activeNodeIndex - size.y]))
                    {
                        possibleDirections.Add(WallType.NegativeX);
                        possibleNextCells.Add(activeNodeIndex - size.y);
                    }
                }

                if (activeNodeY < size.y - 1)
                {
                    if (!visitedCells.Contains(_cells[activeNodeIndex + 1]) && !activeCells.Contains(_cells[activeNodeIndex + 1]))
                    {
                        possibleDirections.Add(WallType.PositiveZ);
                        possibleNextCells.Add(activeNodeIndex + 1);
                    }
                }

                if (activeNodeY > 0)
                {
                    if (!visitedCells.Contains(_cells[activeNodeIndex - 1]) && !activeCells.Contains(_cells[activeNodeIndex - 1]))
                    {
                        possibleDirections.Add(WallType.NegativeZ);
                        possibleNextCells.Add(activeNodeIndex - 1);
                    }
                }

                if (possibleDirections.Count > 0)
                {
                    // int direction = Random.Range(0, possibleDirections.Count);
                    int direction = GetRandom(0, possibleDirections.Count);
                    Cell cell = _cells[possibleNextCells[direction]];

                    switch (possibleDirections[direction])
                    {
                        case WallType.PositiveX:
                            cell.RemoveWall(WallType.NegativeX);
                            activeCells[^1].RemoveWall(WallType.PositiveX);
                            break;
                        case WallType.NegativeX:
                            cell.RemoveWall(WallType.PositiveX);
                            activeCells[^1].RemoveWall(WallType.NegativeX);
                            break;
                        case WallType.PositiveZ:
                            cell.RemoveWall(WallType.NegativeZ);
                            activeCells[^1].RemoveWall(WallType.PositiveZ);
                            break;
                        case WallType.NegativeZ:
                            cell.RemoveWall(WallType.PositiveZ);
                            activeCells[^1].RemoveWall(WallType.NegativeZ);
                            break;
                    }
                    
                    activeCells.Add(cell);
                    cell.UpdateState(CellState.Active);
                }
                else
                {
                    visitedCells.Add(activeCells[^1]);
                    activeCells[^1].UpdateState(CellState.Visited);
                    activeCells.RemoveAt(activeCells.Count - 1);
                }
            }

            SetEndPoints();
            
            _transform.localScale = mazeScale;
        }
     
#if UNITY_EDITOR
        // this method will only be called at start, so i don't worry that much about the GC.
        // i also hate lists and generally prefer hashsets, stacks and queues. but currently i'm on a timer.
        private IEnumerator GenerateMazeEDITOR(Vector2Int size)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    Vector3 cellPosition = new (x * cellPrefabScale.x - (size.x / 2f), 0, y * cellPrefabScale.y - (size.y / 2f));
                    Cell cellInstance = Instantiate(cellPrefab, cellPosition, Quaternion.identity, _transform);
                    cellInstance.levelGenerator = this;
                    _cells.Add(cellInstance);
                    yield return null;
                }
            }

            List<Cell> activeCells = new List<Cell>();
            List<Cell> visitedCells = new List<Cell>();
            
            // choose starting cell
            // activeCells.Add(_cells[Random.Range(0, _cells.Count)]);
            activeCells.Add(_cells[GetRandom(0, _cells.Count)]);
            activeCells[0].UpdateState(CellState.Active);


            while (visitedCells.Count < _cells.Count)
            {
                List<int> possibleNextCells = new List<int>();
                List<WallType> possibleDirections = new List<WallType>();

                int activeNodeIndex = _cells.IndexOf(activeCells[^1]);
                int activeNodeX = activeNodeIndex / size.y;
                int activeNodeY = activeNodeIndex % size.x;

                if (activeNodeX < size.x - 1)
                {
                    if (!visitedCells.Contains(_cells[activeNodeIndex + size.y]) && !activeCells.Contains(_cells[activeNodeIndex + size.y]))
                    {
                        possibleDirections.Add(WallType.PositiveX);
                        possibleNextCells.Add(activeNodeIndex + size.y);
                    }
                }

                if (activeNodeX > 0)
                {
                    if (!visitedCells.Contains(_cells[activeNodeIndex - size.y]) && !activeCells.Contains(_cells[activeNodeIndex - size.y]))
                    {
                        possibleDirections.Add(WallType.NegativeX);
                        possibleNextCells.Add(activeNodeIndex - size.y);
                    }
                }

                if (activeNodeY < size.y - 1)
                {
                    if (!visitedCells.Contains(_cells[activeNodeIndex + 1]) && !activeCells.Contains(_cells[activeNodeIndex + 1]))
                    {
                        possibleDirections.Add(WallType.PositiveZ);
                        possibleNextCells.Add(activeNodeIndex + 1);
                    }
                }

                if (activeNodeY > 0)
                {
                    if (!visitedCells.Contains(_cells[activeNodeIndex - 1]) && !activeCells.Contains(_cells[activeNodeIndex - 1]))
                    {
                        possibleDirections.Add(WallType.NegativeZ);
                        possibleNextCells.Add(activeNodeIndex - 1);
                    }
                }

                if (possibleDirections.Count > 0)
                {
                    // int direction = Random.Range(0, possibleDirections.Count);
                    int direction = GetRandom(0, possibleDirections.Count);
                    Cell cell = _cells[possibleNextCells[direction]];

                    switch (possibleDirections[direction])
                    {
                        case WallType.PositiveX:
                            cell.RemoveWall(WallType.NegativeX);
                            activeCells[^1].RemoveWall(WallType.PositiveX);
                            break;
                        case WallType.NegativeX:
                            cell.RemoveWall(WallType.PositiveX);
                            activeCells[^1].RemoveWall(WallType.NegativeX);
                            break;
                        case WallType.PositiveZ:
                            cell.RemoveWall(WallType.NegativeZ);
                            activeCells[^1].RemoveWall(WallType.PositiveZ);
                            break;
                        case WallType.NegativeZ:
                            cell.RemoveWall(WallType.PositiveZ);
                            activeCells[^1].RemoveWall(WallType.NegativeZ);
                            break;
                    }
                    
                    activeCells.Add(cell);
                    cell.UpdateState(CellState.Active);
                }
                else
                {
                    visitedCells.Add(activeCells[^1]);
                    activeCells[^1].UpdateState(CellState.Visited);
                    activeCells.RemoveAt(activeCells.Count - 1);
                }
                yield return null;
            }
            yield return null;
            
            SetEndPoints();
            _transform.localScale = mazeScale;
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < enemyCount; i++)
            {
                var enemy = Instantiate(enemyPrefab, GetRandomCellPosition(), Quaternion.identity);
                AssignRandomPatrolRoute(enemy);
            }
            
            void AssignRandomPatrolRoute(EnemyActor actor)
            {
                int routeLength = 5;
                actor.Waypoints = new Vector3[routeLength];

                for (int i = 0; i < routeLength; i++)
                {
                    actor.Waypoints[i] = GetRandomCellPosition();
                }
            }
            
            Vector3 GetRandomCellPosition()
            {
                int randomCellIndex = GetRandom(0, _cells.Count);
                Vector3 randomCellPosition = _cells[randomCellIndex].transform.position;
                randomCellPosition.y = 1f;
                return randomCellPosition;
            }
        }
        
        private int GetRandom(int minValue, int maxValue)
        {
            if (_rng == null)
            {
                _rng = new System.Random(seed.Get());
                return _rng.Next(minValue, maxValue);
            }

            return _rng.Next(minValue, maxValue);
        }
    }
#endif
}