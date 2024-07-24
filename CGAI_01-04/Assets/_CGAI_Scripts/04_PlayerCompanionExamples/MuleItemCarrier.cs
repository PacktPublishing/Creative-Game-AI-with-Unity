using System.Collections.Generic;
using _CreativeGameAI_Scripts.Chapter04_FollowMission;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
public class MuleItemCarrier : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string _textPrompt;
    public string TextPrompt => _textPrompt;
    [SerializeField]
    private List<GameObject> _itemsHeld;
    [SerializeField]
    private Vector3 _carryOffset = Vector3.up / 2;
    
    private GameObject _receivedItem;
    private ICarry _carrier;

    public void Interact()
    {
        ReceiveItem();
    }

    private void ReceiveItem()
    {
        if (_carrier == null) { return; }
        if (!_carrier.IsCarrying) { return; }
        _receivedItem = _carrier.CarriedItem;
        _carrier.Drop();
        _itemsHeld.Add(_receivedItem);
        _receivedItem.transform.position = transform.GetChild(0).position + _carryOffset;
        _receivedItem.transform.SetParent(transform.GetChild(0));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _carrier = other.GetComponent<ICarry>();
    }
}
}
