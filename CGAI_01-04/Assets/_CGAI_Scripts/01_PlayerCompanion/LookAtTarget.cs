using UnityEngine;
namespace _CGAI_Scripts._01_PlayerCompanion
{
  public class LookAtTarget : MonoBehaviour
  {
    public Transform TargetTransform;

    void Update()
    {
      transform.LookAt(TargetTransform);
    }
  }
}
