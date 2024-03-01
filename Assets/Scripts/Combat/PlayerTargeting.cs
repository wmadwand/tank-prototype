using System.Collections.Generic;
using TestingTask.Player;
using TestingTask.Target;
using UnityEngine;

namespace TestingTask.Combat
{
    public class PlayerTargeting
    {
        public ITargetable CurrentTarget { private set; get; }
        
        private readonly PlayerController _playerController;

        public PlayerTargeting(PlayerController playerController)
        {
            _playerController = playerController;
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
            var targets = Object.FindObjectsOfType<TargetController>();
            if (targets.Length < 1)
            {
                CurrentTarget = null;
                return;
            }
            
            var closestTarget = targets[0];
            CurrentTarget = closestTarget;
        }
    }
}
