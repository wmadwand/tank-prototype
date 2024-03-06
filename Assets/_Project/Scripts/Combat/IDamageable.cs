using System;

namespace TestingTask.Combat
{
    public interface IDamageable
    {
        void TakeDamage(float value, Action<ITargetable> callback);
    }
}
