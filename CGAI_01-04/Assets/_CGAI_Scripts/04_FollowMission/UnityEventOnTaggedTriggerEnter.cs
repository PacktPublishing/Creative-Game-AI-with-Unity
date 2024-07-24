using UnityEngine;
using UnityEngine.Events;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class UnityEventOnTaggedTriggerEnter : MonoBehaviour
    {
        [SerializeField]
        private string tagToCheck;
        public UnityEvent taggedEnterTriggerEvent;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(tagToCheck))
            {
                Debug.Log("Trigger event with " + tagToCheck);
                taggedEnterTriggerEvent.Invoke();
                transform.gameObject.SetActive(false);
            }
        }
    }
}
