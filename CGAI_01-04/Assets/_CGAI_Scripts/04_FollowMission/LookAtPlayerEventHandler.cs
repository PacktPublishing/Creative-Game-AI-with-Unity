using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class LookAtPlayerEventHandler : MonoBehaviour
    {
        [SerializeField]
        private FollowMissionStepsHandler missionStepsHandler;
        [SerializeField]
        private Transform playerTransform;
        private bool _isLookingAtPlayer;

        private void Start()
        {
            missionStepsHandler.StartLookingAtPlayer += StartLooking;
            missionStepsHandler.StopLookingAtPlayer += StopLooking;
        }

        private void StartLooking()
        {
            _isLookingAtPlayer = true;
            StartCoroutine(LookAtPlayer());
        }
        private void StopLooking()
        {
            _isLookingAtPlayer = false;
        }

        private IEnumerator LookAtPlayer()
        {
            while (_isLookingAtPlayer)
            {
                transform.LookAt(playerTransform);
                yield return null;
            }
        }

        private void OnDisable()
        {
            missionStepsHandler.StartLookingAtPlayer -= StartLooking;
            missionStepsHandler.StopLookingAtPlayer -= StopLooking;
        }
    }
}
