using System;
using System.Collections;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public enum FollowMissionSteps
    {
        StartPartOfMission,
        PlayerFollowingToDestination,
        PromptPlayerAtDestination,
        PlayerCompletesPart
    }
    
    public class FollowMissionProgressionController : MonoBehaviour
    {
        [SerializeField]
        private FollowMissionSteps currentStep = FollowMissionSteps.StartPartOfMission;

        private int _currentMissionPartIndex = 0;
        
        public int CurrentMissionPartIndex
        {
            get => _currentMissionPartIndex;
            set
            {
                if (value >= 0)
                {
                    _currentMissionPartIndex = value;
                }
                else
                {
                    Debug.LogError("Mission part index cannot be negative.");
                }
            }
        }
        
        public event Action<FollowMissionSteps> OnMissionStepChanged;
        
        public void StartFirstStep()
        {
            currentStep = FollowMissionSteps.StartPartOfMission;
            OnMissionStepChanged?.Invoke(currentStep);
        }
        
        public void AdvanceStep()
        {
            currentStep++;
            if ((int)currentStep >= Enum.GetValues(typeof(FollowMissionSteps)).Length)
            {
                Debug.LogWarning($"Step {currentStep} does not exist");
                return;
            }
            OnMissionStepChanged?.Invoke(currentStep);
        }

        public IEnumerator WaitThenAdvanceStep(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            AdvanceStep();
        }

        public void AdvanceToNextMissionPart()
        {
            _currentMissionPartIndex++;
        }
    }
}
