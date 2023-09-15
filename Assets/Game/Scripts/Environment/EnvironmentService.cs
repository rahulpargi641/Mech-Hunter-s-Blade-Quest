using System.Collections.Generic;
using UnityEngine;

public class EnvironmentService : MonoSingletonGeneric<EnvironmentService>
{
    [SerializeField] List<Transform> patrolPoints;
    public List<Transform> PatrolPoints => patrolPoints; // lamda expression

    EnvironmentController environmentController;

    protected override void Awake()
    {
        base.Awake();

        EnvironmentModel environmentModel = new EnvironmentModel();
        environmentController = new EnvironmentController(environmentModel);
    }
}
