using UnityEngine;
using UnityEngine.AI;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class PackMuleController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void NavigateToPosition(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
        }
    }
}
