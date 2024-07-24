using System;
using System.Collections;
using UnityEngine;

namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
public class Cannonball : MonoBehaviour
{
  [SerializeField] 
  private float _forceAmount = 2;
  [SerializeField] 
  private int _damageAmount = 10;
  [Tooltip("How long the cannonball is alive")]
  [SerializeField] 
  private float _timeTillSelfDestruct = 5f;
  
  private float _timeActivated;

  private Rigidbody _ballRigidbody;
  
  private void Awake()
  {
    _ballRigidbody = GetComponent<Rigidbody>();
  }

  private void OnEnable()
  {
    _timeActivated = Time.time;
    ApplyForce();
    DetachParent();
  }

  private void ApplyForce()
  {
    _forceAmount *= UnityEngine.Random.Range(.5f, 1.2f);
    _ballRigidbody.AddForce(transform.parent.forward * _forceAmount, ForceMode.Impulse);
  }
  
  private void DetachParent()
  {
    transform.SetParent(null);
  }

  private void Update()
  {
    if (Time.time - _timeActivated >= _timeTillSelfDestruct)
    {
      SelfDestruct();
    }
  }

  private void OnCollisionEnter(Collision other)
  {
    var damageable = other.transform.GetComponent<IDamageable>();
    if (damageable == null) { return; }
    damageable.TakeDamage(_damageAmount);
    SelfDestruct();
  }
  
  private void SelfDestruct()
  {
    Destroy(gameObject);
  }
}
}
