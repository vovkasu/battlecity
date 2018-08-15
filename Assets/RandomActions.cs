using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "RandomActions", menuName = "BattleCity/RandomActions", order = 3)]
public class RandomActions : ActionsStrategyBase
{
    private Random _random;

    public override void Init(TankController player, Eagle eagle)
    {
        _random = new Random();
    }

    public override DirectionParams SelectDirection(List<DirectionParams> possibleDirections, Transform enemyPosition)
    {
        return possibleDirections[_random.Next(possibleDirections.Count)];
    }
}