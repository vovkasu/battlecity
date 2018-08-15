using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTargetActions", menuName = "BattleCity/PlayerTargetActions", order = 4)]
public class PlayerTargetActions : ActionsStrategyBase
{
    private TankController _player;
    private System.Random _random;

    public override void Init(TankController player, Eagle eagle)
    {
        _random = new System.Random();
        _player = player;
    }

    public override DirectionParams SelectDirection(List<DirectionParams> possibleDirections, Transform enemyPosition)
    {
        var path = _player.transform.position - enemyPosition.position;

        var additionalDirections = new List<Vector2>();
        Vector2 horizontalDirection = new Vector2(path.x, 0);
        if (horizontalDirection.magnitude > 1)
        {
            additionalDirections.Add(horizontalDirection.normalized);
        }

        Vector2 verticalDirection = new Vector2(0, path.y);
        if (verticalDirection.magnitude > 1)
        {
            additionalDirections.Add(verticalDirection.normalized);
        }

        var directions = new List<DirectionParams>();
        foreach (var possibleDirection in possibleDirections)
        {
            directions.Add(possibleDirection);
            if (additionalDirections.Any(_ => _.Equals(possibleDirection.MoveDirection)))
            {
                directions.Add(possibleDirection);
            }
        }
        return directions[_random.Next(directions.Count)];
    }
}