using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class MoveToController : MonoBehaviour
{
    public float duration = 1f;

    private float _duration;

    protected bool _isInMovement;

    public bool IsInMovement { get { return _isInMovement; } }

    private float _startTime;

    public UnityEvent MovementDone = new UnityEvent();

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    public void MoveTo(Vector3 position)
    {
        _startPosition = transform.position;
        _endPosition = position;

        _startTime = Time.time;
        _duration = Mathf.Clamp(duration, 0.1f, float.MaxValue);
        _isInMovement = true;
    }

    public void StopMoving()
    {
        _isInMovement = false;
    }

    void FixedUpdate()
    {
        if (_isInMovement)
        {
            float timeSinceStarted = Time.time - _startTime;
            float percentageComplete = timeSinceStarted / _duration;

            transform.position = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                _isInMovement = false;
                MovementDone.Invoke();
            }
        }
    }
}
