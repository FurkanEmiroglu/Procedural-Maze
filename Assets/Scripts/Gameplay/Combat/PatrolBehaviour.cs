using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace GameName.Gameplay.Combat
{
    public class PatrolBehaviour : AIBehaviour
    {
        private readonly Vector3[] _waypoints;

        private int _currentIndex;

        public PatrolBehaviour(NavMeshAgent a, Vector3[] waypoints)
        {
            waitCache = new WaitForFixedUpdate();
            agent = a;
            _waypoints = waypoints;
        }

        public override IEnumerator BehaviourMode(Action onWaypointArrival = null)
        {
            while (true)
            {
                Vector3 target = _waypoints[_currentIndex % _waypoints.Length];
                
                yield return SetDestination(target, onWaypointArrival);
                IncreaseIndex();
            }
        }

        private void IncreaseIndex()
        {
            _currentIndex++;
        }
    }
}