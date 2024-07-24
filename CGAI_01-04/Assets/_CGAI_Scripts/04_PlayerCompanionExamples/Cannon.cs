using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
  public class Cannon : MonoBehaviour, IFireable
  {
    [SerializeField] private GameObject _cannonballPrefab;
    [SerializeField] private Transform _bulletSpawnTransform;
    public void Fire()
    {
      Instantiate(_cannonballPrefab, _bulletSpawnTransform);
    }
  }
}
