using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
public class SidekickBehavior : MonoBehaviour
{
[SerializeField]
private Transform _playerTransform;
[SerializeField]
private CompanionFollowPlayerBehavior _companionFollowPlayerBehavior;
[SerializeField]
private float _detectionRadius = 15.0f;
[SerializeField]
private float _detectionInterval = 0.5f;
[SerializeField]
private float _fireRate = 1.0f;
[SerializeField]
private float _rotationSpeed = 3f;

private IFireable _fireable;
private float _nextFireTime;
private float _nextDetectionTime;
private Transform _currentTarget;
private int _maxColliders = 10;


private void Start()
{
    _fireable = GetComponent<IFireable>();
}

void Update()
{
    ExecuteCompanionActions();
}

void ExecuteCompanionActions()
{
    HandleEnemyDetection();
    HandleAiming();
    HandleAttacking();
    
    void HandleEnemyDetection()
    {
        if (!(Time.time > _nextDetectionTime)) return;
        
        DetectNearbyEnemies();
        _nextDetectionTime = Time.time + _detectionInterval;
    }

    void HandleAiming()
    {
        if (_companionFollowPlayerBehavior.IsMoving) return;
        if (_currentTarget == null) return;

        AimAtTarget(_currentTarget);
    }
    
    void HandleAttacking()
    {
        if (_companionFollowPlayerBehavior.IsMoving) return;
        if (_currentTarget == null) return;
        if (!(Time.time > _nextFireTime)) return;
        
        AttackEnemies();
        _nextFireTime = Time.time + 1 / _fireRate;
    }
}

private void DetectNearbyEnemies()
{
    Collider[] hitColliders = new Collider[_maxColliders];
    int numberOfColliders = PopulateHitColliders(hitColliders);
    _currentTarget = FindClosestTarget(numberOfColliders, hitColliders);
}

private int PopulateHitColliders(Collider[] hitColliders)
{
    return Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, hitColliders);
}

private Transform FindClosestTarget(int numberOfColliders, Collider[] hitColliders)
{
    float closestDistance = _detectionRadius;
    Transform closestTarget = null;
    
    for (int i = 0; i < numberOfColliders; i++)
    {
        IDamageable damageable = hitColliders[i].GetComponent<IDamageable>();
        
        if (damageable == null) { continue; }
        
        float distance = Vector3.Distance(_playerTransform.position, hitColliders[i].transform.position);
        if (!(distance < closestDistance)) { continue; }
        
        closestDistance = distance;
        closestTarget = hitColliders[i].transform;
    }
    return closestTarget;
}

private void AimAtTarget(Transform target)
{
    Vector3 directionToTarget = (target.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
}

private void AttackEnemies()
{
    _fireable.Fire();
}
}
}
