using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class OnEnableLookAtTarget : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        private bool _isLookingAtTarget;

        private Vector3 lookDirection;
    
        void OnEnable()
        {
            _isLookingAtTarget = true;
        }

        void Update()
        {
            if (_isLookingAtTarget)
            {
                LookAtTarget();
            }
        }

        private void LookAtTarget()
        {
            lookDirection = transform.position - target.position;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        void OnDisable()
        {
            _isLookingAtTarget = false;
        }
    }
}
