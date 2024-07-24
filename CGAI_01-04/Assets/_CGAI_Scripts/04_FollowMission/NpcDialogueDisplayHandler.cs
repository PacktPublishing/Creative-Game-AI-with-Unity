using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class NpcDialogueDisplayHandler : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI missionGuideTMP;
        [SerializeField]
        private Transform missionGuideDialogueParent;
        public float timeBetweenDialogue;
        public float timeForWaitSpacer = 3;

        public void PlayDialogueSequence(List<string> dialogues)
        {
            StartCoroutine(ExecuteDialogueSequence(dialogues));
        }

        private IEnumerator ExecuteDialogueSequence(List<string> dialogues)
        {
            foreach (string dialogue in dialogues)
            {
                missionGuideDialogueParent.gameObject.SetActive(true);
                missionGuideTMP.text = dialogue;
                yield return new WaitForSeconds(timeBetweenDialogue);
                missionGuideDialogueParent.gameObject.SetActive(false);
            }
        }

        public IEnumerator PlayDialogueSequenceAfterWait(List<String> dialogueToPlay)
        {
            yield return new WaitForSeconds(timeForWaitSpacer);
            PlayDialogueSequence(dialogueToPlay);
        }
    }
}
