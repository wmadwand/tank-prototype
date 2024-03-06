using TestingTask.Player;
using TestingTask.Target.Collection;

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

        public void FindClosestTarget()
        {     
            CurrentTarget = _targets.GetClosest(_playerController.TurretTransform.position, _targetingRange);
        }
    }
}
