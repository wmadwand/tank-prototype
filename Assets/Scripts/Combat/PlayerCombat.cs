using System.Collections;
using System.Collections.Generic;
using TestingTask.Player;
using UnityEngine;

namespace TestingTask.Combat
{
    public class PlayerCombat
    {
        private readonly PlayerController _playerController;

        private float _nextShotTime;

        private const float SHOT_COOLDOWN = 1f;

        public PlayerCombat()
        {
            _playerController = Object.FindObjectOfType<PlayerController>();
        }

        public void Shoot(ITargetable target)
        {
            if (CanShoot() == false)
                return;

            var damageTarget = target.gameObject.GetComponent<IDamageable>();
            if (damageTarget == null)
                return;

            RenderLaser(target.GetPosition());
            damageTarget.TakeDamage(50);
            _nextShotTime = Time.time + SHOT_COOLDOWN;
        }

        //TODO: Use TrailRenderer
        private void RenderLaser(Vector3 targetPosition)
        {
            _playerController.StartCoroutine(RenderLaserRoutine(targetPosition));
        }

        private IEnumerator RenderLaserRoutine(Vector3 targetPosition)
        {
            var laserLine = _playerController.LaserLine;
            laserLine.SetPosition(0, laserLine.transform.position);
            laserLine.SetPosition(1, targetPosition);
            laserLine.enabled = true;

            yield return new WaitForSeconds(0.1f);

            laserLine.enabled = false;
        }

        private bool CanShoot()
        {
            return Time.time >= _nextShotTime;
        }
    }
}