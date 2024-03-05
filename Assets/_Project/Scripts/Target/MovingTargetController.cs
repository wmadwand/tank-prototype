using System.Collections;
using System.Collections.Generic;
using TestingTask.Combat;
using UnityEngine;
using SF = UnityEngine.SerializeField;

public class MovingTargetController : MonoBehaviour, IDamageable, ITargetable
{
    [SF] private float m_movementTime = 1f;
    [SF] private Vector3 m_targetOffset;

    private TargetHealth _health;

    private void Awake()
    {
        StartCoroutine(MovementRoutine());
        _health = new TargetHealth(100);
    }

    private IEnumerator MovementRoutine()
    {
        var origin = transform.position;
        var targetPosition = origin + m_targetOffset;
        var goingBack = false;
        var passedTime = 0f;
        
        while (true)
        {
            passedTime += Time.deltaTime;

            if (passedTime >= m_movementTime)
            {
                passedTime = 0f;
                goingBack = !goingBack;
            }
            
            var startPosition = goingBack ? targetPosition : origin;
            var target = goingBack ? origin : targetPosition;
            var normalisedTime = passedTime / m_movementTime;
            
            transform.position = Vector3.Lerp(startPosition, target, normalisedTime);
            yield return new WaitForEndOfFrame();
        }
    }
    
    public void TakeDamage(float value)
    {
        _health.Remove(value);

        if (_health.Value <= 0)
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
