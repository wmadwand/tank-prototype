using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace TestingTask.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SF] private Transform m_target;
        [SF] private float m_followSpeed;
        [SF] private float m_rotationSpeed;
        
        private float _xRotation;
        
        private void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }

        private void UpdatePosition()
        {
            var targetPosition = m_target.position;
            var smoothedPosition = Vector3.Lerp(transform.position, targetPosition, m_followSpeed * Time.deltaTime);
            
            transform.position = smoothedPosition;
        }

        private void UpdateRotation()
        {
            var horizontalInput = Input.GetAxis("Mouse X");
            _xRotation += horizontalInput * m_rotationSpeed;
            var targetRotation = Quaternion.Euler(0, _xRotation, 0);
            
            transform.rotation = targetRotation;
        }
    }
}
