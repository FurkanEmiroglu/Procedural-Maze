using UnityEngine;
using UnityEngine.AI;

namespace GameName.Gameplay.Combat
{
    // i like A* pathfinding project by Aron Granberg but i'm not confident enough with it yet.
    // so i'll try to use navmesh agents by unity
    public class EnemyActor : MonoBehaviour
    {
        [SerializeField] 
        private Sensor sensor;

        [SerializeField] 
        private NavMeshAgent agent;

        public Vector3[] Waypoints { get; set; }

        private Coroutine _currentRoutine;
        private AIBehaviour _currentBehaviour;

        private void OnValidate()
        {
            sensor = GetComponent<Sensor>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            EnterPatrolMode();
        }

        private void FixedUpdate()
        {
            EnemyBehaviour();
        }
        
        private void EnemyBehaviour()
        {
            if (sensor.HasTarget(out IDamageReceiver target))
            {
                if (_currentBehaviour is ChaseBehaviour { behaviourFlag: true }) return;
                EnterChaseMode(target);
            }
            else
            {
                if (_currentBehaviour is PatrolBehaviour { behaviourFlag: true }) return;
                EnterPatrolMode();
            }
        }

        private void EnterChaseMode(IDamageReceiver target)
        {
            OnNewBehaviour();
            _currentBehaviour = new ChaseBehaviour(agent, target);
            _currentBehaviour.behaviourFlag = true;
            _currentRoutine = StartCoroutine(_currentBehaviour.BehaviourMode());
        }

        private void EnterPatrolMode()
        {
            OnNewBehaviour();
            _currentBehaviour = new PatrolBehaviour(agent, Waypoints);
            _currentBehaviour.behaviourFlag = true;
            _currentRoutine = StartCoroutine(_currentBehaviour.BehaviourMode());
        }

        private void OnNewBehaviour()
        {
            StopCurrentBehaviourIfAny();
            EnsureNavmeshSurface();
            
            void StopCurrentBehaviourIfAny()
            {
                if (_currentRoutine != null)
                {
                    StopCoroutine(_currentRoutine);
                    _currentBehaviour.behaviourFlag = false;
                }
            }
            
            void EnsureNavmeshSurface()
            {
                Vector3 p = transform.position;
                p.y = 0f; // put this on a constant container class or check surface h with Physics.Raycast
                agent.Warp(p);
            }
        }
    }
}