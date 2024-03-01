using TestingTask.Combat;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace TestingTask.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SF] private float m_movementSpeed;
        [SF] private float m_rotationSpeed;

        [Header("Visuals")] 
        [SF] private Transform m_turret;
        
        [SF] private LineRenderer m_lineRenderer;

        public PlayerTargeting Targeting { private set; get; }
        public PlayerCombat Combat { private set; get; }
        public Transform TurretTransform => m_turret;
        public LineRenderer LaserLine => m_lineRenderer;
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Targeting = new PlayerTargeting(this);
            Combat = new PlayerCombat();
        }

        private void Update()
        {
            UpdateInput();
            UpdateTargeting();
            UpdateCombat();
        }

        private void UpdateTargeting()
        {
            Targeting.FindClosestTarget();
            Targeting.Process(Time.fixedDeltaTime);
        }

        private void UpdateCombat()
        {
            Combat.Shoot(Targeting.CurrentTarget);
        }
        
        private Vector2 _input;
        private void UpdateInput()
        {
            var verticalInput = Input.GetAxisRaw("Vertical");
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            _input = new Vector2(horizontalInput, verticalInput);
        }
        
        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            UpdateVelocity(deltaTime);
            UpdateRotation(deltaTime);
        }

        private void UpdateVelocity(float deltaTime)
        {
            var movementDirection = new Vector3(_input.x, 0, _input.y).normalized;
            var newVelocity = movementDirection * m_movementSpeed * deltaTime;
            newVelocity.y = _rigidbody.velocity.y;
            
            _rigidbody.velocity = newVelocity;
        }

        private void UpdateRotation(float deltaTime)
        {
            var velocityDirection = _rigidbody.velocity.normalized;
            if (velocityDirection == Vector3.zero || velocityDirection.magnitude <= 0.01f)
                return;
            
            var targetForward = Vector3.Lerp(transform.forward, velocityDirection, m_rotationSpeed * deltaTime);
            targetForward.y = 0f;
            
            transform.forward = targetForward;
        }
    }
}
