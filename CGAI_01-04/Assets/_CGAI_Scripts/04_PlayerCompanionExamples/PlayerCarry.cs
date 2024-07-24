using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
public class PlayerCarry : MonoBehaviour, ICarry
{
    [field: SerializeField]
    public bool IsCarrying
    {
        get;
        private set;
    }
    public GameObject CarriedItem
    {
        get;
        private set;
    }
    
    public void Carry(GameObject itemToCarry, Vector3 carryOffset)
    {
        IsCarrying = true;
        CarriedItem = itemToCarry;
        Transform transformOfCarried = itemToCarry.transform;
        transformOfCarried.position = transform.position + carryOffset;
        transformOfCarried.SetParent(transform);
    }
    
    public void Drop()
    {
        IsCarrying = false;
        CarriedItem.transform.SetParent(null);
        CarriedItem = null;
    }
}
}
