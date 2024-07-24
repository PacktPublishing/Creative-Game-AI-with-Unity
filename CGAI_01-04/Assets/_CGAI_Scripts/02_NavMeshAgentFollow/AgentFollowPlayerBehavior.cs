using UnityEngine;
using UnityEngine.AI;
namespace _CreativeGameAI_Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentFollowPlayerBehavior : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float distanceToStopFollowing;

        private NavMeshAgent _companionAgent;
        private Vector3 _companionPosition;
        private Vector3 _playerPosition;
        

        void Start()
        {
            _companionAgent = GetComponent<NavMeshAgent>();
            InvokeRepeating(nameof(AgentFollowPlayer),1, 1);
        }

        public void AgentFollowPlayer()
        {
            if (IsByPlayer())
            {
                return;
            }
            _companionAgent.SetDestination(playerTransform.position);
        }
        
        private bool IsByPlayer()
        {
            _companionPosition = transform.position;
            _playerPosition = playerTransform.position;
            
            float distanceToPlayer = Vector3.Distance(_playerPosition, _companionPosition);

            return distanceToPlayer < distanceToStopFollowing;
        }
    }
}
