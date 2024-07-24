using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class CompanionFollowPlayerBehavior : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float distanceToStopFollowing;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        
        public bool IsMoving { get; private set; }
        
        private Vector3 _companionPosition;
        private Vector3 _playerPosition;
        private Vector3 _directionToPlayer;
        
        void Update()
        {
            if (IsByPlayer())
            {
                //LookWherePlayerLooks();
                IsMoving = false;
                return;
            }
            FollowPlayer();
            IsMoving = true;
        }
        
        private bool IsByPlayer()
        {
            _companionPosition = transform.position;
            _playerPosition = playerTransform.position;
            
            float distanceToPlayer = Vector3.Distance(_playerPosition, _companionPosition);

            return distanceToPlayer < distanceToStopFollowing;
        }
        private void LookWherePlayerLooks()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, playerTransform.rotation,Time.deltaTime * rotationSpeed);
        }
        private void FollowPlayer()
        {
            _companionPosition = transform.position;
            _playerPosition = playerTransform.position;

            _directionToPlayer = _playerPosition - _companionPosition;
            _directionToPlayer.Normalize();
            Vector3 nextPosition = _directionToPlayer * (Time.deltaTime * movementSpeed);

            transform.Translate(nextPosition, Space.World);
            
            var facingPlayerRotation = Quaternion.LookRotation(_directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, facingPlayerRotation, Time.deltaTime * rotationSpeed);
        }
    }
}