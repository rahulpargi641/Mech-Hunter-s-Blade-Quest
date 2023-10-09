using System.Collections.Generic;
using UnityEngine;


public class EnvironmentService : MonoSingletonGeneric<EnvironmentService>
{
    [SerializeField] Transform enemy1PatrolPoints;
    [SerializeField] Transform enemy2PatrolPoints;
    [SerializeField] Transform enemy3PatrolPoints;
    [SerializeField] Transform enemy4PatrolPoints;
    [SerializeField] Transform enemy5PatrolPoints;
    [SerializeField] Transform enemy6PatrolPoints;
    [SerializeField] Transform enemy7PatrolPoints;

    public Dictionary<int, Transform> enemyPatrolPoints = new Dictionary<int, Transform>();

    private void Start()
    {
        enemyPatrolPoints.Add(1, enemy1PatrolPoints);
        enemyPatrolPoints.Add(2, enemy2PatrolPoints);
        enemyPatrolPoints.Add(3, enemy3PatrolPoints);
        enemyPatrolPoints.Add(4, enemy3PatrolPoints);
        enemyPatrolPoints.Add(5, enemy3PatrolPoints);
        enemyPatrolPoints.Add(6, enemy3PatrolPoints);
        enemyPatrolPoints.Add(7, enemy3PatrolPoints);
    }
}
