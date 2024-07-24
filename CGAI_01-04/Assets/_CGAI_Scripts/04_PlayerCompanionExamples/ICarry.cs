using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
public interface ICarry
{
    public bool IsCarrying { get; }
    public GameObject CarriedItem { get; }
    public void Carry(GameObject itemToCarry, Vector3 carryOffset);
    public void Drop();
}
}