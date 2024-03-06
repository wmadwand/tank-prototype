using System.Collections.Generic;
using TestingTask.Combat;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace TestingTask.Target.Collection
{
    public class TargetCollection : MonoBehaviour
    {
        private List<ITargetable> _targets;
        //TODO: probably the better place for it would be in PlayerTargeting class
        [SF] private float _targetingRange = 10;

        private Vector3 _playerPosDebug = Vector3.zero;

        //--------------------------------------------------------------

        //TODO: greedy algorithm - good for prototype but too expensive for real game
        // alternative - UnityEngine.Physics.SphereCastAll but expensive again
        public ITargetable GetClosest(Vector3 toPoint)
        {
            _playerPosDebug = toPoint;

            if (_targets.Count < 1)
            {
                //CurrentTarget = null;
                return null;
            }

            ITargetable closestTarget = null;
            var minDistance = float.MaxValue;
            for (int i = 0; i < _targets.Count; i++)
            {
                if (_targets[i] == null) { continue; }

                var tempDistance = (_targets[i].GetPosition() - toPoint).sqrMagnitude;
                if (tempDistance < _targetingRange * _targetingRange && tempDistance < minDistance)
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

        //--------------------------------------------------------------

        private void Awake()
        {
            _targets = new List<ITargetable>();
            //TODO: use MVP + enemy factory + object pool + HashTables for the cache, etc 
            GetComponentsInChildren(false, _targets);
        }        

        //TODO: test
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_playerPosDebug, _targetingRange);
        }
    }
}