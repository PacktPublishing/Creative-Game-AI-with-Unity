using System;
using _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples;
using TMPro;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter03_PlayerCompanionInteraction
{
    public class PlayerFeedingHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactPromptText;
    
        private IFeedable _feedableTarget;
        private float _foodAmount = 100;

        private void Start()
        {
            interactPromptText.gameObject.SetActive(false);
        }

        public void OnInteract()
        {
            if (_feedableTarget != null)
            {
                _feedableTarget.Feed(_foodAmount);
                interactPromptText.gameObject.SetActive(false);
            }
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IFeedable>(out IFeedable feedable))
            {
                _feedableTarget = feedable;
                interactPromptText.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IFeedable>(out _))
            {
                _feedableTarget = null;
                interactPromptText.gameObject.SetActive(false);
            }
        }
    }
}
