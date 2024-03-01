using TestingTask.Combat;
using UnityEngine;

namespace TestingTask.Target
{
    public class TargetController : MonoBehaviour, ITargetable, IDamageable
    {
        public Vector3 GetPosition()
        {
            return Vector3.zero;
        }

        public void TakeDamage()
        {
            Destroy(gameObject);
        }
    }
}
