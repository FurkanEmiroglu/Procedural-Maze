using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace GameName.Gameplay.Combat
{
    public abstract class AIBehaviour
    {
        protected WaitForFixedUpdate waitCache;
        protected NavMeshAgent agent;

        public bool behaviourFlag;
        
        private const float Threshold = 0.3f;
        
        public abstract IEnumerator BehaviourMode(Action onWaypointArrival = null);
        
        protected IEnumerator SetDestination(Vector3 p, Action onArrival = null)
        {
            agent.SetDestination(p);
            yield return waitCache;
            while (!CheckComplete())
            {
                yield return waitCache;
            }
        
            onArrival?.Invoke();
        }
        
        private bool CheckComplete()
        {
            return agent.remainingDistance < agent.stoppingDistance + Threshold;
        }
    }
}