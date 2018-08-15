using System.Collections.Generic;
using UnityEngine;

public abstract class ActionsStrategyBase : ScriptableObject
{
    public abstract void Init(TankController player, Eagle eagle);
    public abstract DirectionParams SelectDirection(List<DirectionParams> possibleDirections, Transform enemyPosition);
    public float StrategyDuration;
}