using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class FollowTargetBehavior : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private float _distanceToStopFollowing;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        
        private Vector3 _myPosition;
        private Vector3 _targetPosition;
        private Vector3 _directionToTarget;
        
        void Update()
        {
            if (IsCloseToTarget())
            {
                return;
            }
            
            FollowTarget();
        }
        
        private bool IsCloseToTarget()
        {
            _myPosition = transform.position;
            _targetPosition = _targetTransform.position;
            
            float distanceToTarget = Vector3.Distance(_targetPosition, _myPosition);

            return distanceToTarget < _distanceToStopFollowing;
        }

        private void FollowTarget()
        {
            _myPosition = transform.position;
            _targetPosition = _targetTransform.position;

            _directionToTarget = _targetPosition - _myPosition;
            _directionToTarget.Normalize();
            Vector3 nextPosition = _directionToTarget * (Time.deltaTime * _movementSpeed);
            
            transform.Translate(nextPosition, Space.World);
            
            var facingPlayerRotation = Quaternion.LookRotation(_directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, facingPlayerRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}

