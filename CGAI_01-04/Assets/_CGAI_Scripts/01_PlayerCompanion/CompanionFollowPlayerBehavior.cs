using UnityEngine;
namespace _CGAI_Scripts._01_PlayerCompanion
{
    public class CompanionFollowPlayerBehavior : MonoBehaviour
    {
        [SerializeField] private Transform m_PlayerTransform;
        [SerializeField] private float m_DistanceToStopFollowing = 3f;
        [SerializeField] private float m_MovementSpeed = 2f;
        [SerializeField] private float m_RotationSpeed = 3f;

        private Vector3 m_CompanionPosition;
        private Vector3 m_PlayerPosition;
        private Vector3 m_DirectionToPlayer;
        
        void Update()
        {
            if (IsByPlayer())
            {
                LookWherePlayerLooks();
                return;
            }
            FollowPlayer();
        }
        
        private bool IsByPlayer()
        {
            m_CompanionPosition = transform.position;
            m_PlayerPosition = m_PlayerTransform.position;
            
            float distanceToPlayer = Vector3.Distance(m_PlayerPosition, m_CompanionPosition);

            return distanceToPlayer < m_DistanceToStopFollowing;
        }
        
        private void LookWherePlayerLooks()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, m_PlayerTransform.rotation,Time.deltaTime * m_RotationSpeed);
        }
        
        private void FollowPlayer()
        {
            m_CompanionPosition = transform.position;
            m_PlayerPosition = m_PlayerTransform.position;

            m_DirectionToPlayer = m_PlayerPosition - m_CompanionPosition;
            m_DirectionToPlayer.Normalize();
            Vector3 nextPosition = m_DirectionToPlayer * (Time.deltaTime * m_MovementSpeed);

            transform.Translate(nextPosition, Space.World);
            
            var facingPlayerRotation = Quaternion.LookRotation(m_DirectionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, facingPlayerRotation, Time.deltaTime * m_RotationSpeed);
        }
    }
}
