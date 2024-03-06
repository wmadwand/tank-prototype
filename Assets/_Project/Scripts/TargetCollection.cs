using System.Collections;
using System.Collections.Generic;
using TestingTask.Combat;
using UnityEngine;

namespace TestingTask.Target.Collection
{
    public class TargetCollection : MonoBehaviour
    {
        private List<ITargetable> _targets;

        private void Awake()
        {
            _targets = new List<ITargetable>();
            //TODO: use MVP + enemy factory + object pool + HashTables for the cache, etc 
            GetComponentsInChildren(false, _targets);
        }

        //TODO: greedy algorithm - good for prototype but too expensive for real game
        // alternative - UnityEngine.Physics.SphereCastAll but expensive again
        public ITargetable GetClosest(Vector3 fromPoint, float targetingRange)
        {
            if (_targets.Count < 1)
            {
                //CurrentTarget = null;
                return null;
            }

            ITargetable closestTarget = null;
            var minDistance = float.MaxValue;
            for (int i = 0; i < _targets.Count; i++)
            {
                if (_targets[i] == null)
                {
                    continue;
                }

                var tempDistance = (_targets[i].GetPosition() - fromPoint).sqrMagnitude;
                if (tempDistance < targetingRange * targetingRange && tempDistance < minDistance)
                {
                    closestTarget = _targets[i];
                    minDistance = tempDistance;
                }
            }

            return closestTarget;
        }

        public void Remove(ITargetable element)
        {
            _targets.Remove(element);
        }
    } 
}