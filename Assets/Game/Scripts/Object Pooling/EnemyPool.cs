using UnityEngine;

public class EnemyPool : ObjectPoolGeneric<EnemyController>
{
    private EnemyModel model;
    private EnemyView view;

    public EnemyController GetEnemyContoller(EnemyModel model, EnemyView view)
    {
        this.model = model;
        this.view = view;

        return GetItemFromPool();
    }

    protected override EnemyController CreateItem()
    {
        //EnemyView view = Instantiate(this.view);
        EnemyController controller = new EnemyController(model, view);
        return controller;
    }
}
