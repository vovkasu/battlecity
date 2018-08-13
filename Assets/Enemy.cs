using DefaultNamespace;

public class Enemy : TankController
{
    public void Init(EnemyModel enemyModel)
    {
        View.sprite = enemyModel.Views.Find(_ => _.Name == "Up").Sprite;
        Speed = enemyModel.Speed;
        BulletSpeed = enemyModel.BulletSpeed;

        foreach (var directionParams in Directions)
        {
            directionParams.Sprite = enemyModel.Views.Find(_ => _.Name == directionParams.DirectionName).Sprite;
        }
    }

    public override void Update()
    {
    }
}