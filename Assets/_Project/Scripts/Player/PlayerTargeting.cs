using TestingTask.Player;

namespace TestingTask.Combat
{
    public class PlayerTargeting
    {
        public ITargetable CurrentTarget { private set; get; }

        private readonly PlayerController _playerController;
        private readonly TargetCollection _targets;
        private readonly float _targetingRange = 10;

        //--------------------------------------------------------------

        public PlayerTargeting(PlayerController playerController, float targetingRange, TargetCollection targets)
        {
            _playerController = playerController;
            _targetingRange = targetingRange;
            _targets = targets;
        }

        public void Process(float deltaTime)
        {
            var playerTurret = _playerController.TurretTransform;
            var targetPosition = _playerController.transform.position + _playerController.transform.forward;
            targetPosition.y = playerTurret.position.y;

            if (CurrentTarget != null)
            {
                targetPosition = CurrentTarget.GetPosition();
            }

            playerTurret.LookAt(targetPosition);
        }

        //TODO: greedy algorithm - good for prototype but too expensive for real game
        // alternative - UnityEngine.Physics.SphereCastAll but expensive again
        public void FindClosestTarget()
        {
            //if (_targets.Targets.Count < 1)
            //{
            //    CurrentTarget = null;
            //    return;
            //}

            //var playerPos = _playerController.TurretTransform.position;
            //ITargetable closestTarget = null;
            //var minDistance = float.MaxValue;
            //for (int i = 0; i < _targets.Targets.Count; i++)
            //{
            //    if (_targets.Targets[i] == null)
            //    {
            //        continue;
            //    }

            //    var tempDistance = (_targets.Targets[i].GetPosition() - playerPos).sqrMagnitude;
            //    if (tempDistance < _targetingRange * _targetingRange && tempDistance < minDistance)
            //    {
            //        closestTarget = _targets.Targets[i];
            //        minDistance = tempDistance;
            //    }
            //}

            CurrentTarget = _targets.GetClosest(_playerController.TurretTransform.position, _targetingRange);
        }
    }
}
