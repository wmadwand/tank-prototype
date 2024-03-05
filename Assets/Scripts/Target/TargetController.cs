using TestingTask.Combat;
using UnityEngine;

namespace TestingTask.Target
{
    public class TargetController : MonoBehaviour, ITargetable, IDamageable
    {
        private TargetHealth _health;

        private void Awake()
        {
            _health = new TargetHealth(100);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void TakeDamage(float value)
        {
            _health.Remove(value);

            if (_health.Value <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
