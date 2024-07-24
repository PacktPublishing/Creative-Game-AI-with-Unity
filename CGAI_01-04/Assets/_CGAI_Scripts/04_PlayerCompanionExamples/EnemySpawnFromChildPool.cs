using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    /// <summary>
    /// Add enemies as children of this object
    /// </summary>
public class EnemySpawnFromChildPool : MonoBehaviour
{
    [SerializeField] private Transform _enemyTarget;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _distanceToSpawnAt;
    
    private List<Transform> _childTransforms;

    private void Start()
    {
        InitializeChildEnemies();
        StartCoroutine(SpawnEnemyRoutine());
    }

    private void InitializeChildEnemies()
    {
        _childTransforms = new List<Transform>();
        foreach (Transform child in transform)
        {
            _childTransforms.Add(child);
            child.gameObject.SetActive(false);
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            if (_enemyTarget != null)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Transform enemyTransform = FindInactiveChildTransform();
        if (enemyTransform != null)
        {
            PositionEnemy(enemyTransform);
            ActivateEnemy(enemyTransform);
        }
    }

    void PositionEnemy(Transform enemyTransform)
    {
        Vector3 enemyPosition = CalculateRandomPositionAroundCircle();
        enemyTransform.position = enemyPosition + _enemyTarget.position;
    }

    private Vector3 CalculateRandomPositionAroundCircle()
    {
        var theta = UnityEngine.Random.Range(0, Mathf.PI * 2);
        return new Vector3(
            Mathf.Cos(theta) * _distanceToSpawnAt,
            0,
            Mathf.Sin(theta) * _distanceToSpawnAt
        );
    }

    private Transform FindInactiveChildTransform()
    {
        return _childTransforms.Find(t => !t.gameObject.activeInHierarchy);
    }

    private void ActivateEnemy(Transform availableTransform)
    {
        availableTransform.gameObject.SetActive(true);
    }
}
}