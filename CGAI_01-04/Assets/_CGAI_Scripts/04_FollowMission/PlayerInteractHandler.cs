using _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples;
using TMPro;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_FollowMission
{
    public class PlayerInteractHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _interactPromptText;
    
        private IInteractable _interactable;

        private void OnEnable()
        {
            _interactPromptText.gameObject.SetActive(false);
        }

        public void OnInteract()
        {
            if (_interactable != null)
            {
                _interactable.Interact();
                _interactPromptText.gameObject.SetActive(false);
            }
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                _interactable = interactable;
                _interactPromptText.text = interactable.TextPrompt;
                _interactPromptText.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out _))
            {
                return;
            }
            _interactable = null;
            _interactPromptText.gameObject.SetActive(false);
        }
    }
}
