using System.Collections.Generic;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_FollowMission
{
    [CreateAssetMenu(fileName = "FollowMissionDialogue", menuName = "CreativeAIScriptableObjects/FollowMissionDialogue")]
    public class DialogueForFollowMissionPartSo : ScriptableObject
    {
        [TextArea]
        [SerializeField]
        private string noteForMissionPart;
        [TextArea]
        public List<string> startPartOfMissionDialogue;
        [TextArea]
        public List<string> playerFollowingToDestinationDialogue;
        [TextArea]
        public List<string> promptPlayerAtDestinationDialogue;
        [TextArea]
        public List<string> playerCompletesPartDialogue;
    }
}
