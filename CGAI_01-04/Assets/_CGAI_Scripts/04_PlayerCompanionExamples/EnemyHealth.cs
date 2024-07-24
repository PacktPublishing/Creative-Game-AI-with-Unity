using System.Collections;
using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
public class EnemyHealth : MonoBehaviour, IDamageable
{
  private float _timeUntilDeath = 1;
  
  public void TakeDamage(int amount)
  {
    StartCoroutine(DeathRoutine());
  }
  
  IEnumerator DeathRoutine()
  {
    transform.localScale *= .5f;
    yield return new WaitForSeconds(_timeUntilDeath);
    transform.localScale = Vector3.one;
    gameObject.SetActive(false);
  }
}
}
