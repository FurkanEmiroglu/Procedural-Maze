using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace GameName.Gameplay.Combat
{
    public class ChaseBehaviour : AIBehaviour
    {
        private readonly IDamageReceiver _target;

        public ChaseBehaviour(NavMeshAgent a, IDamageReceiver target)
        {
            agent = a;
            _target = target;
            waitCache = new WaitForFixedUpdate();
        }

        public override IEnumerator BehaviourMode(Action onWaypointArrival = null)
        {
            while (true)
            {
               yield return SetDestination(_target.Transform().position, onWaypointArrival);
            }
        }
    }
}