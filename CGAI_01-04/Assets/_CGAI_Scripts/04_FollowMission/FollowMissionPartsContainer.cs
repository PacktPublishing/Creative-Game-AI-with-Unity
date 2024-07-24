using System;
using System.Collections.Generic;
using _CreativeGameAI_Scripts.Chapter04_FollowMission;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class FollowMissionPartsContainer : MonoBehaviour
    {
        [Serializable]
        public struct MissionPart
        {
            public Transform startTransform;
            public DialogueForFollowMissionPartSo dialogue;
            public Transform nextDestinationTransform;
        }
        
        [SerializeField] private List<MissionPart> parts;

        public List<MissionPart> Parts => parts;
    }
}
