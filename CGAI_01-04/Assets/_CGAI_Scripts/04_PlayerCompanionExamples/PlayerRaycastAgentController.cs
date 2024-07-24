using UnityEditor;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
public class PlayerRaycastAgentController : MonoBehaviour
{
  [SerializeField] private float _raycastDistance = 100f;
  [SerializeField] private LayerMask _hitLayers;
  [SerializeField] private PackMuleController _packMuleController;
  [SerializeField] private Camera _camera;
  private Vector3? _lastHitPoint = null;
  
  public void OnOrder()
  {
    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out var hit, _raycastDistance, _hitLayers))
    {
      Debug.DrawLine(_camera.transform.position, hit.point, Color.red, 2f);
      _packMuleController.NavigateToPosition(hit.point);
      _lastHitPoint = hit.point;
    }
  }
  
  void OnDrawGizmos()
  {
    if (_lastHitPoint.HasValue)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(_lastHitPoint.Value, 0.5f);
    }
  }
}
}
