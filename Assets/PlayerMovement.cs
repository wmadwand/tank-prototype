using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace TestingTask.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SF] private Camera m_camera;
        [SF] private float m_movementSpeed;
        [SF] private float m_rotationSpeed;

        private Rigidbody _rigidbody;
        private Vector2 _input;

        //--------------------------------------------------------------

        //TODO: put in a separate Input class
        public void UpdateInput()
        {
            var verticalInput = Input.GetAxisRaw("Vertical");
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            _input = new Vector2(horizontalInput, verticalInput);
        }

        public void UpdateVelocity(float deltaTime)
        {
            var cameraRight = m_camera.transform.right;
            var cameraForward = Vector3.Cross(cameraRight, Vector3.up).normalized;
            var movementDirection = (cameraRight * _input.x + cameraForward * _input.y).normalized;
            var newVelocity = movementDirection * m_movementSpeed * deltaTime;
            newVelocity.y = _rigidbody.velocity.y;

            _rigidbody.velocity = newVelocity;
        }

        public void UpdateRotation(float deltaTime)
        {
            var velocityDirection = _rigidbody.velocity.normalized;
            if (velocityDirection == Vector3.zero || velocityDirection.magnitude <= 0.01f) { return; }

            var targetForward = Vector3.Lerp(transform.forward, velocityDirection, m_rotationSpeed * deltaTime);
            targetForward.y = 0f;

            transform.forward = targetForward;
        }

        //--------------------------------------------------------------

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}