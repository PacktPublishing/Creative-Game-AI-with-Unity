using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter01_CompanionFollow
{
  public class LookAtTarget : MonoBehaviour
  {
    public Transform targetTransform;

    void Update()
    {
      transform.LookAt(targetTransform);
    }
  }
}
