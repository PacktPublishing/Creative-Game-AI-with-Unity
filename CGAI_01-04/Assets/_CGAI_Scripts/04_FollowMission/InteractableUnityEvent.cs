using System;
using _CreativeGameAI_Scripts.Chapter04_FollowMission;
using UnityEngine;
using UnityEngine.Events;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class InteractableUnityEvent : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private UnityEvent interactionEvent;
        [SerializeField]
        private string textPrompt;
        private bool _hasInteracted;

        private void Start()
        {
            _hasInteracted = false;
        }

        public string TextPrompt
        {
            get => textPrompt;
            set => textPrompt = value;
        }

        public void Interact()
        {
            if (_hasInteracted)
            {
                return;
            }
            interactionEvent.Invoke();
            _hasInteracted = true;
        }
    }
}
