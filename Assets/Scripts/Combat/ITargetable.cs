using UnityEngine;

namespace TestingTask.Combat
{
    public interface ITargetable
    {
        /// <summary>
        /// Grants access to the GameObject of the interface owning class
        /// </summary>
        GameObject gameObject { get; }
        
        /// <summary>
        /// Get the position of the target
        /// </summary>
        Vector3 GetPosition();
    }   
}
