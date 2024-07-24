using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class CompanionAgentAnimatorConductor : MonoBehaviour
{
    private NavMeshAgent _companionAgent;
    private Animator _companionAnimator;
    
    private static readonly int IsWalkingStringID = Animator.StringToHash("isWalking");

    void Start()
    {
        _companionAgent = GetComponent<NavMeshAgent>();
        _companionAnimator = GetComponent<Animator>();
    }

    private void StandIdle()
    {
        _companionAnimator.SetBool(IsWalkingStringID,false);
    }

    private void Walk()
    {
        _companionAnimator.SetBool(IsWalkingStringID, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_companionAgent.velocity.magnitude < 0.1f)
        {
            StandIdle();
        }
        else
        {
            Walk();
        }
    }
}
