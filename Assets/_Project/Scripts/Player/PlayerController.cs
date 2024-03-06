using TestingTask.Combat;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace TestingTask.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SF] private TargetCollection m_targets;

        [Header("Visuals")]
        [SF] private Transform m_turret;
        [SF] private Transform m_shotPivot;
        [SF] private LineRenderer m_lineRenderer;

        [Header("Misc")]
        [SF] private float m_targetingRange;

        public PlayerTargeting Targeting { private set; get; }
        public PlayerCombat Combat { private set; get; }
        public Transform TurretTransform => m_turret;
        public LineRenderer LaserLine => m_lineRenderer;

        private PlayerMovement _movement;

        //--------------------------------------------------------------

        //TODO: test
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 10);
        }

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            Targeting = new PlayerTargeting(this, m_targetingRange, m_targets);
            Combat = new PlayerCombat(m_shotPivot);
        }

        private void Update()
        {
            _movement.UpdateInput();
            UpdateTargeting();
            UpdateCombat();
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            _movement.UpdateVelocity(deltaTime);
            _movement.UpdateRotation(deltaTime);
        }

        private void UpdateTargeting()
        {
            Targeting.FindClosestTarget();
            Targeting.Process(Time.fixedDeltaTime);
        }

        private void UpdateCombat()
        {
            //TODO: not quite elegant - RemoveFromCollection callback
            Combat.Shoot(Targeting.CurrentTarget, m_targets.Remove);
        }
    }
}