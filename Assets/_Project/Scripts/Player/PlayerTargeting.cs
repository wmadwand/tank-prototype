using TestingTask.Player;

namespace TestingTask.Combat
{
    public class PlayerTargeting
    {
        public ITargetable CurrentTarget { private set; get; }

        private readonly PlayerController _playerController;
        private readonly TargetCollection _targets;

        private const float RANGE = 10;

        public PlayerTargeting(PlayerController playerController, TargetCollection targets)
        {
            _playerController = playerController;
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
        public void FindClosestTarget()
        {
            if (_targets.Targets.Count < 1)
            {
                CurrentTarget = null;
                return;
            }

            var playerPos = _playerController.TurretTransform.position;
            var closestTarget = _targets.Targets[0];
            var closestTargetDistance = (closestTarget.GetPosition() - playerPos).sqrMagnitude;
            for (int i = 0; i < _targets.Targets.Count; i++)
            {
                if (_targets.Targets[i] == null)
                {
                    continue;
                }

                var tempDistance = (_targets.Targets[i].GetPosition() - playerPos).sqrMagnitude;
                if (tempDistance < RANGE * RANGE && tempDistance < closestTargetDistance)
                {
                    closestTarget = _targets.Targets[i];
                    closestTargetDistance = tempDistance;
                }
            }

            if (closestTargetDistance < RANGE * RANGE)
            {
                CurrentTarget = closestTarget;
            }
            
            //_targets.Targets.Remove(closestTarget);
        }
    }
}
