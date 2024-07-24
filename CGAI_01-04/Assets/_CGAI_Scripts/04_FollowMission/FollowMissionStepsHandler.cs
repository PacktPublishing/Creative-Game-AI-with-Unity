using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class FollowMissionStepsHandler : MonoBehaviour
    {
        [SerializeField]
        private Transform companionTransform;
        [SerializeField]
        private NpcDialogueDisplayHandler npcDialogueDisplay;
        [SerializeField]
        private FollowMissionProgressionController missionController;
        [SerializeField]
        private FollowMissionPartsContainer mission;

        private NavMeshAgent _companionAgent;

        public event Action StartLookingAtPlayer;
        public event Action StopLookingAtPlayer;

        private void Start()
        {
            InitializeFollowMission();
        }

        private void InitializeFollowMission()
        {
            missionController.OnMissionStepChanged += HandleMissionStepChange;
            if (companionTransform.TryGetComponent(out NavMeshAgent companionAgent))
            {
                _companionAgent = companionAgent;
            }

            if (mission.Parts.Count <= 0)
            {
                Debug.LogError("There are no mission parts.");
                return;
            }
            missionController.CurrentMissionPartIndex = 0;
            MissionGuideSetDestination(mission.Parts[missionController.CurrentMissionPartIndex].startTransform.position);
        }

        private void HandleMissionStepChange(FollowMissionSteps newStep)
        {
            switch (newStep)
            {
                case FollowMissionSteps.StartPartOfMission:
                    HandleStartPartOfMission();
                    break;
                
                case FollowMissionSteps.PlayerFollowingToDestination:
                    HandlePlayerFollowingToDestination();
                    break;

                case FollowMissionSteps.PromptPlayerAtDestination:
                    HandlePromptPlayerAtDestination();
                    break;

                case FollowMissionSteps.PlayerCompletesPart:
                    HandlePlayerCompletesTask();
                    break;
            }
        }
        
        private void HandleStartPartOfMission()
        {
            StartLookingAtPlayer?.Invoke();
            
            List<string> startingDialogue = GetCurrentMissionPart().dialogue.startPartOfMissionDialogue;
            npcDialogueDisplay.PlayDialogueSequence(startingDialogue);
            
            float waitTime = startingDialogue.Count * npcDialogueDisplay.timeBetweenDialogue + npcDialogueDisplay.timeForWaitSpacer;
            StartCoroutine(missionController.WaitThenAdvanceStep(waitTime));
        }

        private void HandlePlayerFollowingToDestination()
        {
            StopLookingAtPlayer?.Invoke();
            
            var part = GetCurrentMissionPart();
            MissionGuideSetDestination(part.nextDestinationTransform.position);
            
            StartCoroutine(npcDialogueDisplay.PlayDialogueSequenceAfterWait(
                part.dialogue.playerFollowingToDestinationDialogue));
        }

        private void HandlePromptPlayerAtDestination()
        {
            StartLookingAtPlayer?.Invoke();
            
            var part = GetCurrentMissionPart();
            StartCoroutine(npcDialogueDisplay.PlayDialogueSequenceAfterWait(
                part.dialogue.promptPlayerAtDestinationDialogue));
        }

        private void HandlePlayerCompletesTask()
        {
            StartLookingAtPlayer?.Invoke();
            
            var part = GetCurrentMissionPart();
            npcDialogueDisplay.PlayDialogueSequence(part.dialogue.playerCompletesPartDialogue);
            
            StartCoroutine(WaitThenStartNewPartOfMission());
        }

        private IEnumerator WaitThenStartNewPartOfMission()
        {
            var part = GetCurrentMissionPart();
            int dialogueLinesToWaitFor = part.dialogue.playerCompletesPartDialogue.Count;
            yield return new WaitForSeconds(dialogueLinesToWaitFor * npcDialogueDisplay.timeBetweenDialogue + npcDialogueDisplay.timeForWaitSpacer);
            missionController.AdvanceToNextMissionPart();
            StopLookingAtPlayer?.Invoke();
            if (missionController.CurrentMissionPartIndex < mission.Parts.Count)
            {
                missionController.StartFirstStep();
            }
            else
            {
                Debug.Log("Player finished follow mission!");
            }
        }
        
        private FollowMissionPartsContainer.MissionPart GetCurrentMissionPart()
        {
            return mission.Parts[missionController.CurrentMissionPartIndex];
        }
        
        private void MissionGuideSetDestination(Vector3 destination)
        {
            _companionAgent.SetDestination(destination);
        }
        
        private void OnDisable()
        {
            missionController.OnMissionStepChanged -= HandleMissionStepChange;
        }
    }
}
