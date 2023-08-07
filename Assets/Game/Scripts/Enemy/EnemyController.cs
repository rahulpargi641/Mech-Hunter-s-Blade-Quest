using UnityEngine;

public class EnemyController
{
    readonly EnemyModel model;
    readonly EnemyView view;

    public EnemyController(EnemyModel model, EnemyView view)
    {
        this.model = model;
        this.view = view;

        view.Controller = this;
    }

    public void EnemyMovement()
    {
        float distanceToPlayer = Vector3.Distance(view.playerTransform.position, view.transform.position);
        if (distanceToPlayer >= view.NavMeshAgent.stoppingDistance)
        {
            view.NavMeshAgent.SetDestination(view.playerTransform.position);
            view.Animator.SetFloat("Speed", view.NavMeshAgent.speed);
        }
        else
        {
            view.NavMeshAgent.SetDestination(view.transform.position);
            view.Animator.SetFloat("Speed", 0f);
        }
    }
}
