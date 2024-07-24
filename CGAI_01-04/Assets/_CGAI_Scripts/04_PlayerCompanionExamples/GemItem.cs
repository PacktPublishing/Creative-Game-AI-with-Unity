using System;
using _CreativeGameAI_Scripts.Chapter04_FollowMission;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
public class GemItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string _textPrompt;
    [SerializeField]
    private Vector3 _carryOffset = Vector3.up;
    [SerializeField]
    private SineWaveBob _sineWaveBob;
    
    public string TextPrompt => _textPrompt;
    private Collider _collider;
    private ICarry _carrier;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _carrier = other.GetComponent<ICarry>();
    }

    public void Interact()
    {
        PickUp();
    }
    
    private void PickUp()
    {
        if (_carrier == null) return;
        _carrier.Carry(gameObject, _carryOffset);
        _sineWaveBob.enabled = false;
        _collider.enabled = false;
    }
}
}
