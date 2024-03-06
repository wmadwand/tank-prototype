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
        private Transform _shotPivot;

        public PlayerCombat(Transform shotPivot)
        {
            _playerController = Object.FindObjectOfType<PlayerController>();
            _shotPivot = shotPivot;
        }

        public void Shoot(ITargetable target)
        {
            if (CanShoot() == false || target == null)
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
            laserLine.SetPosition(0, _shotPivot.position);
            laserLine.SetPosition(1, targetPosition);

            laserLine.enabled = true;
            //TODO: to inspector/scriptable settings
            yield return new WaitForSeconds(0.1f);
            laserLine.enabled = false;
        }

        private bool CanShoot()
        {
            return Time.time >= _nextShotTime;
        }
    }
}